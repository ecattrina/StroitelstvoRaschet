using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Models
{
	public class Material
	{
		public string Name { get; set; }
		public string Brigade { get; set; }
		public double CostPerUnit { get; set; }
		public double Quantity { get; set; }
	}
}
