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
    public class AccountModel : PageModel
    {

        public void OnGet()
        {
            //get the complex obj --user
            User testCustomer = new User(2,UserType.Customer,100.00,100.00,"testuser@ycp.edu","mypasswordisgood" );

            User user = testCustomer;
            if(user.userType == UserType.Customer)
            {
                List<Order> userOrder = DatabaseOperations.selectOrdersByUserID(900);

                HttpContext.Session.SetComplexObject<List<Order>>("userOrders",userOrder );


                
            }else if (user.userType == UserType.Employee)
            {

            }else if(user.userType == UserType.SpartsAdmin)
            {

            }

            

        }
    }
}