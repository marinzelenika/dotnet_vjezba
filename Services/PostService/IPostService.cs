using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_vjezba.Dtos.Post;
using dotnet_vjezba.Models;

namespace dotnet_vjezba.Services.PostService
{
    public interface IPostService
    {
        Task<ServiceResponse<List<GetPostDto>>> GetAllPostsByUser(int userId);
        Task<ServiceResponse<List<GetPostDto>>> GetAllPosts();
        Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost);
    }
}