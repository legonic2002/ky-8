﻿using System;
using System.Collections.Generic;

namespace Su23_Trial.Models
{
    public partial class Producer
    {
        public Producer()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
    }
}