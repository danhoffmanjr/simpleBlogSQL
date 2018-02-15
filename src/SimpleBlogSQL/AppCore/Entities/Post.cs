using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set;}
        public string Permalink { get; set; }
        public string PostContent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingsCount { get; set; }
    }
}
