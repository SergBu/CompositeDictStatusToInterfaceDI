using DictStatusToInterfaceDI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.CompositeDictionary;

namespace Templates.Controllers
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
        private readonly ICalculateTerminalTimeslotVehicleStatusService _calculateTerminalTimeslotVehicleStatus;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ICalculateTerminalTimeslotVehicleStatusService calculateTerminalTimeslotVehicleStatus)
        {
            _logger = logger;
            _calculateTerminalTimeslotVehicleStatus = calculateTerminalTimeslotVehicleStatus;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var terminalTimeslotVehicleWithStatuses = _calculateTerminalTimeslotVehicleStatus.Calculate(
                new List<TimeslotVehicle>()
                {
                    new TimeslotVehicle() { TerminalTimeslotVehicleId = 1 }
                });

            var terminalTimeslotVehicleWithStatusesDictionary = terminalTimeslotVehicleWithStatuses.ToDictionary(x => x.TerminalTimeslotVehicleId);

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
