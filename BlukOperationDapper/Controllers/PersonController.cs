using BlukOperationDapper.Model;
using BlukOperationDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlukOperationDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("bulk-insert")]
        public async Task<IActionResult> BulkInsert([FromBody] List<Person> people)
        {
            await _personService.BulkInsertAsync(people);
            return Ok("Bulk insert successful");
        }

        [HttpPut("bulk-update")]
        public async Task<IActionResult> BulkUpdate([FromBody] List<Person> people)
        {
            await _personService.BulkUpdateAsync(people);
            return Ok("Bulk update successful");
        }

        [HttpDelete("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
        {
            await _personService.BulkDeleteAsync(ids);
            return Ok("Bulk delete successful");
        }
    }
}
