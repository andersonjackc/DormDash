using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using DormDash;
using WebApplication1;

namespace DormDash.Pages
{
    public class SpartsAdminModel : PageModel
    {
        public void OnGet()
        {
            List <Order> waitingOrders = DatabaseOperations.selectOrdersByStatus(Order.status.waiting);

            List<Order> ackedOrders = DatabaseOperations.selectOrdersByStatus(Order.status.acknowledged);

            List<Order> pickupOrders = DatabaseOperations.selectOrdersByStatus(Order.status.pickupReady);



            HttpContext.Session.SetComplexObject<List<Order>>("waitingOrders",waitingOrders);
            HttpContext.Session.SetComplexObject<List<Order>>("ackedOrders", ackedOrders);
            HttpContext.Session.SetComplexObject<List<Order>>("pickupOrders", pickupOrders);



        }

        public void OnPost()
        {

        }
    }
}