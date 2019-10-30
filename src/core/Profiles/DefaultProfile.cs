using System;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Models;
using CaveCore.SchemaModels;

namespace CaveCore.Profiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(dto => dto.Created, opt => opt.AddTransform(s => s == default(DateTime) ? DateTime.UtcNow : s));
            CreateMap<User, UserDto>();
            CreateMap<CategoryDto, Category>()
                .ForMember(dto => dto.Created, opt => opt.AddTransform(s => s == default(DateTime) ? DateTime.UtcNow : s));
            CreateMap<ICategory, CategoryDto>();

            CreateMap<PostDto, Post>()
                .ForMember(dto => dto.Created, opt => opt.AddTransform(s => s == default(DateTime) ? DateTime.UtcNow : s));
            CreateMap<IPost, PostDto>();
            CreateMap<IUserValidateResult, UserValidateResultDto>();
        }
    }
}
