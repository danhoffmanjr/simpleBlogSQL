using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Interfaces
{
    public interface IPostRepository
    {
        List<Post> GetAll();
        Post GetById(int id);
        Post GetByPermalink(string permalink);
        void Create(Post post);
        void Delete(int id);
        void Update(Post post);
        void CreateRating(Rating rating);

    }
}