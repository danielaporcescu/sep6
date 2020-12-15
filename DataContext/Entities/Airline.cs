using System.ComponentModel.DataAnnotations;

namespace DataContext.Entities
{
    public class Airline
    {
        [Key]
        public string Carrier { get; set; }

        public string Name { get; set; }
    }
}