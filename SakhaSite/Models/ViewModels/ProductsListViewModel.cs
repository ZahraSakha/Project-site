using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhaSite.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
