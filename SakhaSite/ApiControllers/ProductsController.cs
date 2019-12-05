using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SakhaSite.Models.ViewModels;
using SakhaSite.Repository;

namespace SakhaSite4.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductsRepository productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var list = await productsRepository.GetAllAsync();
            return Json(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await productsRepository.GetByIdAsync(id);
            return Json(item);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(ProductViewModel item)
        {
            var result = await productsRepository.InsertAsync(item);
            return Json(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put(ProductViewModel item)
        {
            var result = await productsRepository.UpdateAsync(item);
            return Json(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productsRepository.DeleteAsync(id);
            return Json(result);
        }
    }
}
