using Class03.Homework.WebApiApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Class03.Homework.WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet] // api/books
        public IActionResult GetAllBooks()
        {
            try
            {
                var response = new
                {
                    Message = "Books retrieved successfully",
                    Data = StaticDb.Books
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving books.");
            }
        }

        [HttpGet("singleBook")] // api/books/singleBook?index=0
        public IActionResult OneBookResult([FromQuery] int? index)
        {
            try
            {
                if (index < 0 || index >= StaticDb.Books.Count)
                {
                    return BadRequest("Index has invalid value.");
                }
                if (index == null)
                {
                    return BadRequest("Index is required.");
                }
                var book = StaticDb.Books[index.Value];
                return StatusCode(StatusCodes.Status200OK, book);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving book.");
            }
        }

        [HttpGet("filter")] // api/books/filter?author=AuthorName&title=BookTitle
        public ActionResult<List<Book>> GetBookByAuthorAndTitle([FromQuery] string author, [FromQuery] string title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return Ok(new { Message = "Here is a list of all available books", Data = StaticDb.Books });
                }

                List<Book> books = StaticDb.Books
                    .Where(x => (string.IsNullOrEmpty(author) || x.Author == author)
                                || (string.IsNullOrEmpty(title) || x.Title == title))
                    .ToList();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            try
            {
                if (newBook == null || string.IsNullOrEmpty(newBook.Title) || string.IsNullOrEmpty(newBook.Author))
                {
                    return BadRequest("Invalid book data.");
                }
                StaticDb.Books.Add(newBook);
                var response = new
                {
                    Message = "Book added successfully",
                    Data = newBook
                };
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding book.");
            }
        }

        [HttpPost("multipleBooks")]
        public IActionResult AddMultipleBooks([FromBody] List<Book> newBooks)
        {
            try
            {
                if (newBooks.Count == 0 || newBooks == null)
                {
                    return BadRequest("Invalid book data.");
                }

                StaticDb.Books.AddRange(newBooks);
                var titles = newBooks.Select(b => b.Title).ToList();
                var response = new
                {
                    Message = "Books added successfully",
                    Data = titles
                };
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding books.");
            }
        }
    }
}