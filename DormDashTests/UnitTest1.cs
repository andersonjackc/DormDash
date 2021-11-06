using NUnit.Framework;
using System;
using WebApplication1;

namespace DormDashTests
{
    public class Tests
    {

        User testUser2;

        [SetUp]
        public void Setup()
        {
            testUser2 = new User(5000, UserType.Customer, 120.0,123.0, "test1@ycp.edu", "password123");

            DatabaseOperations.insertUser(testUser2);
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine("hello0");
        }
    }
}