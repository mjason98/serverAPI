using System;
using System.ComponentModel.DataAnnotations;

namespace serverAPI.Dtos {
    public record CreateLessonDto{
        [Required]
        public string name {get; init;}
        [Required]
        public string prophesor{get; init;}
        [Required]
        public DateTimeOffset date {get; init;}
        public string description {get; init;}
    }
}
