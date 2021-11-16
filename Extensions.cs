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
    }
}