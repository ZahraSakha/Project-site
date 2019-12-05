using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhaSite.Models.DataModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsAavailable { get; set; }
        public int CountOfAavailable { get; set; }
        public string ImageAddress { get; set; }
        public List<ProductFeature> ProductFeatures { get; set; }
    }
}
