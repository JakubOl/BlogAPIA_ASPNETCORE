using AutoMapper;
using BlogAPIModels;
using BlogAPIModels.DtoModels;
using BlogAPIModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIRepositories
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<PostDto, Post>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();
        }
    }
}
