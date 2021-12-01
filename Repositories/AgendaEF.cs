using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using serverAPI.Entities;
using System.Linq;

namespace serverAPI.Repositories{

    public class AgendaDbContext: DbContext {

        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base (options){
        }

        public DbSet<Topic> Topics {get; set;}
        
    }


    public class AgendaEF : IAgendaRepository
    {
        private readonly AgendaDbContext _context;
        public AgendaEF(AgendaDbContext context){
            _context = context;
        }
        public Task<int> CreateLessonAsync(Lesson _lesson)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateProfesorAsync(Profesor _lesson)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateTopicAsync(Topic _lesson)
        {
            await _context.AddAsync(_lesson);
            await _context.SaveChangesAsync();
            return _lesson.id;
        }

        public Task DeleteLessonAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProfesorAsync(int _id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTopicAsync(int _id)
        {
            var toDelete = await _context.Topics.Where(x => x.id == _id).SingleOrDefaultAsync();
            if (toDelete is null)
                return;
            _context.Remove(toDelete);
            await _context.SaveChangesAsync();
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

        public async Task<Topic> GetTopicAsync(int _id)
        {
            var topic = await _context.Topics.Where(x => x.id == _id).SingleOrDefaultAsync();
            return topic;
        }

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            var topics = await _context.Topics.ToListAsync();
            return topics;       
        }

        public Task UpdateLessonAsync(Lesson _lesson)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfesorAsync(Profesor _lesson)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTopicAsync(Topic _lesson)
        {
            _context.Update(_lesson);
            await _context.SaveChangesAsync();
        }
    }
}