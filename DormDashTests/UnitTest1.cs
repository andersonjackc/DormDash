using NUnit.Framework;
using System;
using WebApplication1;

namespace DormDashTests
{
    public class Tests
    {

        User testUser1;

        [SetUp]
        public void Setup()
        {
            testUser1 = new User(1, UserType.Customer, "test@ycp.edu", "password123");
            
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine(testUser1.salt);
            Console.WriteLine(testUser1.hashPWD);
        }
    }
}