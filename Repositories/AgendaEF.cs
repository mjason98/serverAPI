using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using serverAPI.Entities;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace serverAPI.Repositories{

    public class AgendaDbContext: IdentityDbContext<ApplicationUser> {

        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base (options){
        }

        public DbSet<Topic> Topics {get; set;}
        public DbSet<Profesor> Claustro {get; set;}
        public DbSet<Lesson> Lessons {get; set;}
    }


    public class AgendaEF : IAgendaRepository
    {
        private readonly AgendaDbContext _context;
        public AgendaEF(AgendaDbContext context){
            _context = context;
        }
        public async Task<int> CreateLessonAsync(Lesson _lesson)
        {
            // var topic = await _context.Topics.Where(x => x.Id == _lesson.TopicId).SingleOrDefaultAsync();
            // var profe = await _context.Claustro.Where(x => x.Id == _lesson.ProfesorId).SingleOrDefaultAsync();

            // var lesson = _lesson with{
            //     ProfesorId = profe.Id,
            //     TopicId = topic.Id,
            // };

            await _context.AddAsync(_lesson);
            await _context.SaveChangesAsync();
            return _lesson.Id;
        }

        public async Task<int> CreateProfesorAsync(Profesor _lesson)
        {
            await _context.AddAsync(_lesson);
            await _context.SaveChangesAsync();
            return _lesson.Id;
        }

        public async Task<int> CreateTopicAsync(Topic _lesson)
        {
            await _context.AddAsync(_lesson);
            await _context.SaveChangesAsync();
            return _lesson.Id;
        }

        public async Task DeleteLessonAsync(int _id)
        {
            var toDelete = await _context.Lessons.Where(x => x.Id == _id).SingleOrDefaultAsync();
            if (toDelete is null)
                return;
            _context.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfesorAsync(int _id)
        {
            var toDelete = await _context.Claustro.Where(x => x.Id == _id).SingleOrDefaultAsync();
            if (toDelete is null)
                return;
            _context.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTopicAsync(int _id)
        {
            var toDelete = await _context.Topics.Where(x => x.Id == _id).SingleOrDefaultAsync();
            if (toDelete is null)
                return;
            _context.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tuple<int, int>>> GetDailyLessonsAsync(int month, int year)
        {
            var days = await _context.Lessons.Where(x => x.dateIni.Year == year && x.dateIni.Month == month)
                                       .Select(x => new { day = x.dateIni.Day} ).GroupBy(x => x.day) 
                                       .Select(x => new Tuple<int,int>(x.Key, x.Count()))
                                       .ToListAsync();
            return days;
        }

        public async Task<Lesson> GetLessonAsync(int _id)
        {
            var lesson = await _context.Lessons.Where(x => x.Id == _id).SingleOrDefaultAsync();
            return lesson;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync()
        {
            var lessons = await _context.Lessons.ToListAsync();
            return lessons;
        }

        public async Task<IEnumerable<LessonE>> GetLessonsByDateAsync(int day, int month, int year)
        {
            var lessons = await _context.Lessons.Where(x => x.dateIni.Day == day && x.dateIni.Month == month && x.dateIni.Year == year)
                                          .Include(x => x.Profesor).Include(x => x.Topic)
                                          .Select(x => new LessonE{
                                              Id = x.Id,
                                              TopicId = x.TopicId,
                                              ProfesorId =  x.ProfesorId,
                                              description = x.description,
                                              dateIni = x.dateIni,
                                              dateFin = x.dateFin,
                                              nameS = x.Topic.name,
                                              prophesorS = x.Profesor.name,
                                          }).ToListAsync();
            return lessons;
        }

        public async Task<Profesor> GetProfesorAsync(int _id)
        {
            var prof = await _context.Claustro.Where(x => x.Id == _id).SingleOrDefaultAsync();
            return prof;
        }

        public async Task<IEnumerable<Profesor>> GetProfesorsAsync()
        {
            var profs = await _context.Claustro.ToListAsync();
            return profs; 
        }

        public async Task<Topic> GetTopicAsync(int _id)
        {
            var topic = await _context.Topics.Where(x => x.Id == _id).SingleOrDefaultAsync();
            return topic;
        }

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            var topics = await _context.Topics.ToListAsync();
            return topics;       
        }

        public async Task UpdateLessonAsync(Lesson _lesson)
        {
            _context.Update(_lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfesorAsync(Profesor _lesson)
        {
            _context.Update(_lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTopicAsync(Topic _lesson)
        {
            _context.Update(_lesson);
            await _context.SaveChangesAsync();
        }
    }
}