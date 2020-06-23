using AutoMapper.Mappers;
using GraphQL.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        [Route("learnCollectionsList")]
        public IActionResult LearnCollections() {
            List<Student> studentList = new List<Student>()
            {
                new Student(){ age = 10, name = "Tom" },
                new Student(){ age = 11, name = "Simon" }
            };
            var alice = new Student() { age = 12, name = "Alice" };
            studentList.Add(alice);
            //studentList.Remove(alice);

            //var findAlice = studentList.Find(item => item.name == "Alice");
            //studentList.Remove(findAlice);

            studentList.Sort((a, b) => b.age - a.age);

            foreach (var student in studentList)
            {
                Console.WriteLine($"student = {student}"); 
            }

            return Ok();
        }

        [HttpGet]
        [Route("learnDictionary")]
        public IActionResult LearnDictionary() {
            var studentMap = new Dictionary<string, Student>()
            {
                ["Tom"] = new Student() { age = 10, name = "Tom" },
                ["Sandy"] = new Student() { age = 11, name = "Sandy" }
            };

            Console.WriteLine($"studentMap[\"Tom\"] = {studentMap["Tom"]}");

            return Ok();
        }

        [HttpGet]
        [Route("learnLINQ")]
        public IActionResult LearnLINQ() {
            List<Student> studentList = new List<Student>()
            {
                new Student(){ age = 10, name = "Tom", city = "上海" },
                new Student(){ age = 11, name = "Simon", city = "北京" },
                new Student(){ age = 12, name = "Alice", city = "香港" }
            };

            IEnumerable<Student> students_1 = studentList.Where(item => item.name == "Tom");
            var students_2 = students_1.ToList()[0];
            Console.WriteLine($"students_2 = {students_2}");

            IEnumerable<Student> students_3 = studentList.Select(item => {
                item.name = "Alex";
                return item;
            });
            var students_4 = students_3.ToList()[0];
            Console.WriteLine($"students_4 = {students_4}");

            IEnumerable<(string name, string city)> tupleStudent = studentList.Select(item => {
                return (
                    name: item.name,
                    city: item.city
                );
            });
            var tupleStudentList = tupleStudent.ToList();
            Console.WriteLine($"tupleStudentList Select返回元组 name = {tupleStudentList[0].name} city = {tupleStudentList[0].city}  ");

            Console.WriteLine($"开始排序...");
            /// 升序
            //IEnumerable<Student> students_5 = studentList.OrderBy(item => item.age);
            /// 降序
            IEnumerable<Student> students_5 = studentList.OrderByDescending(item => item.age);
            var students_6 = students_5.ToList();
            foreach (var item in students_6)
            {
                Console.WriteLine($"student = {item}");
            }

            return Ok();
        }

        [HttpGet]
        [Route("learnDelegate")]
        public IActionResult LearnDelegate() {
            Thermostat thermostat = new Thermostat();
            Heater heater = new Heater(80);
            Cooler cooler = new Cooler(60);
            string temperature = "85";
            thermostat.OnTemperatureChange += heater.OnTemperatureChanged;
            thermostat.OnTemperatureChange += (newTemperature) =>
            {
                throw new InvalidOperationException();
            };
            thermostat.OnTemperatureChange += cooler.OnTemperatureChanged;
            thermostat.CurrentTemperature = int.Parse(temperature);
            return Ok();
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

    public class Student {
        public int age { get; set; }
        public string name { get; set; }
        public string city { get; set; }

        public override string ToString()
        {
            return $"姓名：{name}，年龄：{age}，城市：{city}";
        }
    }

    public class Cooler
    {
        public Cooler(float temperature)
        {
            Temperature = temperature;        
        }

        public float Temperature { get; set; }

        public void OnTemperatureChanged(float newTemperature)
        {
            if (newTemperature > Temperature)
            {
                Console.WriteLine("制冷 开启");
            }
            else
            {
                Console.WriteLine("制冷 关闭");
            }
        }
    }

    public class Heater
    {
        public Heater(float temperature)
        {
            Temperature = temperature;
        }
        public float Temperature { get; set; }

        public void OnTemperatureChanged(float newTemperature)
        {
            if (newTemperature < Temperature)
            {
                Console.WriteLine("制热 开启");
            }
            else
            {
                Console.WriteLine("制热 关闭");
            }
        }
    }
    public class Thermostat
    { 
        public Action<float> OnTemperatureChange { get; set; }
        public float CurrentTemperature {
            get 
            { 
                return _CurrentTemperature; 
            }
            set
            {
                //if (value != CurrentTemperature)
                //{
                //    _CurrentTemperature = value;
                //    OnTemperatureChange?.Invoke(value);
                //}

                if (value != CurrentTemperature)
                {
                    _CurrentTemperature = value;
                    Action<float> onTemperatureChange = OnTemperatureChange;
                    if (onTemperatureChange != null)
                    {
                        List<Exception> exceptionCollection = new List<Exception>();
                        var invocationList = onTemperatureChange.GetInvocationList();
                        foreach (Action<float> handler in invocationList)
                        {
                            try
                            {
                                handler(value);
                            }
                            catch (Exception exception)
                            {
                                exceptionCollection.Add(exception);
                            }
                        }
                        if (exceptionCollection.Count > 0)
                        {
                            throw new AggregateException("有一些异常", exceptionCollection);
                        }
                    }
                }
            }
        }
        private float _CurrentTemperature;
    }

    public class ThermostatEvent
    {
        public class TemperatureArgs: System.EventArgs
        {
            public TemperatureArgs(float newTemperature)
            {
                NewTemperature = newTemperature;
            }
            public float NewTemperature { get; set; }
        }

        public event EventHandler<TemperatureArgs> OnTemperatureChange = delegate { };


        public float CurrentTemperature
        {
            get
            {
                return _CurrentTemperature;
            }
            set
            {
                if (value != CurrentTemperature)
                {
                    _CurrentTemperature = value;
                    OnTemperatureChange?.Invoke(this, new TemperatureArgs(value));
                }
            }
        }

        private float _CurrentTemperature;
    }
}
