using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public decimal Score { get; set; }
    }
}
