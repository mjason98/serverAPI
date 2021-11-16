using System;

namespace serverAPI.Entities {
    public record TopicDto {
        public int id {get; init;}
        public string name {get; init;}
    }
}