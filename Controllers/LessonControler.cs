using System.Linq;
using Microsoft.AspNetCore.Mvc;
using serverAPI.Repositories;
using serverAPI.Dtos;
using serverAPI.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace serverAPI.Controllers {
    
    [ApiController]
    [Route("api/[controller]")] //revisar lo de api
    public class LessonController : ControllerBase {
        private ILessonRepository repository;

        public LessonController (ILessonRepository _repo){
            this.repository = _repo;
        }

        [HttpGet]
        public async Task<IEnumerable<LessonDto>> GetLessonsAsync(){
            var lessons = ( await repository.GetLessonsAsync()).Select(lesson => lesson.asDto());
            return lessons;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLessonAsync(int id){
            var lesson = await repository.GetLessonAsync(id);

            if (lesson is null)
                return NotFound();

            return lesson.asDto();
        }

        [HttpPost]
        public async Task<ActionResult<LessonDto>> CreateLessonAsync(CreateLessonDto lessonDto){
            Lesson lesson = new (){
                id = 10, //esto esta re mal
                name = lessonDto.name,
                prophesor = lessonDto.prophesor,
                dateIni = lessonDto.dateIni,
                dateFin = lessonDto.dateFin,
                description = lessonDto.description,
            };

            await repository.CreateLessonAsync(lesson);

            return CreatedAtAction(nameof(GetLessonAsync), new {id = lesson.id}, lesson.asDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLessonAsync(int id, UpdateLessonDto lessonDto){
            var lesson = await repository.GetLessonAsync(id);
            if (lesson is null) 
                return NotFound();
            var updatedLesson = lesson with {
                name = lessonDto.name,
                prophesor = lessonDto.prophesor,
                dateIni = lessonDto.dateIni,
                dateFin = lessonDto.dateFin,
                description = lessonDto.description
            };

            await repository.UpdateLessonAsync(updatedLesson);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLessonAsync(int id){
            var existingLesson = await repository.GetLessonAsync(id);
            
            if (existingLesson is null)
                return NotFound();
            
            await repository.DeleteLessonAsync(id);
            return NoContent();
        }
    }  
}