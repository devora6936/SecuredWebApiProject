using AutoMapper;
using Entities;
using DTOs;

namespace MyFirstApiSite
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.CategoryName)).ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<UserLoginDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
