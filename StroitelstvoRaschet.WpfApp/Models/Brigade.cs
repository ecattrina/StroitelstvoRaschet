using FinancialManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocreteCalculator.Models
{
	public class Brigade
	{
		public string Name { get; set; }
		public List<Material> Materials { get; set; }
		public double CostPerDay { get; set; }
		public int DaysWorked { get; set; }
	}
}
