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

        public User( int id, UserType userType, String email, String plainTextPassword)
        {
            this.id = id;
            this.userType = userType;
            this.email = email;
            //hash & salt passowrd
            this.salt = generateSalt(70);
            this.hashPWD = HashPassword(plainTextPassword, this.salt, 10101, 70);
            
        }

        public int id { get; set; }
        public UserType userType { get; set; }
        public String email { get; set; }
      
        public String hashPWD { get; set; }

        public String salt { get; set; }

        public String generateSalt(int numBytes)
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
