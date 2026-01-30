using API_Farmacia.DTOs;
using API_Farmacia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Farmacia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuministrosController : ControllerBase
    {
        private readonly ISuministrosService _service;
        public SuministrosController(ISuministrosService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        // GET api/Suministros/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return NotFound("No existe el suministro");

            return Ok(result);
        }

        // POST api/Suministros?codBarra=...&descripcion=...
        [HttpPost]
        public IActionResult Post([FromQuery] SuministroPostDTO dto)
        {
            if (!_service.Add(dto))
                return BadRequest("Datos inválidos o faltantes");

            return Ok("Suministro agregado con éxito");
        }

        // PUT api/Suministros/{id}?codBarra=...
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromQuery] SuministroPostDTO dto)
        {
            if (!_service.Update(dto, id))
                return BadRequest("Datos inválidos o faltantes");

            return Ok("Suministro modificado con éxito");
        }

        // DELETE api/Suministros/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id))
                return NotFound("El suministro no existe.");

            return Ok("Suministro eliminado con éxito.");
        }

        // GET api/Suministros/tipos
        [HttpGet("tipos")]
        public IActionResult GetTipos()
        {
            return Ok(_service.tiposSuministros());
        }
    }
}
