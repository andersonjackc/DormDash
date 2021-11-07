using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.Http;
using DormDash;
using WebApplication1;

namespace DormDash.Pages
{
    public class OrderConfirmedModel : PageModel
    {

        //passing the order

        public void OnGet()
        {
            //get user and query db for 'user'
            int orderNumber = Int32.Parse(HttpContext.Session.GetString("newOrderId"));

           

        }
    }
}