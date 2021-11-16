using System;
using System.Collections.Generic;
using serverAPI.Entities;
using System.Linq;

namespace serverAPI.Repositories {
    public class LessonRepository : ILessonRepository{
        private List<Lesson> lessons = new(){
            new Lesson {id = Guid.NewGuid(), name = "Math", prophesor ="Ms. Baes", description ="a regular lesson", date = DateTimeOffset.UtcNow},
            new Lesson {id = Guid.NewGuid(), name = "Calculus", prophesor ="Ms. Duralys", description ="a regular lesson too", date = DateTimeOffset.UtcNow},
            new Lesson {id = Guid.NewGuid(), name = "Java", prophesor ="Ms. Mirian", description ="a regular lesson too too", date = DateTimeOffset.UtcNow},
        };

        public IEnumerable<Lesson> GetLessons(){
            return lessons;
        }

        public Lesson GetLesson(Guid _id){
            return lessons.Where(lesson => lesson.id == _id).SingleOrDefault();
        }

        public void CreateLesson(Lesson _lesson){
            lessons.Add(_lesson);
        }

        public void UpdateLesson(Lesson _lesson){
            var index = lessons.FindIndex(lesson => lesson.id == _lesson.id);
            lessons[index] = _lesson;
        }

        public void DeleteLesson(Guid _id){
            var index = lessons.FindIndex(lesson => lesson.id == _id);
            lessons.RemoveAt(index);
        }
    }
}