using SakhaSite.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SakhaSite.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public bool IsAavailable { get; set; }
        public int CountOfAavailable { get; set; }
        public string ImageAddress { get; set; }
        public List<ProductFeature> ProductFeatures { get; set; }
    }
}
