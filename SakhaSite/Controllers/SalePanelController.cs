using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SakhaSite.Models.ViewModels;
using SakhaSite.Repository;
using SakhaSite.Constants;


namespace SakhaSite.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class SalePanelController : Controller
    {
        private IProductsRepository productsRepository;

        public SalePanelController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var list = await productsRepository.GetAllAsync();
            return View(list);
        }

        public IActionResult Insert()
        {
            var model = new ProductViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(ProductViewModel item, string returnUrl)
        {
            var Item = await productsRepository.InsertAsync(item);

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/SalePanel/Index";
            return Redirect(returnUrl);
            //return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var product = await productsRepository.GetByIdAsync(Id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel item)
        {
            await productsRepository.UpdateAsync(item);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await productsRepository.DeleteAsync(Id);
            if (result)
                return Redirect("/");
            return Redirect("/");
        }
    }
}