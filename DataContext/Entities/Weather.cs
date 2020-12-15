using System;
using System.ComponentModel.DataAnnotations;

namespace DataContext.Entities
{
    public class Weather
    {
        [Key]
        public int Id { get; set; }

        public string Origin { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public double? Temp { get; set; }
        public double? Dewp { get; set; }
        public double? Humid { get; set; }
        public int? Wind_Dir { get; set; }
        public double? Wind_Speed { get; set; }
        public double? Wind_Gust { get; set; }
        public double Precip { get; set; }
        public double? Pressure { get; set; }
        public double? Visib { get; set; }
        public DateTime Time_Hour { get; set; }
    }
}