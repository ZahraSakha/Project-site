using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SakhaSite.Contexts;
using SakhaSite.Models.DataModels;
using SakhaSite.Models.ViewModels;

namespace SakhaSite.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private DataContext dataContext;

        public ProductsRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var list = await dataContext.Products
                .Select(i => new ProductViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    CountOfAavailable = i.CountOfAavailable,
                    ImageAddress = i.ImageAddress,
                    Price = i.Price,
                    ProductFeatures = i.ProductFeatures
                }).ToListAsync();
            return list;
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            var item = await dataContext.Products
                .Where(i => i.Id == id)
                .Select(i => new ProductViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    CountOfAavailable = i.CountOfAavailable,
                    ImageAddress = i.ImageAddress,
                    Price = i.Price,
                    ProductFeatures = i.ProductFeatures
                }).FirstOrDefaultAsync();
            return item;
        }
        //to do edit product 
        public async Task<Product> InsertAsync(ProductViewModel item)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Description = item.Description,
                Price = item.Price
            };
            dataContext.Products.Add(product);
            if ((await dataContext.SaveChangesAsync()) > 0)
                return product;
            else
                return null;
        }

        public async Task<Product> UpdateAsync(ProductViewModel item)
        {
            var product = await dataContext.Products
                .FirstOrDefaultAsync(i => i.Id == item.Id);
            if (product == null)
                return null;
            product.Name = item.Name;
            product.Price = item.Price;
            product.Description = item.Description;
            if ((await dataContext.SaveChangesAsync()) > 0)
                return product;
            else
                return null;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await dataContext.Products
                .FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
                return false;
            dataContext.Products.Remove(product);
            return (await dataContext.SaveChangesAsync()) > 0;
        }
    }


}
