using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocreteCalculator.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public double RentPerDay { get; set; }
        public int DaysRented { get; set; }
    }
}
