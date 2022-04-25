using BookTrade.Models;
using System;
using System.Collections.Generic;

namespace BookTrade.BookTradeData.Interfaces
{
    // BookData Interface
    public interface IBookData
    {
        #region Methods
        List<Book> GetBooks();

        Book GetBooks(Guid id);

        Book AddBook(Book book, Guid id);

        void DeleteBook(Book book);

        Book EditBook(Book book);

        List<Book> GetAllBooksByUser(Guid id);

        List<Book> FindBook(FindBook book);
        #endregion
    }
}
