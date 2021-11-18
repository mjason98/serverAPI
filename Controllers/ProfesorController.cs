using System.Linq;
using Microsoft.AspNetCore.Mvc;
using serverAPI.Repositories;
using serverAPI.Dtos;
using serverAPI.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

///// HAY ERROR EN EL CREATE CON EL ID

namespace serverAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesorController : ControllerBase {
        private IAgendaRepository repository;

        public ProfesorController (IAgendaRepository _repo){
            this.repository = _repo;
        }

        [HttpGet]
        public async Task<IEnumerable<ProfesorDto>> GetProfesorsAsync(){
            var vs = ( await repository.GetProfesorsAsync()).Select(v => v.asDto());
            return vs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorDto>> GetProfesorAsync(int id){
            var v = await repository.GetProfesorAsync(id);

            if (v is null)
                return NotFound();

            return v.asDto();
        }

        [HttpPost]
        public async Task<ActionResult<ProfesorDto>> CreateProfesorAsync(CreateUpdateProfesorDto prof){
            Profesor v = new (){
                name = prof.name
            };

            int ide = await repository.CreateProfesorAsync(v);
            Profesor vIde = v with {
                id = ide
            };
            return CreatedAtAction(nameof(GetProfesorAsync), new {id = ide}, vIde.asDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProfesorAsync(int id, CreateUpdateProfesorDto profDto){
            var prof = await repository.GetProfesorAsync(id);
            if (prof is null) 
                return NotFound();
            var updatedProf = prof with {
                name = profDto.name
            };

            await repository.UpdateProfesorAsync(updatedProf);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProfesorAsync(int id){
            var existingProf = await repository.GetProfesorAsync(id);
            
            if (existingProf is null)
                return NotFound();
            
            await repository.DeleteProfesorAsync(id);
            return NoContent();
        }
    }  
}