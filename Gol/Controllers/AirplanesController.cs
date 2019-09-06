using Gol.Domains;
using Gol.Domains.Generics;
using Gol.Entities;
using Gol.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gol.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirplanesController : ControllerBase
    {
        private readonly IDomain<Airplane> _domain;

        public AirplanesController([FromServices]IDomain<Airplane> domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody]Airplane airplane)
        {
            try
            {
                airplane.RegistryCreationDate = DateTime.Now;

                var result = await _domain.SaveAsync(airplane);
                return StatusCode(200, airplane);
            }
            catch(AirplaneException e)
            {
                return StatusCode(400, e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _domain.GetAsync();
                return StatusCode(200, result);
            }
            catch (AirplaneException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            try
            {
                var result = await _domain.GetByIdAsync(id);
                return StatusCode(200, result);
            }
            catch (AirplaneException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]Airplane airplane)
        {
            try
            {
                var result = await _domain.UpdateAsync(airplane);
                return StatusCode(200, airplane);
            }
            catch (AirplaneException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            try
            {
                var airplane = await _domain.GetByIdAsync(id);
                await _domain.DeleteAsync(airplane);

                return StatusCode(200, airplane);
            }
            catch (AirplaneException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}