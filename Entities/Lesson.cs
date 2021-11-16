using System;

namespace serverAPI.Entities {
    public record Lesson {
        public Guid id {get; init;}
        public string name {get; init;}
        public string prophesor{get; init;}
        public DateTimeOffset date {get; init;}
        public string description {get; init;}
    }
}