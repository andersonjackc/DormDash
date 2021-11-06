using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Pages
{
    public class CreateAccountModel : PageModel
    {

        public string username;
        public string password;
        public string confirmPassword;
        public int ycpid;
        public UserType userType;
        public void OnGet()
        {

        }

        public void OnPost()
        {

            username = Request.Form["username"];
            password = Request.Form["password"];
            confirmPassword = Request.Form["confirmPassword"];

            if (password != confirmPassword)
            {
                HttpContext.Session.SetString("errMessage", "Passwords do not match");
                Response.Redirect("/CreateAccount");
            }
            else
            {
                ycpid = Int32.Parse(Request.Form["ycpid"]);
                userType = Request.Form["usertype"] == "Customer" ? UserType.Customer : UserType.Employee;
            
                User user = new User(ycpid, userType, 20, 20, username, password);

                DatabaseOperations.insertUser(user);
                HttpContext.Session.SetString("username", username);
                Response.Redirect("/Home");
                
            }
            
            


        }
    }



}
