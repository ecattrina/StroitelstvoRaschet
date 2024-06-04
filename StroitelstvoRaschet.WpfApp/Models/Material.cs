using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocreteCalculator.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public double Price { get; set; }

        [NotMapped]
        public int Quantity { get; set; }

        [NotMapped]
        public double Total { get; set; }
    }
}
