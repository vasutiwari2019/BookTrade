using BookTrade.BookTradeData.Interfaces;
using BookTrade.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookTrade.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookData _bookData;

        public BookController(IBookData bookData)
        {
            _bookData = bookData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetBooks()
        {
            return Ok(_bookData.GetBooks());
        }

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
    }
}
