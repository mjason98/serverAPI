using System;

namespace serverAPI.Entities {
    public record Topic {
        public int id {get; init;}
        public string name {get; init;}
    }
}