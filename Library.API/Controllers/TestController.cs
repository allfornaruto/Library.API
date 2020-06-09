using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private ILogger<TestController> Logger { get; }

        public TestController(ILogger<TestController> logger)
        {
            Logger = logger;
        }

        [HttpGet]
        [Route("okjson")]
        public IActionResult Okjson()
        {
            var obj = new
            {
                a=1,
                b=2
            };
            return Ok(obj);
        }

        [HttpGet]
        [Route("okstring")]
        public IActionResult Okstring()
        {
            return Ok("/test/index");
        }

        [HttpPost]
        [Route("getqueryandbody")]
        public IActionResult GetQueryAndBody([FromBody] TestBody body, [FromQuery] int a, [FromQuery] string b)
        {
            // POST或PUT可以不写
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = new
            {
                a,
                b,
                body
            };
            return Ok(result);
        }
    }

    public class TestBody
    { 
        [Required]
        public int? C { get; set; }
        [NoSpace]
        public string D { get; set; }
        public string E { get; set; }
    }

    public class NoSpaceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value.ToString().Contains(" "))
            {
                return new ValidationResult("字符串不可包含空格");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
