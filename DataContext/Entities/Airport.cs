using System.ComponentModel.DataAnnotations;

namespace DataContext.Entities
{
    public class Airport
    {
        [Key]
        public string Faa { get; set; }

        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Alt { get; set; }
        public double Tz { get; set; }
        public string Dst { get; set; }
        public string Tzone { get; set; }
    }
}