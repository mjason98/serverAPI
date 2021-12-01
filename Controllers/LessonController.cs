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
    public class LessonController : ControllerBase {
        private IAgendaRepository repository;

        public LessonController (IAgendaRepository _repo){
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

        [HttpPost("days-bmy")] //days bY mONTH AND yEAR
        public async Task<IEnumerable<DaysDto>> GetDaysByMonthYearAsync(MonthYearDto myDto){
            var daysI = (await repository.GetDailyLessonsAsync(myDto.month, myDto.year)).Select(v => v.asDaysDto());
            return  daysI;
        }

        [HttpPost("lbd")] //lESSON bY dATE
        public async Task<IEnumerable<LessonEDto>> GetLessonsByDate(DayMonthYearDto myDto){
            var lessons = ( await repository.GetLessonsByDateAsync(myDto.day, myDto.month, myDto.year)).Select(lesson => lesson.asDto());
            return lessons;
        }

        [HttpPost]
        public async Task<ActionResult<LessonDto>> CreateLessonAsync(CreateLessonDto lessonDto){
            Lesson lesson = new (){
                TopicId = lessonDto.name,
                ProfesorId = lessonDto.prophesor,
                dateIni = lessonDto.dateIni,
                dateFin = lessonDto.dateFin,
                description = lessonDto.description,
            };

            int ide = await repository.CreateLessonAsync(lesson);
            Lesson lessonWIde = lesson with {
                Id = ide,
            };

            return CreatedAtAction(nameof(GetLessonAsync), new {id = ide}, lessonWIde.asDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLessonAsync(int id, UpdateLessonDto lessonDto){
            var lesson = await repository.GetLessonAsync(id);
            if (lesson is null) 
                return NotFound();
            var updatedLesson = lesson with {
                TopicId = lessonDto.name,
                ProfesorId = lessonDto.prophesor,
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