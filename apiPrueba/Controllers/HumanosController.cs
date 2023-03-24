using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiPrueba.Data;
using Bogus;
using static Bogus.DataSets.Name;
using Bogus.DataSets;

namespace apiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanosController : ControllerBase
    {
        private pruebaContext _context;

        public HumanosController(pruebaContext context)
        {
            _context = context;
        }

        [HttpGet("Generado")]
        public IActionResult GetUsers() {

            var orderIds = 1;
            var fakeHumanos = new Faker<Humano>(locale: "es_MX")
                .RuleFor(a => a.Id, f => orderIds++)
                .RuleFor(a => a.Genero, f => f.Person.Gender.ToString())
                .RuleFor(a => a.Nombre, f => f.Person.FullName)
                .RuleFor(a => a.Edad, f => f.Random.Number(10, 90))
                .RuleFor(a => a.Altura, f => Double.Round(f.Random.Double(0.5, 2), 2))
                .RuleFor(a => a.Peso, f => Double.Round(f.Random.Double(30, 150), 2));

            var users = fakeHumanos.Generate(10);
            return Ok(users);
        }

        // GET: api/Humanos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Humano>>> GetHumano()
        {
          if (_context.Humanos == null)
          {
              return NotFound("No se encontrtaron datos");
          }
            return await _context.Humanos.ToListAsync();
        }

        // GET: api/Humanos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Humano>> GetHumano(int id)
        {
          if (_context.Humanos == null)
          {
              return NotFound("No se encontrtaron datos");
          }
            var humano = await _context.Humanos.FindAsync(id);

            if (humano == null)
            {
                return NotFound("No se encontrtaron datos");
            }

            return humano;
        }

        // PUT: api/Humanos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHumano(int id, Humano humano)
        {
            if (id != humano.Id)
            {
                return BadRequest("Se ingreso un id inválido");
            }
            if (humano.Edad == 0)
            {
                return BadRequest("La edad no puede ser 0");
            }
            if (humano.Peso == 0)
            {
                return BadRequest("El peso no puede ser 0");
            }
            if (humano.Altura == 0)
            {
                return BadRequest("La altura no puede ser 0");
            }
            if (humano.Nombre.Trim() == "")
            {
                return BadRequest("Ingrese un nombre");
            }
            if (humano.Genero.Trim() == "")
            {
                return BadRequest("Ingrese un género");
            }

            _context.Entry(humano).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanoExists(id))
                {
                    return NotFound("No se encontrtaron datos");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Humanos
        [HttpPost]
        public async Task<ActionResult<Humano>> PostHumano(Humano humano)
        {
            if (_context.Humanos == null)
            {
                return Problem("No se ingresaron datos");
            }
            if (humano.Edad == 0)
            {
                return BadRequest("La edad no puede ser 0");
            }
            if (humano.Peso == 0)
            {
                return BadRequest("El peso no puede ser 0");
            }
            if (humano.Altura == 0)
            {
                return BadRequest("La altura no puede ser 0");
            }
            if (humano.Nombre.Trim() == "")
            {
                return BadRequest("Ingrese un nombre");
            }
            if (humano.Genero.Trim() == "")
            {
                return BadRequest("Ingrese un género");
            }

            _context.Humanos.Add(humano);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHumano", new { id = humano.Id }, humano);
        }

        // DELETE: api/Humanos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHumano(int id)
        {
            if (_context.Humanos == null)
            {
                return NotFound("No se encontrtaron datos");
            }
            var humano = await _context.Humanos.FindAsync(id);
            if (humano == null)
            {
                return NotFound("No se encontrtaron datos");
            }

            _context.Humanos.Remove(humano);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumanoExists(int id)
        {
            return (_context.Humanos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
