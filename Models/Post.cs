using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_vjezba.Models
{
    public class Post
    {
        [Key]
        public Guid Guid { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public User User { get; set; }
    }
}