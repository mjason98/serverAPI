using System.ComponentModel.DataAnnotations;

namespace serverAPI.Entities {
    public record Profesor {
        public int Id {get; set;}
        [StringLength(60)]
        public string name {get; set;}
   }    
}