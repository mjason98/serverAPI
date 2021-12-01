using serverAPI.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security;
using System;
using System.Threading.Tasks;

namespace serverAPI.Repositories{
    
    public class AgendaRepositorySQLServer : IAgendaRepository
    {
        private readonly IConfiguration configuration;
        
        #region InitMethods
        
        public AgendaRepositorySQLServer(IConfiguration _configuration){
            this.configuration = _configuration;
        }

        private SqlCredential CreateMyCredentials(){
            string pwdR = configuration.GetConnectionString("Password"); 
            string user = configuration.GetConnectionString("User");
            
            //a very bad practice, seek other way in the future ------------------
            // The variable pwdR mos be removed, couse the idea of habing Sequirity strings its to not habe the passwrd
            // value in memory, and pwdR does.
            SecureString pwd = new SecureString();
            foreach(var c in pwdR){
                pwd.AppendChar(c);
            }
            //--------------------------------------------------------------------
            pwd.MakeReadOnly();

            return new SqlCredential(user, pwd);
        }

        private async Task<DataTable> processSQLQuery(string query){
            DataTable table = new DataTable();
            string sqlDataSource = configuration.GetConnectionString("serverAPICon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)){
                SqlCredential cred = CreateMyCredentials();
                myCon.Credential = cred;
                
                await myCon.OpenAsync();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)){
                    myReader = await myCommand.ExecuteReaderAsync();
                    table.Load(myReader);
                    myReader.Close();
                }
                await myCon.CloseAsync();
            }
            return table;
        }

        private async Task processSQLQueryNotAnswer(string query){
            string sqlDataSource = configuration.GetConnectionString("serverAPICon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)){
                SqlCredential cred = CreateMyCredentials();
                myCon.Credential = cred;
                
                await myCon.OpenAsync();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)){
                    myReader = await myCommand.ExecuteReaderAsync();
                    myReader.Close();
                }
                await myCon.CloseAsync();
            }
        }

        #endregion
        
        #region LessonMethods

        public async Task<int> CreateLessonAsync(Lesson _lesson)
        { 
            string dateIni = _lesson.dateIni.asStringDB();
            string dateFin = _lesson.dateFin.asStringDB();
            string query   = @"insert into dbo.Lessons (name, prophesor, dateIni, dateFin, descr) values
                             ("+_lesson.TopicId.ToString()+@", "+_lesson.ProfesorId.ToString()+@", '"+dateIni+@"', '"+dateFin+@"', '"+_lesson.description.ToString() + @"');
                             SELECT SCOPE_IDENTITY() as n;";
            
            var table = await processSQLQuery(query);
            var nRow = table.Select().SingleOrDefault();

            if (nRow is null)
                return 0;
            return Convert.ToInt32(nRow["n"]);
        }

        public async Task DeleteLessonAsync(int _id)
        {
            string query = @"delete from dbo.Lessons where id = "+_id.ToString();
            await processSQLQueryNotAnswer(query);
        }

        public async Task<Lesson> GetLessonAsync(int _id)
        {
            string query = @"select id, name, prophesor, dateIni, dateFin, descr
                                   from dbo.Lessons where id =" + _id.ToString();
            var table  = await processSQLQuery(query);
            var lessonRow =  table.Select().SingleOrDefault();
            
            if (lessonRow is null)
                return null;

            Lesson lesson = new (){
                Id = Convert.ToInt32(lessonRow["id"]),
                TopicId = Convert.ToInt32(lessonRow["name"]),
                ProfesorId =  Convert.ToInt32(lessonRow["prophesor"]),
                description = lessonRow["descr"].ToString(),
                dateIni = DateTimeOffset.Parse(lessonRow["dateIni"].ToString()),
                dateFin = DateTimeOffset.Parse(lessonRow["dateFin"].ToString())
            };
            return lesson;
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsync()
        {
            const string query = @"select id, name, prophesor, dateIni, dateFin, descr
                                   from dbo.Lessons";
            var table = await processSQLQuery(query);
            
            var lessons = table.Select().Select(row => 
                new Lesson{
                    Id = Convert.ToInt32(row["id"]),
                    TopicId = Convert.ToInt32(row["name"]),
                    ProfesorId =  Convert.ToInt32(row["prophesor"]),
                    description = row["descr"].ToString(),
                    dateIni = DateTimeOffset.Parse(row["dateIni"].ToString()),
                    dateFin = DateTimeOffset.Parse(row["dateFin"].ToString())
                }
            );

            return lessons;
        }
 
        public async Task UpdateLessonAsync(Lesson _lesson)
        {
            string dateIni = _lesson.dateIni.asStringDB();
            string dateFin = _lesson.dateFin.asStringDB();
            string query = @"update dbo.Lessons set name = "+_lesson.TopicId.ToString() + @", 
                             prophesor = "+_lesson.ProfesorId.ToString() + @", descr = '"+_lesson.description.ToString() + @"', 
                             dateIni = '"+dateIni+@"', dateFin = '"+dateFin+@"' where 
                             id = "+_lesson.Id.ToString();
            await processSQLQueryNotAnswer(query);
        }

        public async Task<IEnumerable<Tuple<int, int>>> GetDailyLessonsAsync(int month, int year)
        {
            string query = @"SELECT t1.day as day, COUNT(*) as n FROM ( SELECT DAY(dateIni) as day FROM Lessons
                             WHERE YEAR(dateIni) = "+year.ToString()+@" and MONTH(dateIni) = "+month.ToString()+@" ) as t1 GROUP BY t1.day";
            var table = await processSQLQuery(query);
            var days = table.Select().Select(row => new Tuple<int,int>(Convert.ToInt32(row["day"]), Convert.ToInt32(row["n"])));
            return days;
        }

        public async Task<IEnumerable<LessonE>> GetLessonsByDateAsync(int day, int month, int year)
        {
            string query = @"select Lessons.id as id, Lessons.name as name, Lessons.prophesor as prophesor, Lessons.dateIni as dateIni, 
                              Lessons.dateFin as dateFin, Lessons.descr as descr, Topics.name as nameS, Claustro.name as prophesorS
                              from Lessons, Claustro, Topics 
                              where day(dateIni) = "+day.ToString()+@" and year(dateIni) = "+year.ToString()+@" and month(dateIni) = "+month.ToString()+@" 
                                    and Lessons.name = Topics.id and Claustro.id = Lessons.prophesor
                              order by Lessons.dateIni";
            var table = await processSQLQuery(query);
            
            var lessons = table.Select().Select(row => 
                new LessonE{
                    Id = Convert.ToInt32(row["id"]),
                    TopicId = Convert.ToInt32(row["name"]),
                    ProfesorId =  Convert.ToInt32(row["prophesor"]),
                    description = row["descr"].ToString(),
                    dateIni = DateTimeOffset.Parse(row["dateIni"].ToString()),
                    dateFin = DateTimeOffset.Parse(row["dateFin"].ToString()),
                    nameS = row["nameS"].ToString(),
                    prophesorS = row["prophesorS"].ToString(),
                }
            );

            return lessons;
        }

        #endregion

        #region ProfesorMethods

        public async Task<IEnumerable<Profesor>> GetProfesorsAsync()
        {
            const string query = @"select id, name from dbo.Claustro";
            var table = await processSQLQuery(query);
            
            var profesors = table.Select().Select(row => 
                new Profesor{
                    Id = Convert.ToInt32(row["id"]),
                    name = row["name"].ToString()
                }
            );
            return profesors;
        }

        public async Task<Profesor> GetProfesorAsync(int _id)
        {
            string query = @"select id, name from dbo.Claustro where id = " + _id.ToString();
            var table  = await processSQLQuery(query);
            var vRow =  table.Select().SingleOrDefault();
            
            if (vRow is null)
                return null;

            Profesor prof = new (){
                Id = Convert.ToInt32(vRow["id"]),
                name = vRow["name"].ToString()
            };
            return prof;
        }

        public async Task<int> CreateProfesorAsync(Profesor prof)
        {
            string query   = @"insert into dbo.Claustro (name) values ('" + prof.name + @"');;
                             SELECT SCOPE_IDENTITY() as n;";
            
            var table = await processSQLQuery(query);
            var nRow = table.Select().SingleOrDefault();

            if (nRow is null)
                return 0;
            return Convert.ToInt32(nRow["n"]);
        }

        public async Task UpdateProfesorAsync(Profesor prof)
        {
            string query   = @"update dbo.Claustro set name='" + prof.name + @"' where id = " + prof.Id.ToString();
            await processSQLQueryNotAnswer(query);
        }

        public async Task DeleteProfesorAsync(int _id)
        {
            string query = @"delete from dbo.Claustro where id = "+_id.ToString();
            await processSQLQueryNotAnswer(query);
        }

        #endregion

        #region TopicMethods
        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            const string query = @"select id, name from dbo.Topics";
            var table = await processSQLQuery(query);
            
            var topics = table.Select().Select(row => 
                new Topic{
                    Id = Convert.ToInt32(row["id"]),
                    name = row["name"].ToString()
                }
            );
            return topics;
        }

        public async Task<Topic> GetTopicAsync(int _id)
        {
            string query = @"select id, name from dbo.Topics where id = " + _id.ToString();
            var table  = await processSQLQuery(query);
            var vRow =  table.Select().SingleOrDefault();
            
            if (vRow is null)
                return null;

            Topic topic = new (){
                Id = Convert.ToInt32(vRow["id"]),
                name = vRow["name"].ToString()
            };
            return topic;
        }

        public async Task<int> CreateTopicAsync(Topic topic)
        {
            string query   = @"insert into dbo.Topics (name) values ('" + topic.name + @"');
                             SELECT SCOPE_IDENTITY() as n;";
            
            var table = await processSQLQuery(query);
            var nRow = table.Select().SingleOrDefault();

            if (nRow is null)
                return 0;
            return Convert.ToInt32(nRow["n"]);
        }

        public async Task UpdateTopicAsync(Topic topic)
        {
            string query   = @"update dbo.Topics set name='" + topic.name + @"' where id = " + topic.Id.ToString();
            await processSQLQueryNotAnswer(query);
        }

        public async Task DeleteTopicAsync(int _id)
        {
            string query = @"delete from dbo.Topics where id = "+_id.ToString();
            await processSQLQueryNotAnswer(query);
        }

        #endregion
    }
}