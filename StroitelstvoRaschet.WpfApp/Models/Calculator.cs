using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocreteCalculator.Models
{
    public class Calculator
    {
        [Key]
        public int Id { get; set; }
        
        public double MarginPercent { get; set; }

        public double Architector { get; set; }
        public double Constructor { get; set; }
        public double Engineer { get; set; }
        public Material Material { get; set; }
        public Brigade Brigade { get; set; }
        public Equipment Equipment{ get; set; }
        public double TotalCost { get; set; }
    }
}
