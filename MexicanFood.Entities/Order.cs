using System;
using System.Collections.Generic;

namespace MexicanFood.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime PickUpDateAndTime { get; set; }
        public DateTime OrderedDateAndTime { get; set; }
        public string Comment { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}