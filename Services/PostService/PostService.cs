using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_vjezba.Data;
using dotnet_vjezba.Dtos.Post;
using dotnet_vjezba.Models;
using Microsoft.AspNetCore.Http;
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
            post.createdAt = DateTime.Now;
            post.updatedAt = DateTime.Now;
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Posts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetPostDto>> GetPostById(int id)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            Post dbPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            serviceResponse.Data = _mapper.Map<GetPostDto>(dbPost);
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            try
            {
                Post post = await _context.Posts.FirstAsync(p => p.Id == id);
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

                serviceResponse.Data = (_context.Posts.Select(p => _mapper.Map<GetPostDto>(p))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            try
            {
                Post post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == updatedPost.Id);

                post.title = updatedPost.title;
                post.body = updatedPost.body;
                post.createdAt = updatedPost.createdAt;
                post.updatedAt = DateTime.Now;
                
                

                _context.Posts.Update(post);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetPostDto>(post);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        
    }
}