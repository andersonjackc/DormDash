using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace WebApplication1
{

    public enum UserType
    {
        SpartsAdmin,
        Employee,
        Customer
    }

    public class User
    {

        public User(int id, UserType userType, double flexBalance, double diningBalance, String email, String plainTextPassword)
        {
            this.id = id;
            this.userType = userType;
            this.email = email;
            //hash & salt passowrd
            this.salt = generateSalt(70);
            this.hashPWD = HashPassword(plainTextPassword, this.salt, 10101, 70);
            this.flexBalance = flexBalance;
            this.diningBalance = diningBalance;

        }

        public double diningBalance { get; set; }
        public double flexBalance { get; set; }

        public int id { get; set; }
        public UserType userType { get; set; }
        public string email { get; set; }

        public string hashPWD { get; set; }

        public string salt { get; set; }

        public string generateSalt(int numBytes)
        {

            var saltBytes = new byte[numBytes];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes); ;
        }

        public static string HashPassword(string password, string salt, int nIterations, int nHash)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
            }
        }
    }
}
