using StroitelstvoRaschet.WpfApp;
using Microsoft.EntityFrameworkCore;

namespace CocreteCalculator.Models
{
    public class ConcreteCalculatorDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\всеподряд\\курсовые\\StroitelstvoRaschet\\StroitelstvoRaschet.WpfApp\\database.db");
        }

        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<Calculator> Calculators { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialBrigade> MaterialBrigades { get; set; }

    }
}
