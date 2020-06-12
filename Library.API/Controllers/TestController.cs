using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        [HttpGet]
        [Route("learnIndexAndRange")]
        public IActionResult LearnIndexAndRange()
        {
            string[] words = new string[]
            {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };              // 9 (or words.Length) ^0
            Console.WriteLine($"The last word is {words[^1]}");

            // 包含左边，不包含右边
            string[] quickBrownFox = words[0..^0];
            foreach (var word in quickBrownFox)
                Console.Write($"< {word} >");
            Console.WriteLine();

            string[] lazyDog = words[1..4];
            foreach (var word in lazyDog)
                Console.Write($"< {word} >");
            Console.WriteLine();

            string[] allWords = words[..]; // contains "The" through "dog".
            string[] firstPhrase = words[..4]; // contains "The" through "fox"
            string[] lastPhrase = words[6..]; // contains "the, "lazy" and "dog"
            foreach (var word in allWords)
                Console.Write($"< {word} >");
            Console.WriteLine();
            foreach (var word in firstPhrase)
                Console.Write($"< {word} >");
            Console.WriteLine();
            foreach (var word in lastPhrase)
                Console.Write($"< {word} >");
            Console.WriteLine();

            Index the = ^3;
            Console.WriteLine(words[the]);
            Range phrase = 1..4;
            string[] text = words[phrase];
            foreach (var word in text)
                Console.Write($"< {word} >");
            Console.WriteLine();

            return Ok("ok");
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
