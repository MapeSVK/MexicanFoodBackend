using System;
using System.Collections.Generic;
using System.Text;

namespace MexicanFood.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        //public int[] MealId { get; set; }
        public string MobileNumber { get; set; }
        public string CustomerName { get; set; }
        public double TotalPrice { get; set; }
        public DateTime PickUpDateAndTime { get; set; }
        public DateTime OrderedDateAndTime { get; set; }
        public string Comment { get; set; }
    }
}
