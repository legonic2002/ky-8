﻿using System;
using System.Collections.Generic;

namespace Su23_Trial.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
    }
}