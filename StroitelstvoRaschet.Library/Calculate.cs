using System.Numerics;

namespace CalculatorLib
{
	public class CalculatorEngine
	{
		private List<double> _materialCosts;
		private List<int> _materialQuantities;
		private List<double> _brigadeRates;
		private List<int> _brigadeDaysCounts;
		private double _firmPrice;
		private double _profitPercent;
		private double _architectorCost;
		private double _constructorCost;
		private double _engineerCost;
		private double _apartmentArea;
		public CalculatorEngine(List<double> materialCosts,
			List<int> materialQuantities,
			List<double> brigadeRates,
			List<int> brigadeDaysCounts,
			double firmPrice,
			double profitPercent,
			double architectorCost,
			double constructorCost,
			double engineerCost,
			double apartmentArea
		)
		{
			_materialCosts = materialCosts;
			_materialQuantities = materialQuantities;
			_brigadeRates = brigadeRates;
			_brigadeDaysCounts = brigadeDaysCounts;
			_firmPrice = firmPrice;
			_profitPercent = profitPercent;
			_architectorCost = architectorCost;
			_constructorCost = constructorCost;
			_engineerCost = engineerCost;
			_apartmentArea = apartmentArea;
		}
		public double CalculateTotalCost()
		{
			double totalMaterialCost = 0;
			double totalBrigadeCost = 0;

			for (int i = 0; i < _materialCosts.Count; i++)
			{
				totalMaterialCost += MaterialCost(_materialCosts[i], _materialQuantities[i]);
			}

			for (int i = 0; i < _brigadeRates.Count; i++)
			{
				totalBrigadeCost += BrigadeCost(_brigadeRates[i], _brigadeDaysCounts[i]);
			}

			double totalFirmPrice = CompanyIncome(_firmPrice, _profitPercent);

			double totalWorkerPaymentByArea = WorkerPaymentByArea(
				_architectorCost,
				_constructorCost,
				_engineerCost,
				_apartmentArea
			);

			return totalMaterialCost + totalBrigadeCost + totalFirmPrice + totalWorkerPaymentByArea;
		}

		public double CalculateAll(ref double totalMaterialCost, ref double totalBrigadeCost, ref double totalWorkPayment, ref double companyIncome)
		{
			for (int i = 0; i < _materialCosts.Count; i++)
			{
				totalMaterialCost += MaterialCost(_materialCosts[i], _materialQuantities[i]);
			}

			for (int i = 0; i < _brigadeRates.Count; i++)
			{
				totalBrigadeCost += BrigadeCost(_brigadeRates[i], _brigadeDaysCounts[i]);
			}

			companyIncome = CompanyIncome(_firmPrice, _profitPercent);

			totalWorkPayment = WorkerPaymentByArea(
				_architectorCost,
				_constructorCost,
				_engineerCost,
				_apartmentArea
			);

			return totalMaterialCost + totalBrigadeCost + companyIncome + totalWorkPayment;
		}
		public double MaterialCost(double cost, double quantity)
		{
			return cost * quantity;
		}
		public double BrigadeCost(double rate, double daysCount)
		{
			return rate * daysCount;
		}

		public static double CompanyIncome(double firmPrice, double profitPercent)
		{
			return firmPrice * profitPercent / 100 + firmPrice;
		}

		public double WorkerPaymentByArea(double architectorCost, double constructorCost, double engineerCost, double apartmentArea)
		{
			return (architectorCost + constructorCost + engineerCost) * apartmentArea;
		}
		
	}
}
