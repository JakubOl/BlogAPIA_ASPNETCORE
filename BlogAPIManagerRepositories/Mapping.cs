using AutoMapper;
using BlogAPIModels;
using BlogAPIModels.DtoModels;
using BlogAPIModels.Entities;

namespace BlogAPIRepositories
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<PostDto, Post>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<RegisterUserDto, LoginUserDto>();
        }
    }
}
