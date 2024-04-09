using LibraryManagement.Data;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryManagementDbContext _dbContext;
        public BookController(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books = await _dbContext.Books.ToListAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBookByISBN(int isbn)
        {
            try
            {
                var book = await _dbContext.Books.FindAsync(isbn);
                if (book == null)
                    return NotFound($"Book with ISBN {isbn} not found");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            try
            {
                _dbContext.Books.Add(book);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBookByISBN), new { isbn = book.ISBN }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(int isbn, [FromBody] Book book)
        {
            try
            {
                if (isbn != book.ISBN)
                    return BadRequest("ISBN mismatch");
                Book updateBook= _dbContext.Books.Find(isbn);
                if (updateBook == null)
                    return NotFound("Book not found");
                _dbContext.Entry(updateBook).CurrentValues.SetValues(book);
                await _dbContext.SaveChangesAsync();
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int isbn)
        {
            try
            {
                var book = await _dbContext.Books.FindAsync(isbn);
                if (book == null)
                    return NotFound($"Book with ISBN {isbn} not found");

                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
