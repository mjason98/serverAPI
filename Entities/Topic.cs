using System;
using System.ComponentModel.DataAnnotations;

namespace serverAPI.Entities {
    public record Topic {
        public int Id {get; set;}
        [StringLength(30)]
        public string name {get; set;}
    }
}