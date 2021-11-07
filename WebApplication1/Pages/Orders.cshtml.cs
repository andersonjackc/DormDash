using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1;
using Microsoft.AspNetCore.Http;

namespace DormDash.Pages
{
    public class OrdersModel : PageModel
    {
        public void OnGet()
        { 
            User loggedInCustomer = Utils.GetComplexObject<User>(HttpContext.Session, "user");

            if(loggedInCustomer.userType == UserType.Customer)
            {
                List<Order> orderList = DatabaseOperations.selectOrdersByUserID(loggedInCustomer.id);
                HttpContext.Session.SetComplexObject<List<Order>>("orderList", orderList);
            }
            else if(loggedInCustomer.userType == UserType.Employee)
            {
                List<Order> ordersReadyToPickUp = DatabaseOperations.selectOrdersByStatus(Order.status.pickupReady);
                HttpContext.Session.SetComplexObject<List<Order>>("orderList", ordersReadyToPickUp);
            }
            else
            {
                // Sparts admin
                Response.Redirect("/SpartsAdmin");
                return;
            }

        }
    }
}
