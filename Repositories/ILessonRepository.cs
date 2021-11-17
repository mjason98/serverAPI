using System.Collections.Generic;
using System.Threading.Tasks;
using serverAPI.Entities;

namespace serverAPI.Repositories {
    public interface ILessonRepository {
        Task<IEnumerable<Lesson>> GetLessonsAsync();
        Task<Lesson> GetLessonAsync(int _id);
        Task CreateLessonAsync(Lesson _lesson);
        Task UpdateLessonAsync(Lesson _lesson);
        Task DeleteLessonAsync(int _id);
    }
}