using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Order
    {

        public enum status
        {
            waiting,
            acknowledged,
            done,
            pickupReady,
            delivering,
            complete
        }

        public Order(int id, int userId, DateTime orderTime, double runningTotal, Destination  dest , List<MenuItem> orderedItems, Boolean claimed)
        {
            this.id = id;
            this.orderTime = DateTime.Now;
            this.runningTotal = 0;
            this.orderDestination = dest;
            this.orderedItems = orderedItems;
            this.userId = userId;
            this.claimed = claimed;
        }

        public Destination orderDestination { get; set; }
        public int id { get; set; }
        public DateTime orderTime { get; set; }
        public double runningTotal { get; set; }
        public List<MenuItem> orderedItems { get; set; }
        public status Status { get; set; }

        public Boolean claimed { get; set; }

        public int userId { get; set; }

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
