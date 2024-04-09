using LibraryManagement.Data;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IssuedDetailsController : ControllerBase
    {
        private readonly LibraryManagementDbContext _dbContext;
        public IssuedDetailsController(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetIssuedDetails()
        {
            try
            {
                var issuedDetails = await _dbContext.IssuedDetails.Include(i => i.User).Include(i => i.Book).ToListAsync();
                return Ok(issuedDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetIssuedDetailById(int id)
        {
            try
            {
                var issuedDetail = await _dbContext.IssuedDetails.Include(i => i.User).Include(i => i.Book).Where(i => i.IssuedId == id).ToListAsync();
                if (issuedDetail == null)
                    return NotFound($"Issued detail with ID {id} not found");
                return Ok(issuedDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddIssuedDetail([FromBody] IssuedDetails issuedDetail)
        {
            try
            {
                _dbContext.IssuedDetails.Add(issuedDetail);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetIssuedDetailById), new { id = issuedDetail.IssuedId }, issuedDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIssuedDetail(int id, [FromBody] IssuedDetails issuedDetail)
        {
            try
            {
                if (id != issuedDetail.IssuedId)
                    return BadRequest("IssuedId mismatch");
                IssuedDetails updateIssued = _dbContext.IssuedDetails.Find(id);
                if (updateIssued == null)
                    return NotFound("Issued Details not found");
                _dbContext.Entry(updateIssued).CurrentValues.SetValues(issuedDetail);
                await _dbContext.SaveChangesAsync();
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIssuedDetail(int id)
        {
            try
            {
                var issuedDetail = await _dbContext.IssuedDetails.FindAsync(id);
                if (issuedDetail == null)
                    return NotFound($"Issued detail with ID {id} not found");

                _dbContext.IssuedDetails.Remove(issuedDetail);
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
