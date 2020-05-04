using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestBenches.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        const int NUMBER_OF_PERSON= 1000000;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMapper mapper)
        {
            _logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("manual")]
        public IEnumerable<PersonDto> Get()
        {
            Stopwatch stopwatch = new Stopwatch();
            var mockPerson = new MockPerson(NUMBER_OF_PERSON);

            stopwatch.Start();
            var dtos = mockPerson.People.Select(x => new PersonDto()
            {
                Age = (int)(DateTime.Now.Subtract(x.BirthDate ?? DateTime.Now).TotalDays / 365),
                CityName=x.City.Name,
                FirstName=x.Name,
                LastAddress=x.Addresses.LastOrDefault().Value,
                FullName=$"{x.Name} {x.Family}",
                LastName=x.Family,
                PersonId=x.Id
            });
            Console.WriteLine($"manual: {stopwatch.ElapsedTicks} ticks | {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Stop();
            return dtos;
        }

        [HttpGet("autoMapper")]
        public IEnumerable<PersonDto> Get2()
        {
            Stopwatch stopwatch = new Stopwatch();
            var mockPerson = new MockPerson(NUMBER_OF_PERSON);

            

            stopwatch.Start();
            var dtos = _mapper.Map<List<PersonDto>>(mockPerson.People);
            Console.WriteLine($"automapper: {stopwatch.ElapsedTicks} ticks | {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Stop();
            return dtos;
        }
    }
}
