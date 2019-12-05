using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SakhaSite.Contexts;
using SakhaSite.Models.ViewModels;
using SakhaSite.Repository;

namespace SakhaSite.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository productsRepository;

        private DataContext _context;
        private IMapper _mapper;

        public ProductsController(IProductsRepository productsRepository, DataContext context, IMapper mapper)
        {
            this.productsRepository = productsRepository;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult List(int page = 1)
        {
            const int itemsPerPage = 5;
            if (page < 1) page = 1;
            var result = _context.Products
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(i => _mapper.Map<ProductViewModel>(i))
                .ToList();
            var maxPages = (int)Math.Ceiling((double)_context.Products.Count() / itemsPerPage);

            return View(new ProductsListViewModel()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                PagesCount = maxPages,

                Products = result,
            });
            //var list = await productsRepository.GetAllAsync();
            //return View(list);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var item = await productsRepository.GetByIdAsync(id);
            return View(item);
        }
    }
}