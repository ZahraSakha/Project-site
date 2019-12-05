using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SakhaSite.Models.DataModels;
using SakhaSite.Models.ViewModels;

namespace SakhaSite.Repository
{
    public interface IProductsRepository
    {
        Task<bool> DeleteAsync(Guid id);
        Task<List<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(Guid id);
        Task<Product> InsertAsync(ProductViewModel item);
        Task<Product> UpdateAsync(ProductViewModel item);
    }
}
