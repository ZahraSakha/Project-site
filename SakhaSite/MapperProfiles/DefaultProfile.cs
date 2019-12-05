using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SakhaSite.Models.DataModels;
using SakhaSite.Models.ViewModels;

namespace SakhaSite.MapperProfiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
