using asp02di1_sk2.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asp02di1_sk2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ValueStorage _vs;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ValueStorage vs, ILogger<ValuesController> logger)
        {
            _vs = vs;
            _logger = logger;
            _logger.LogInformation("Creating Controller");
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<int> Get()
        {
            _logger.LogInformation($"{nameof(Get)}");
            return _vs.List();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            _logger.LogInformation($"{nameof(Get)} with id={id}");
            try 
            {
                var item = _vs.GetItem(id);
                return Ok(item);
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] int value)
        {
            _logger.LogInformation($"{nameof(Post)} with value={value}");
            _vs.AddItem(value);
            return Created();        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] int value)
        {
            _logger.LogInformation($"{nameof(Put)} with id={id} and value={value}");
            try 
            {
                _vs.ReplaceItem(id, value);
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"{nameof(Delete)} with id={id}");
            try 
            {
                _vs.RemoveItem(id);
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }

    // CRUD operations
    // Create - POST
    // Read, List - GET
    // Update - PUT, PATCH
    // Delete - DELETE
}
