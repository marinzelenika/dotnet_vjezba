using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_vjezba.Data;
using dotnet_vjezba.Dtos.Post;
using dotnet_vjezba.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_vjezba.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PostService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPostsByUser(int userId)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            List<Post> dbPosts = await _context.Posts.Where(p => p.User.Id == userId).ToListAsync();
            serviceResponse.Data = (dbPosts.Select(p => _mapper.Map<GetPostDto>(p))).ToList();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPosts()
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            List<Post> dbPosts = await _context.Posts.ToListAsync();
            serviceResponse.Data = (dbPosts.Select(p => _mapper.Map<GetPostDto>(p))).ToList();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            Post post =  _mapper.Map<Post>(newPost);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Posts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            return serviceResponse;
        }
        
    }
}