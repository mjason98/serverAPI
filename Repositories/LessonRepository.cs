using System;
using System.Collections.Generic;
using serverAPI.Entities;
using System.Linq;

namespace serverAPI.Repositories {
    public class LessonRepository : ILessonRepository{
        private List<Lesson> lessons = new(){
            new Lesson {id = 1, name = 1, prophesor = 1, description ="a regular lesson", dateIni = DateTimeOffset.UtcNow},
            new Lesson {id = 2, name = 1, prophesor = 1, description ="a regular lesson too", dateIni = DateTimeOffset.UtcNow},
            new Lesson {id = 3, name = 2, prophesor = 2, description ="a regular lesson too too", dateIni = DateTimeOffset.UtcNow},
        };

        public IEnumerable<Lesson> GetLessons(){
            return lessons;
        }

        public Lesson GetLesson(int _id){
            return lessons.Where(lesson => lesson.id == _id).SingleOrDefault();
        }

        public void CreateLesson(Lesson _lesson){
            lessons.Add(_lesson);
        }

        public void UpdateLesson(Lesson _lesson){
            var index = lessons.FindIndex(lesson => lesson.id == _lesson.id);
            lessons[index] = _lesson;
        }

        public void DeleteLesson(int _id){
            var index = lessons.FindIndex(lesson => lesson.id == _id);
            lessons.RemoveAt(index);
        }
    }
}