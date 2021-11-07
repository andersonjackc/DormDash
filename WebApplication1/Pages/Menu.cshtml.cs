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
        public void OnGet()
        {
            List<MenuItem> items = DatabaseOperations.selectMenuItems();
            HttpContext.Session.SetComplexObject<List<MenuItem>>("menuItems",items);
        }

        public void OnPost()
        {
            items = Request.Form["itemsToPurchase"];

        }
    }
}
