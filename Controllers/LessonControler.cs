using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using serverAPI.Repositories;
using serverAPI.Dtos;
using System;
using serverAPI.Entities;

namespace serverAPI.Controllers {
    
    [ApiController]
    [Route("[controller]")]
    public class LessonController : ControllerBase {
        private ILessonRepository repository;

        public LessonController (ILessonRepository _repo){
            this.repository = _repo;
        }

        [HttpGet]
        public IEnumerable<LessonDto> GetLessons(){
            var lessons = repository.GetLessons().Select(lesson => lesson.asDto());
            return lessons;
        }

        [HttpGet("{id}")]
        public ActionResult<LessonDto> GetLesson(Guid id){
            var lesson = repository.GetLesson(id);

            if (lesson is null)
                return NotFound();

            return lesson.asDto();
        }

        [HttpPost]
        public ActionResult<LessonDto> CreateLesson(CreateLessonDto lessonDto){
            Lesson lesson = new (){
                id = Guid.NewGuid(),
                name = lessonDto.name,
                prophesor = lessonDto.prophesor,
                date = lessonDto.date,
                description = lessonDto.description,
            };

            repository.CreateLesson(lesson);

            return CreatedAtAction(nameof(GetLesson), new {id = lesson.id}, lesson.asDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateLesson(Guid id, UpdateLessonDto lessonDto){
            var lesson = repository.GetLesson(id);
            if (lesson is null) 
                return NotFound();
            var updatedLesson = lesson with {
                name = lessonDto.name,
                prophesor = lessonDto.prophesor,
                //date = lessonDto.date,
                description = lessonDto.description
            };

            repository.UpdateLesson(updatedLesson);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLesson(Guid id){
            var existingLesson = repository.GetLesson(id);
            
            if (existingLesson is null)
                return NotFound();
            
            repository.DeleteLesson(id);
            return NoContent();
        }
    }  
}