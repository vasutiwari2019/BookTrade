﻿using BookTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTrade.Data
{
    public class BookTradeContext:DbContext
    {
        public BookTradeContext(DbContextOptions<BookTradeContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<TradeDetails> TradeDetails { get; set; }
    }
}
