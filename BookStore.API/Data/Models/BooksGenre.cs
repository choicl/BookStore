using System;
using System.Collections.Generic;

namespace BookStore.API.Data
{
    public partial class BooksGenre
    {
        public int? BookId { get; set; }
        public int? GenreId { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Genre? Genre { get; set; }
    }
}
