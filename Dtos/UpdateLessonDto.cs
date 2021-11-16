using System;
using System.ComponentModel.DataAnnotations;

namespace serverAPI.Dtos {
    public record UpdateLessonDto{
        //[Required]
        //public Guid id {get; init;}
        public string name {get; init;}
        public string prophesor{get; init;}
        //public DateTimeOffset date {get; init;}
        public string description {get; init;}
    }
}
