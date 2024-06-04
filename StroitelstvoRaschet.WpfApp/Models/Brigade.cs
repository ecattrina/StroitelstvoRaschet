using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocreteCalculator.Models
{
    public class Brigade
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public double Rate { get; set; }

        [NotMapped]
        public int DaysCount { get; set; }

        [NotMapped]
        public double Total { get; set; }
    }
}
