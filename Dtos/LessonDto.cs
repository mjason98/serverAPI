using System;

namespace serverAPI.Dtos {
    public record LessonDto{
        public int id {get; init;}
        public int name {get; init;}
        public int prophesor{get; init;}
        public DateTimeOffset dateIni {get; init;}
        public DateTimeOffset dateFin {get; init;}
        public string description {get; init;}
    }
}
