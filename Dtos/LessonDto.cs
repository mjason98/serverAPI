using System;
using System.ComponentModel.DataAnnotations;

namespace serverAPI.Dtos {
    public record LessonDto{
        public int id {get; init;}
        public int name {get; init;}
        public int prophesor{get; init;}
        public DateTimeOffset dateIni {get; init;}
        public DateTimeOffset dateFin {get; init;}
        public string description {get; init;}
    }

    public record LessonEDto{
        public int id {get; init;}
        public string name {get; init;}
        public string prophesor{get; init;}
        public DateTimeOffset dateIni {get; init;}
        public DateTimeOffset dateFin {get; init;}
        public string description {get; init;}
    }

    public record MonthYearDto{
        [Required]
        public int month {get; init;}
        [Required]
        public int year {get; init;}
    }
}
