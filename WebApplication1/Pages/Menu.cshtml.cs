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
        public string paymentMethod;
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
            paymentMethod = Request.Form["payment_method"];
            Console.WriteLine(paymentMethod);

            string[] itemArr = items.Split(',');

            if (itemArr.Length >= 1 && itemArr[0].Length >= 1){
                foreach(string itemId in itemArr)
                {
                    menuItems.Add(DatabaseOperations.selectMenuItemsById(int.Parse(itemId)));
                }
                foreach (MenuItem menuItem in menuItems)
                {
                    totalOrderPrice += menuItem.price;
                }

                // check if uswer has sufficient funds for order
                if(paymentMethod == "flex")
                {
                    if (user.flexBalance < totalOrderPrice)
                    {
                        HttpContext.Session.SetString("errMessage", "Insufficient Funds on Flex.");
                        Response.Redirect("/Menu");
                    }
                    else
                    {
                        user.flexBalance -= totalOrderPrice;
                        DatabaseOperations.updateUser(user);
                        HttpContext.Session.SetComplexObject<User>("user", user);
                    }
                }
                else if(paymentMethod == "dining")
                {
                    if(user.diningBalance < totalOrderPrice)
                    {
                        HttpContext.Session.SetString("errMessage", "Insufficient Funds on Dining.");
                        Response.Redirect("/Menu");
                    }
                    else
                    {
                        user.diningBalance -= totalOrderPrice;
                        DatabaseOperations.updateUser(user);

                        HttpContext.Session.SetComplexObject<User>("user", user);

                    }
                }
                order = new Order(0, user.id, DateTime.Now, totalOrderPrice, dest, menuItems, false);
                order.Status = Order.status.waiting;
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
                    order.id = newOrderId;
                    HttpContext.Session.SetComplexObject<Order>("newOrder", order);
                    Response.Redirect("/OrderConfirmed");

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
