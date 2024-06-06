using Microsoft.EntityFrameworkCore;
using System;

namespace CocreteCalculator.Models
{
	public class ConcreteCalculatorDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			try
			{
				optionsBuilder.UseSqlite("Data Source=D:\\всеподряд\\курсовые\\StroitelstvoRaschet\\StroitelstvoRaschet.WpfApp\\database.db");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ошибка при открытии базы данных: " + ex.Message);
			}
		}

		public DbSet<Brigade> Brigades { get; set; }
		public DbSet<Material> Materials { get; set; }
		public DbSet<MaterialBrigade> MaterialBrigades { get; set; }
	}
}
