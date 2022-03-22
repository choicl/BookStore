﻿using System;
using System.Collections.Generic;

namespace BookStore.API.Data
{
    public partial class BookAuthor
    {
        public int? BookId { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author? Author { get; set; }
        public virtual Book? Book { get; set; }
    }
}
