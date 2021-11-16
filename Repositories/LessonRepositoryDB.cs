using serverAPI.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace serverAPI.Repositories{
    public class LessonRepositoryBD : ILessonRepository
    {
        private readonly IConfiguration configuration;
        
        public LessonRepositoryBD(IConfiguration _configuration){
            this.configuration = _configuration;
        }
        
        public void CreateLesson(Lesson _lesson)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteLesson(int _id)
        {
            throw new System.NotImplementedException();
        }

        public Lesson GetLesson(int _id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Lesson> GetLessons()
        {
            const string query = @"select Lessons.id, Lessons.name, Lessons.prophesor, Lessons.dateIni, Lessons.dateFin, Lessons.descr
                                   from dbo.Lessons";
            throw new System.NotImplementedException();
        }

        public void UpdateLesson(Lesson _lesson)
        {
            throw new System.NotImplementedException();
        }
    }
}