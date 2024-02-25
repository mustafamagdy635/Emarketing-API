using AutoMapper;
using Emarketing_API.Models.DTO;
using Emarketing_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.DataAccess
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Products, ShowProductWithCategoryAndBrandsVM>()
                .ForMember(des=>des.categoryName,src=>src.MapFrom(src=>src.categories.Name))
                .ForMember(des=>des.brand_Id,src=>src.MapFrom(src=>src.brand_Id))
                .ForMember(des=>des.Category_Id,src=>src.MapFrom(src=>src.Category_Id))
                .ForMember(des=>des.Id,src=>src.MapFrom(src=>src.Id)).ReverseMap();

            CreateMap<Stocks, StockWithProductVM>().ForMember(des=>des.Name,src=>src.MapFrom(src=>src.products.Name)).ReverseMap();
            //CreateMap<ApplicationUser, AccountDTO>()
            //    .ForMember(des=>des.UserName,src=>src.MapFrom(src=>src.UserName))
            //    .ForMember(des=>des.Email,src=>src.MapFrom(src=>src.Email))
            //    .ForMember(des=>des.Password,src=>src.MapFrom(src=>src.PasswordHash))
                
            //    .ReverseMap();
        }

    }
}
