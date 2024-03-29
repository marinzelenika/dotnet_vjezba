﻿using System;

namespace dotnet_vjezba.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public User User { get; set; }
    }
}