using System;
using System.Collections.Generic;
using System.Text;

namespace MexicanFood.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
    }
}
