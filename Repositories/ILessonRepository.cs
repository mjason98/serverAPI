using System;
using System.Collections.Generic;
using serverAPI.Entities;

namespace serverAPI.Repositories {
    public interface ILessonRepository {
        IEnumerable<Lesson> GetLessons();
        Lesson GetLesson(int _id);
        void CreateLesson(Lesson _lesson);
        void UpdateLesson(Lesson _lesson);
        void DeleteLesson(int _id);
    }
}