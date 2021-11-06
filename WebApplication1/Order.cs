using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Order
    {
        private List<MenuItem> orderedItems; 

        public Order(int id, DateTime orderTime, double runningTotal, Destination  dest )
        {
            this.id = id;
            this.orderTime = DateTime.Now;
            this.runningTotal = 0;
            this.orderDestination = dest;
        }

        public Destination orderDestination { get; set; }
        public int id { get; set; }
        public DateTime orderTime { get; set; }
        public double runningTotal { get; set; }

        //adds the item to the order & updates the running total
        public void addToOrder(MenuItem newItem)
        {
            orderedItems.Add(newItem);
            this.runningTotal += newItem.price;
        }

        //adds the items to the order & updates the running total
        public void addToORder(List<MenuItem> newItems)
        {
            foreach(MenuItem item in newItems){
                this.runningTotal += item.price;
                orderedItems.Add(item);
            }
        }
        
    }
}
