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

        public List<double> prices;

        public Order order;
        // user
        public void OnGet()
        {
            List<MenuItem> items = DatabaseOperations.selectMenuItems();
            HttpContext.Session.SetComplexObject<List<MenuItem>>("menuItems",items);
        }

        public void OnPost()
        {
            items = Request.Form["itemsToPurchase"];

            string[] itemArr = items.Split(',');

            if (itemArr.Length > 1){
                foreach(string itemId in itemArr)
                {
                    menuItems.Add(DatabaseOperations.selectMenuItemsById(int.Parse(itemId)));
                }

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
