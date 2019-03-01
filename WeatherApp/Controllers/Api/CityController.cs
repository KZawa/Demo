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
    public class CityController : Controller
    {
        ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [AllowAnonymous]
        [HttpGet("id/{id}/{dayCount}")]
        public async Task<IActionResult> Get(int id, int dayCount)
        {
            var cityDto = await _cityService.GetAsync(id, dayCount);
            var x = Json(cityDto);
            return x;
        }

        [AllowAnonymous]
        [HttpGet("name/{name}/{dayCount}")]
        public async Task<IActionResult> Get(string name, int dayCount)
        {
            var cityDTO = await _cityService.GetAsync(name, dayCount);
            return Json(cityDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CityDTO command)
        {
            try
            {
                await _cityService.CreateAsync(command);
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
            try
            {
                await _cityService.DeleteAsync(id);
                return NoContent();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}