using System.ComponentModel.DataAnnotations;

namespace serverAPI.Dtos{
    public record DaysDto{
        public int day {get; init;}
        public int n {get; init;}
    }

    public record DayMonthYearDto{
        [Required]
        public int month {get; init;}
        [Required]
        public int year {get; init;}
        [Required]
        public int day {get; init;}
    }
}