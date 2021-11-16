using System;
using System.ComponentModel.DataAnnotations;

namespace serverAPI.Dtos {
    public record CreateLessonDto{
        [Required]
        public int name {get; init;}
        [Required]
        public int prophesor{get; init;}
        [Required]
        public DateTimeOffset dateIni {get; init;}
        [Required]
        public DateTimeOffset dateFin {get; init;}
        public string description {get; init;}
    }
}
