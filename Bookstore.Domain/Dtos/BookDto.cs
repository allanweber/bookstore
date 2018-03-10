﻿using Bookstore.Framework.Dtos;

namespace Bookstore.Domain.Dtos
{
    public class BookDto: IDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Year { get; set; }
    }
}
