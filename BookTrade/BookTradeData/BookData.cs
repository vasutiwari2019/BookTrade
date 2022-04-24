using BookTrade.BookTradeData.Interfaces;
using BookTrade.Data;
using BookTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTrade.BookTradeData
{
    public class BookData:IBookData
    {
        private readonly BookTradeContext _bookTradeContext;

        public BookData(BookTradeContext bookTradeContext)
        {
            _bookTradeContext = bookTradeContext;
        }

        public Book AddBook(Book book, Guid id)
        {
            book.BookId = Guid.NewGuid();

            var userFound = _bookTradeContext.Users.Find(id);
            if (userFound != null)
            {
                book.CreatedByUserId = id;
                _bookTradeContext.Books.Add(book);
                _bookTradeContext.SaveChanges();

                return book;
            }
            else
            {
                return null;
            }
        }

        public void DeleteBook(Book book)
        {
            _bookTradeContext.Remove(book);
            _bookTradeContext.SaveChanges();
        }

        public Book EditBook(Book book)
        {
            var existingBook = _bookTradeContext.Books.Find(book.BookId);

            if(existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Condition = book.Condition;
                existingBook.Genre = book.Genre;
                existingBook.About = book.About;
                existingBook.CreatedByUserId= book.CreatedByUserId;
                existingBook.TradeType = book.TradeType;
                existingBook.IsTraded = book.IsTraded;
                existingBook.CreatedDate = DateTime.Now;
                _bookTradeContext.Books.Update(existingBook);
                _bookTradeContext.SaveChanges();
            }
            return book;
        }

        public List<Book> GetBooks()
        {
            return _bookTradeContext.Books.ToList();
        }

        public Book GetBooks(Guid id)
        {
            return _bookTradeContext.Books.Find(id);
        }

        public List<Book> GetAllBooksByUser(Guid id)
        {
            return _bookTradeContext.Books.Where(x=>x.CreatedByUserId == id && !x.IsTraded).ToList();
        }

        public List<Book> FindBook(FindBook book)
        {
            if (!string.IsNullOrEmpty(book.Title) && !string.IsNullOrEmpty(book.Author) && !string.IsNullOrEmpty(book.Genre))
            {
                return _bookTradeContext.Books.Where(x => x.Title == book.Title && x.Author == book.Author && x.Genre == book.Genre).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Title) && !string.IsNullOrEmpty(book.Author))
            {
                return _bookTradeContext.Books.Where(x => x.Title == book.Title && x.Author == book.Author).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Title) && !string.IsNullOrEmpty(book.Genre))
            {
                return _bookTradeContext.Books.Where(x => x.Title == book.Title && x.Genre == book.Genre).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Author) && !string.IsNullOrEmpty(book.Genre))
            {
                return _bookTradeContext.Books.Where(x => x.Author == book.Author && x.Genre == book.Genre).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Title))
            {
                return _bookTradeContext.Books.Where(x => x.Title == book.Title).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Author))
            {
                return _bookTradeContext.Books.Where(x => x.Author == book.Author).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Genre))
            {
                return _bookTradeContext.Books.Where(x => x.Genre == book.Genre).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
