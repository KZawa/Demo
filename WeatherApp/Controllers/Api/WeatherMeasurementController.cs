using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Infrastructure.DTOs;
using WeatherApp.Infrastructure.Services;

namespace WeatherApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class WeatherMeasurementController : Controller
    {
        IWeatherMeasureService _weatherMeasureService;

        public WeatherMeasurementController(IWeatherMeasureService weatherMeasureService)
        {
            _weatherMeasureService = weatherMeasureService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var weatherMeasureDto = await _weatherMeasureService.GetAsync(id);
            return Json(weatherMeasureDto);
        }

        [AllowAnonymous]
        [HttpGet("{measurementDate}/{cityId}")]
        public async Task<IActionResult> Get(int cityId, DateTime measurementDate)
        {
            var weatherMeasureDto = await _weatherMeasureService.GetAsync(cityId, measurementDate);
            return Json(weatherMeasureDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeatherMeasureDTO command)
        {
            try
            {
                await _weatherMeasureService.CreateAsync(command);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]WeatherMeasureDTO command)
        {
            try
            {
                await _weatherMeasureService.UpdateAsync(command);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("cityName/{cityName}")]
        public async Task<IActionResult> Put (string cityName)
        {
            try
            {
                await _weatherMeasureService.UpdateAsync(cityName);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _weatherMeasureService.DeleteAsync(id);
            return NoContent();
        }
    }
}