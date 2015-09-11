using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public int Length { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
