using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp01api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiceController : ControllerBase
    {
        [HttpGet]
        public int GetNumber()
        {
            int val = Random.Shared.Next(0, 100);
            return val;
        }

        [HttpGet("range/{min}/{max}")]
        public ActionResult<int> GetNumberInRange(int min = 0, int max = 100)
        {
            int val = Random.Shared.Next(min, max);
            return Ok(val);
        }
        /*
        [HttpGet("range/{min}/{max}")]
        public int GetNumberInRange(int min = 0, int max = 100)
        {
            int val = Random.Shared.Next(min, max);
            return val;
        }
        */

        [HttpGet("bum")]
        public IActionResult GetErrors()
        {
            //return NotFound(); // 404
            return Unauthorized(); 
            return Forbid();
            return StatusCode(415);
            return Ok(); // 200
            return BadRequest();
            return NoContent(); 
            return RedirectToAction("range/10/10");
        }

        [HttpPost]
        public IActionResult SendData([FromBody] int value)
        {
            return Ok(value);
        }

        [HttpGet("/student")]
        public IActionResult GetStudent()
        {
            var st = new Student { Id = 1, Name = "Alfons Vomáčka"};
            return Ok(st);
        }
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
