using System;
using System.Linq;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Models;
using CaveCore.SchemaModels;
using CaveCore;

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
            CreateMap<IPost, PostDto>()
                .ForMember(dto => dto.UpVotes, opt => opt.MapFrom(s => s.Votes.Where( i => i.VoteType == (int)VoteType.UpVote).Count()))
                .ForMember(dto => dto.DownVotes, opt => opt.MapFrom(s => s.Votes.Where( i => i.VoteType == (int)VoteType.DownVote).Count()))
                .ForMember(dto => dto.Comments, opt => opt.MapFrom(s => s.Comments));
            
            CreateMap<IUserValidateResult, UserValidateResultDto>();
            CreateMap<ValidateUserDto, UserDto>();
            CreateMap<IComment, CommentDto>();
            
        }
    }
}
