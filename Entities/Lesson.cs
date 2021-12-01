using System;
using System.ComponentModel.DataAnnotations;

namespace serverAPI.Entities {
    public record Lesson {
        public int Id {get; set;}
        public int TopicId {get; set;}
        public int ProfesorId{get; set;}
        public DateTimeOffset dateIni {get; set;}
        public DateTimeOffset dateFin {get; set;}
        [StringLength(256)]
        public string description {get; set;}

        public Topic Topic {get; set;}
        public Profesor Profesor {get; set;}
    }
    public record LessonE : Lesson {
        public string nameS {get; set;}
        public string prophesorS{get; set;}
    }
}