using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class MenuItem
    {
        public MenuItem(int id, String name, String description, String imagePath, double price, Boolean available)
        {
            this.Name = name;
            this.price = price;
            this.description = description;
            this.id = id;
            this.imagePath = imagePath;
            this.available = available;
        }

        public MenuItem(String name, double price)
        {
            this.Name = name;
            this.price = price;
        }

        public String imagePath { get; set; }
        public String Name { get; set; }
        public String description { get; set; }
        public double price { get; set; }
        public int id { get; set; }

        public Boolean available { get; set; }
    }
}
