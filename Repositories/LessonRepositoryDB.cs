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
    
    public class LessonRepositoryBD : ILessonRepository
    {
        private readonly IConfiguration configuration;
        
        public LessonRepositoryBD(IConfiguration _configuration){
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
        
        public async Task CreateLessonAsync(Lesson _lesson)
        { 
            string dateIni = _lesson.dateIni.asStringDB();
            string dateFin = _lesson.dateFin.asStringDB();
            string query   = @"insert into dbo.Lessons (name, prophesor, dateIni, dateFin, descr) values
                             ("+_lesson.name.ToString()+@", "+_lesson.prophesor.ToString()+@", '"+dateIni+@"', '"+dateFin+@"', '"+_lesson.description.ToString() + @"')";
            await processSQLQueryNotAnswer(query);
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
                id = Convert.ToInt32(lessonRow["id"]),
                name = Convert.ToInt32(lessonRow["name"]),
                prophesor =  Convert.ToInt32(lessonRow["prophesor"]),
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
                    id = Convert.ToInt32(row["id"]),
                    name = Convert.ToInt32(row["name"]),
                    prophesor =  Convert.ToInt32(row["prophesor"]),
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
            string query = @"update dbo.Lessons set name = "+_lesson.name.ToString() + @", 
                             prophesor = "+_lesson.prophesor.ToString() + @", descr = '"+_lesson.description.ToString() + @"', 
                             dateIni = '"+dateIni+@"', dateFin = '"+dateFin+@"' where 
                             id = "+_lesson.id.ToString();
            await processSQLQueryNotAnswer(query);
        }
    }
}