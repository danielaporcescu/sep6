using System;

namespace Services.Models.Common
{
    public class DateValueCounted
    {
        public DateTime Date { get; set; }

        public double? Value { get; set; }
        public int Count { get; set; } = 1;
    }
}