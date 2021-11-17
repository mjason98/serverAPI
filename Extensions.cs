using serverAPI.Entities;
using serverAPI.Dtos;
using System;

namespace serverAPI{
    public static class Extensions{
        public static LessonDto asDto(this Lesson lesson){
            return new LessonDto{
                name = lesson.name,
                id = lesson.id,
                prophesor = lesson.prophesor,
                dateIni = lesson.dateIni,
                dateFin = lesson.dateFin,
                description = lesson.description
            };
        }
        public static string asStringDB(this DateTimeOffset date){
            return date.Year.ToString()+'-' + date.Month.ToString()+'-'+date.Day.ToString()+' '+date.Hour+':'+date.Minute+':'+date.Second;
        }
        public static ProfesorDto asDto (this Profesor prof){
            return new ProfesorDto{
                name = prof.name,
                id = prof.id
            };
        }
        public static TopicDto asDto (this Topic topic){
            return new TopicDto{
                name = topic.name,
                id = topic.id
            };
        }

        public static DaysDto asDaysDto(this Tuple<int, int> tup){
            return new DaysDto {
                day = tup.Item1,
                n = tup.Item2,
            };
        }
    }
}