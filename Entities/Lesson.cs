using System;

namespace serverAPI.Entities {
    public record Lesson {
        public int id {get; init;}
        public int name {get; init;}
        public int prophesor{get; init;}
        public DateTimeOffset dateIni {get; init;}
        public DateTimeOffset dateFin {get; init;}
        public string description {get; init;}
    }
    public record LessonE : Lesson {
        public string nameS {get; init;}
        public string prophesorS{get; init;}
    }
}