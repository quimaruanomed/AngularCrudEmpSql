using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadoCrud_SQL.Data;
using EmpleadoCrud_SQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpleadoCrud_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeContext _context;
        public EmployeesController(EmployeContext context)
        {
            _context = context;
        }

        //Mostrará todos los empleados 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmployes()
        {
            return await _context.Employe.ToListAsync();
        }
        //Ruta api/Employes/Id --> Mostrará los datos del empleado que coincidan con la id , sino existe "Registro no encontrado"
        [HttpGet("{Id}")]

        public async Task<ActionResult<Employe>> GetEmployesId(int Id)
        {
            var employeId = await _context.Employe.FindAsync(Id);
            if (employeId == null)
            {
                return NotFound("Registro no encontrado");

            }
            return employeId;

        }

        //Método para añadir un nuevo registro 
        [HttpPost]
        public async Task<ActionResult<Employe>> PostEmployeId(Employe employe)
        {
            _context.Employe.Add(employe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployesId), new { Id = employe.Id }, employe);

        }
        [HttpPut("{Id}")]

        public async Task<IActionResult> PutEmployeId(int Id, Employe employe)
        {
            if (Id != employe.Id)
            {
                return BadRequest();
            }
            _context.Entry(employe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();


        }
     //Método para borrar registros filtrando por Id
    [HttpDelete("{Id}")]
        
        public async Task<IActionResult> DeleteId(int Id)
        {
            var employeId = await _context.Employe.FindAsync(Id);
            if(employeId == null)
            {
                return NotFound("Registro no encontrado");

            }
            _context.Employe.Remove(employeId);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //Método Patch que comprubea si existe el id en la base de datos , devolverá un código en función del resultado de la ejecución del código  
        [HttpPatch("FirstName/{Id}")]
        public async Task<IActionResult> ChangeHello(int id, string ChangeFName)
        {
            if (string.IsNullOrWhiteSpace(ChangeFName))
            {
                return BadRequest();
            }
            var Employe = await _context.Employe.FindAsync(id);
            if (Employe == null)
            {
                return NotFound();

            }
            if (await _context.Employe.Where(x => x.FirstName == ChangeFName && x.Id != id).AnyAsync())
            {
                return BadRequest("El id ya Existe");

            }
            Employe.FirstName = ChangeFName;
            await _context.SaveChangesAsync();
            return StatusCode(200, Employe);
        }
        //Método que ejecutará el cambio parcial
        [HttpPatch("{Id}")]
        public async Task<IActionResult> PatchId(int Id, JsonPatchDocument<Employe> _ChangeEmploye)
        {
            
            var ChangeEmploye = await _context.Employe.FindAsync(Id);
            if (ChangeEmploye == null)
            {
                return NotFound("Registro no encontrado");
            }
            _ChangeEmploye.ApplyTo(ChangeEmploye, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);
            await _context.SaveChangesAsync();
            return Ok(ChangeEmploye);

        }
    }

}

