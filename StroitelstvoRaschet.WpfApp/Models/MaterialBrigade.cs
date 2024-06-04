using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocreteCalculator.Models
{
    public class MaterialBrigade
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Material")]
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        [ForeignKey("Brigade")]
        public int BrigadeId { get; set; }
        public virtual Brigade Brigade { get; set; }
    }
}
