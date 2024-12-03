using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _queryService;

        public QueryController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuery(QueryDTO queryDto)
        {
            if (queryDto == null) {
                return BadRequest("Query data is null");
            }
              

            bool isCreated = await _queryService.CreateQuery(queryDto);

            if (isCreated)
                return Ok("Query created successfully");

            return StatusCode(500, "A problem occurred while creating the query.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuery(int id)
        {
            try
            {
                var query = await _queryService.GetQuery(id);
                if (query == null)
                    return NotFound($"Query with ID {id} not found.");

                return Ok(query);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "A problem occurred while retrieving the query.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuery()
        {
            try
            {
                var query = await _queryService.GetAllQuery();
                if (query == null)
                    return NotFound($"Queries not found.");

                return Ok(query);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "A problem occurred while retrieving the query.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditQuery(int id, QueryDTO queryDto)
        {
            if (queryDto == null)
                return BadRequest("Query data is null");

            try
            {
                bool isUpdated = await _queryService.EditQuery(id, queryDto);
                if (isUpdated)
                    return NoContent();

                return NotFound($"Query with ID {id} not found.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "A problem occurred while updating the query.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuery(int id)
        {
            try
            {
                bool isDeleted = await _queryService.DeleteQuery(id);
                if (isDeleted)
                    return Ok("Deleted Succesfully");

                return NotFound($"Query with ID {id} not found.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "A problem occurred while deleting the query.");
            }
        }
    }
}
