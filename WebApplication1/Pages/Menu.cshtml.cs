using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DormDash;
using Microsoft.AspNetCore.Http;
using WebApplication1;

namespace DormDash.Pages
{
    public class MenuModel : PageModel
    {
        public string items;

        public List<MenuItem> menuItems = new List<MenuItem>();

        public double totalOrderPrice = 0.0;

        public User user;

        public Order order;

        public building blding;

        public int roomNum;

        public Destination dest;

        public int newOrderId;
        // user
        public void OnGet()
        {
            List<MenuItem> items = DatabaseOperations.selectMenuItems();
            HttpContext.Session.SetComplexObject<List<MenuItem>>("menuItems",items);
        }

        public void OnPost()
        {
            user = HttpContext.Session.GetComplexObject<User>("user");
            items = Request.Form["itemsToPurchase"];
            blding = (building)Int32.Parse(Request.Form["dest"]);
            roomNum = Int32.Parse(Request.Form["roomNum"]);
            dest = new Destination(blding, roomNum);

            string[] itemArr = items.Split(',');

            if (itemArr.Length > 1){
                foreach(string itemId in itemArr)
                {
                    menuItems.Add(DatabaseOperations.selectMenuItemsById(int.Parse(itemId)));
                }
                foreach (MenuItem menuItem in menuItems)
                {
                    totalOrderPrice += menuItem.price;
                }

                order.Status = Order.status.waiting;
                // check if uswer has sufficient funds for order
                order = new Order(0, user.id, DateTime.Now, totalOrderPrice, dest, menuItems, false);
                newOrderId = DatabaseOperations.insertOrder(order);

                // -1 means failure
                if(newOrderId == -1)
                {
                    HttpContext.Session.SetString("errMessage", "An error occurred sending your order in. Please try again.");
                    Response.Redirect("/Menu");

                }
                // else order succeeded
                else
                {
                    HttpContext.Session.SetString("newOrderId", newOrderId.ToString());
                    Response.Redirect("/OrderConfirm");

                }
            }
            else
            {
                HttpContext.Session.SetString("errMessage", "You must select at least one item to add to your order.");
                Response.Redirect("/Menu");
            }
           

        }
    }
}
