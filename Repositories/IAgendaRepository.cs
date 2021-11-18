using System.Collections.Generic;
using System.Threading.Tasks;
using serverAPI.Entities;

namespace serverAPI.Repositories {
    public interface IAgendaRepository {
        Task<IEnumerable<Lesson>> GetLessonsAsync();
        Task<Lesson> GetLessonAsync(int _id);
        Task<int> CreateLessonAsync(Lesson _lesson);
        Task UpdateLessonAsync(Lesson _lesson);
        Task DeleteLessonAsync(int _id);

        /* Tuple<day number, number of lessons that day> */
        Task<IEnumerable<System.Tuple<int,int>>> GetDailyLessonsAsync(int month, int year);

        Task<IEnumerable<LessonE>> GetLessonsByDateAsync(int day, int month, int year);

        Task<IEnumerable<Profesor>> GetProfesorsAsync();
        Task<Profesor> GetProfesorAsync(int _id);
        Task<int> CreateProfesorAsync(Profesor _lesson);
        Task UpdateProfesorAsync(Profesor _lesson);
        Task DeleteProfesorAsync(int _id);

        Task<IEnumerable<Topic>> GetTopicsAsync();
        Task<Topic> GetTopicAsync(int _id);
        Task<int> CreateTopicAsync(Topic _lesson);
        Task UpdateTopicAsync(Topic _lesson);
        Task DeleteTopicAsync(int _id);
    }
}