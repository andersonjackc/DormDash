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

namespace WebApplication1.Pages
{
    public class CreateAccountModel : PageModel
    {

        public string username;
        public string password;
        public string confirmPassword;
        public void OnGet()
        {

        }

        public void OnPost()
        {

            username = Request.Form["username"];
            password = Request.Form["password"];
            confirmPassword = Request.Form["confirmPassword"];


        }
    }



}
