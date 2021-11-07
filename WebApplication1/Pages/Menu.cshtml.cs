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

        public Destination dest;
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
            dest = HttpContext.Session.GetComplexObject<Destination>("dest");

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


                // check if uswer has sufficient funds for order
                order = new Order(0, user.id, DateTime.Now, totalOrderPrice, dest, menuItems, false);
                DatabaseOperations.insertOrder(order);
            }
            else
            {
                HttpContext.Session.SetString("errMessage", "You must select at least one item to add to your order.");
                Response.Redirect("/Menu");
            }
           

        }
    }
}
