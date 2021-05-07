using AutoMapper;
using dotnet_vjezba.Dtos.Character;
using dotnet_vjezba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_vjezba.Dtos.Post;

namespace dotnet_vjezba
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Post, GetPostDto>();
            CreateMap<AddPostDto, Post>();
        }
    }
}
