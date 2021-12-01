
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using serverAPI.Entities;

namespace serverAPI.Repositories{
    public class AgendaEF : IAgendaRepository
    {
        public Task<int> CreateLessonAsync(Lesson _lesson)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateProfesorAsync(Profesor _lesson)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateTopicAsync(Topic _lesson)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLessonAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProfesorAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTopicAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tuple<int, int>>> GetDailyLessonsAsync(int month, int year)
        {
            throw new NotImplementedException();
        }

        public Task<Lesson> GetLessonAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lesson>> GetLessonsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LessonE>> GetLessonsByDateAsync(int day, int month, int year)
        {
            throw new NotImplementedException();
        }

        public Task<Profesor> GetProfesorAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Profesor>> GetProfesorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Topic> GetTopicAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateLessonAsync(Lesson _lesson)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfesorAsync(Profesor _lesson)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTopicAsync(Topic _lesson)
        {
            throw new NotImplementedException();
        }
    }
}