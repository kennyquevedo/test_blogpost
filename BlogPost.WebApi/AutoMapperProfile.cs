using AutoMapper;
using BlogPost.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = BlogPost.BLogic.Dto;

namespace BlogPost.WebApi
{
    /// <summary>
    /// Main profile to map entities.
    /// </summary>
    public class AutoMapperProfile:Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Post, Dto.Post>().ReverseMap();
            CreateMap<PostStatus, Dto.PostStatus>().ReverseMap();

        }
    }
}
