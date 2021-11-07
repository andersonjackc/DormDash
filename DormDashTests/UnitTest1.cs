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
            System.Collections.Generic.List<MenuItem> menu_items = DatabaseOperations.selectMenuItems();

            Order order = new Order(67, 903242133, DateTime.Now, 100, new Destination(building.Kinsley, 000), menu_items,false);
            DatabaseOperations.insertUser(new User(903242133, UserType.Employee, 20.00, 20.00, "test@ycp.edu", "test123"));
            DatabaseOperations.updateUser(new User(903242133, UserType.Employee, 69.00, 21.00, "gggggggggggggg1@ycp.edu"));
            DatabaseOperations.insertOrder(order);
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine("hello0");
        }
    }
}