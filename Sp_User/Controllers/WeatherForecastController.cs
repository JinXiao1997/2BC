using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUserBLL;
using Microsoft.AspNetCore.Mvc;
namespace Sp_User.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        IMemberUserBLL db;
        public WeatherForecastController(IMemberUserBLL db) {
            this.db = db;
        }
        //tEST3ddddD
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {

            var model = await db.Login("123", "456");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
