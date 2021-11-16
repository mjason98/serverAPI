using serverAPI.Entities;
using serverAPI.Dtos;

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
    }
}