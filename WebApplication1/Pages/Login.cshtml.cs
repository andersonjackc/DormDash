using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using WebApplication1;
using System.Text;
using DormDash;

namespace WebApplication1.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class LoginModel : PageModel
    {
        public string username;
        public string password;
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<LoginModel> _logger;


        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

  

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

           
        public void OnPost()
        {
            username = Request.Form["username"];
            password = Request.Form["password"];
            if (DatabaseOperations.LogUserIn(username, password)){
                HttpContext.Session.SetString("username", username);
                User user = DatabaseOperations.selectUserByEmail(username);
                HttpContext.Session.SetComplexObject<User>("user", user);
                Response.Redirect("/Menu");
            }
            else
            {
                //(Encoding.ASCII.GetBytes("Error: Invalid Username or Password"));
                HttpContext.Session.SetString("errMessage", "Invalid Username or Password");
                Response.Redirect("/Login");
               
            }


        }

    }
}
