using System.Collections.Generic;
using System.Threading.Tasks;
using serverAPI.Entities;

namespace serverAPI.Repositories {
    public interface IAgendaRepository {
        Task<IEnumerable<Lesson>> GetLessonsAsync();
        Task<Lesson> GetLessonAsync(int _id);
        Task CreateLessonAsync(Lesson _lesson);
        Task UpdateLessonAsync(Lesson _lesson);
        Task DeleteLessonAsync(int _id);

        Task<IEnumerable<Profesor>> GetProfesorsAsync();
        Task<Profesor> GetProfesorAsync(int _id);
        Task CreateProfesorAsync(Profesor _lesson);
        Task UpdateProfesorAsync(Profesor _lesson);
        Task DeleteProfesorAsync(int _id);

        Task<IEnumerable<Topic>> GetTopicsAsync();
        Task<Topic> GetTopicAsync(int _id);
        Task CreateTopicAsync(Topic _lesson);
        Task UpdateTopicAsync(Topic _lesson);
        Task DeleteTopicAsync(int _id);
    }
}