using Feel2Scale.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAIApi;
namespace Feel2Scale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScaleController : ControllerBase
    {
        private readonly Data.AppDbContext _context;
        private readonly OpenAIService _openAIService;
        public ScaleController(Data.AppDbContext context, OpenAIService AiService)
        {
            _context = context;
            _openAIService = AiService;
        }

        // GET: api/scale
        [HttpGet]
        public IActionResult GetScales()
        {
            var scales = _context.ScaleData.ToList();
            return Ok(scales);
        }
        // GET: api/scale/{id}
        [HttpGet("{id}")]
        public IActionResult GetScale(int id)
        {
            var scale = _context.ScaleData.Find(id);
            if (scale == null)
            {
                return NotFound();
            }
            return Ok(scale);
        }
        // POST: api/scale
        [HttpPost]
        public IActionResult CreateScale([FromBody] UserMessage req)
        {
            if (req == null)
            {
                return BadRequest("Scale data is null.");
            }
            if (string.IsNullOrWhiteSpace(req.Message))
            {
                return BadRequest("Message cannot be empty.");
            }
            var scaleData = _openAIService.GetScaleAttributes(req.Message);
            if (scaleData == null)
            {
                return BadRequest("Failed to parse scale data from OpenAI response.");
            }
            _context.ScaleData.Add(scaleData);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetScale), new { id = scaleData.Id }, scaleData);
        }
    }
}
