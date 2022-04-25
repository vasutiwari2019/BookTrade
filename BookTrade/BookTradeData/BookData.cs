using BookTrade.BookTradeData.Interfaces;
using BookTrade.Data;
using BookTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTrade.BookTradeData
{
    // BookData class for handling Book related operations
    public class BookData:IBookData
    {
        #region Global variables
        private readonly BookTradeContext _bookTradeContext;
        #endregion

        #region Constructor
        public BookData(BookTradeContext bookTradeContext)
        {
            _bookTradeContext = bookTradeContext;
        }
        #endregion

        #region Public Methods

        // Method for adding a new book
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

        // Method for deleting a book
        public void DeleteBook(Book book)
        {
            _bookTradeContext.Remove(book);
            _bookTradeContext.SaveChanges();
        }

        // Method for editing a book
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
                existingBook.ImageUrl = book.ImageUrl;
                _bookTradeContext.Books.Update(existingBook);
                _bookTradeContext.SaveChanges();
            }
            return book;
        }

        // Method for getting all books
        public List<Book> GetBooks()
        {
            return _bookTradeContext.Books.ToList();
        }

        // Method for getting specified book
        public Book GetBooks(Guid id)
        {
            return _bookTradeContext.Books.Find(id);
        }

        // Method for getting all books by a specified user
        public List<Book> GetAllBooksByUser(Guid id)
        {
            return _bookTradeContext.Books.Where(x=>x.CreatedByUserId == id && !x.IsTraded).ToList();
        }

        // Method for book search using title, author and genre
        public List<Book> FindBook(FindBook book)
        {
            if (!string.IsNullOrEmpty(book.Title) && !string.IsNullOrEmpty(book.Author) && !string.IsNullOrEmpty(book.Genre))
            {
                return _bookTradeContext.Books.Where(x => x.Title.Contains(book.Title) && x.Author.Contains(book.Author) && x.Genre.Contains(book.Genre)).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Title) && !string.IsNullOrEmpty(book.Author))
            {
                return _bookTradeContext.Books.Where(x => x.Title.Contains(book.Title) && x.Author.Contains(book.Author)).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Title) && !string.IsNullOrEmpty(book.Genre))
            {
                return _bookTradeContext.Books.Where(x => x.Title.Contains(book.Title) && x.Genre.Contains(book.Genre)).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Author) && !string.IsNullOrEmpty(book.Genre))
            {
                return _bookTradeContext.Books.Where(x => x.Author.Contains(book.Author) && x.Genre.Contains(book.Genre)).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Title))
            {
                return _bookTradeContext.Books.Where(x => x.Title.Contains(book.Title)).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Author))
            {
                return _bookTradeContext.Books.Where(x => x.Author.Contains(book.Author)).ToList();
            }
            else if (!string.IsNullOrEmpty(book.Genre))
            {
                return _bookTradeContext.Books.Where(x => x.Genre.Contains(book.Genre)).ToList();
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
