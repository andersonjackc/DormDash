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
        UserType userType;
        public void OnGet()
        {

            if(userType == UserType.Customer)
            {
                User loggedInCustomer = Utils.GetComplexObject<User>(HttpContext.Session, "user");

                List<Order> orderList = DatabaseOperations.selectOrdersByUserID(loggedInCustomer.id);
                HttpContext.Session.SetComplexObject<List<Order>>("orderList", orderList);
            }
            else if(userType == UserType.Employee)
            {
                List<Order> ordersReadyToPick;
            }
            else
            {
                // Sparts admin
            }

        }
    }
}
