using System;
using System.ComponentModel.DataAnnotations;

namespace serverAPI.Dtos {
    public record UpdateLessonDto{
        //[Required]
        //public Guid id {get; init;}
        public int name {get; init;}
        public int prophesor{get; init;}
        public DateTimeOffset dateIni {get; init;}
        public DateTimeOffset dateFin {get; init;}
        public string description {get; init;}
    }
}
