﻿using System;

namespace dotnet_vjezba.Dtos.Post
{
    public class AddPostDto
    {
        public string title { get; set; }
        public string body { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        //public Models.User User { get; set; }
    }
}