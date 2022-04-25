using BookTrade.BookTradeData.Interfaces;
using BookTrade.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookTrade.Controllers
{
    // BookController for handling Book API calls
    [ApiController]
    public class BookController : ControllerBase
    {
        #region Global Variables
        private readonly IBookData _bookData;
        #endregion

        #region Constructor
        public BookController(IBookData bookData)
        {
            _bookData = bookData;
        }
        #endregion

        #region Public Methods

        // API for getting all books
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetBooks()
        {
            return Ok(_bookData.GetBooks());
        }

        // API for getting a specified book
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetBooks(Guid id)
        {
            var book = _bookData.GetBooks(id);
            if (book != null)
                return Ok(book);
            else
                return NotFound($"Book with Id:{id} was not found");
        }

        // API for creating a book for a specified user
        [HttpPost]
        [Route("api/[controller]/{id}")]
        public IActionResult AddBooks(Book book, Guid id)
        {
            var bookCreated = _bookData.AddBook(book, id);
            if (bookCreated != null)
                return Ok(bookCreated);
            else
                return BadRequest("Book Not Created");
        }

        // API for getting all books
        [HttpPost]
        [Route("api/[controller]/getallbooksbyuser/{id}")]
        public IActionResult GetAllBooksByUser(Guid id)
        {
            var books = _bookData.GetAllBooksByUser(id);

            if (books != null)
                return Ok(books);
            else
                return NotFound("$Books not found for user");
        }

        // API for deleting a specified book
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteBook(Guid id)
        {
            var existingBook = _bookData.GetBooks(id);

            if (existingBook != null)
            {
                _bookData.DeleteBook(existingBook);
                return Ok();
            }

            return NotFound($"Book with Id:{id} was not found");
        }

        // API for editing a specified book
        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditBook(Guid id, Book book)
        {
            var existingBook = _bookData.GetBooks(id);

            if (existingBook != null)
            {
                book.BookId = existingBook.BookId;
                _bookData.EditBook(book);
                return Ok(book);
            }

            return NotFound($"Book with Id:{id} was not found");
        }

        // API for finding book by title, author and genre
        [HttpPost]
        [Route("api/[controller]/findbooks")]
        public IActionResult FindBooks(FindBook book)
        {
            var booksFound = _bookData.FindBook(book);

            if (booksFound != null)
                return Ok(booksFound);
            else
                return NotFound("Books not found");
        }
        #endregion
    }
}
