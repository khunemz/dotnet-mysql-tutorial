using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace example_web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

       
        [HttpGet("GetBlogs")]
        public async Task<IActionResult> GetBlogs()
        {
            await using var connection = new MySqlConnection("server=localhost;user=khunemz;password=password;database=example");
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Blog>("SELECT * FROM blog;", null, commandType: CommandType.Text);
            await connection.CloseAsync();

            return Ok(result);
        }

    }

    public class Blog
    {
        public string blog_title { get; set; }
        public string blog_description { get; set; }
    }
}
