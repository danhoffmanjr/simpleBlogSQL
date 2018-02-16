using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCore.Entities
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public Rating Rating { get; set; }
    }
}