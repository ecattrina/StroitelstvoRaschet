using NUnit.Framework;
using CalculatorLib;
using System.Collections.Generic;

namespace CalculatorLib.Tests
{
	[TestFixture]
	public class CalculatorEngineTests
	{
		[Test]
		public void CalculateTotalCost_ShouldReturnCorrectValue()
		{
			// Arrange
			var materialCosts = new List<double> { 100, 200 };
			var materialQuantities = new List<int> { 2, 3 };
			var brigadeRates = new List<double> { 150, 250 };
			var brigadeDaysCounts = new List<int> { 4, 5 };
			double firmPrice = 30000;
			double profitPercent = 10;
			double architectorCost = 500;
			double constructorCost = 400;
			double engineerCost = 300;
			double apartmentArea = 150;

			var calculator = new CalculatorEngine(
				materialCosts,
				materialQuantities,
				brigadeRates,
				brigadeDaysCounts,
				firmPrice,
				profitPercent,
				architectorCost,
				constructorCost,
				engineerCost,
				apartmentArea
			);

			// Act
			double totalCost = calculator.CalculateTotalCost();

			// Assert
			double expectedTotalMaterialCost = 100 * 2 + 200 * 3;
			double expectedTotalBrigadeCost = 150 * 4 + 250 * 5;
			double expectedFirmPrice = firmPrice + firmPrice * profitPercent / 100;
			double expectedWorkerPaymentByArea = (architectorCost + constructorCost + engineerCost) * apartmentArea;
			double expectedTotalCost = expectedTotalMaterialCost + expectedTotalBrigadeCost + expectedFirmPrice + expectedWorkerPaymentByArea;

			Assert.AreEqual(expectedTotalCost, totalCost, 0.001);
		}

		[Test]
		public void CalculateAll_ShouldReturnCorrectValues()
		{
			// Arrange
			var materialCosts = new List<double> { 100, 200 };
			var materialQuantities = new List<int> { 2, 3 };
			var brigadeRates = new List<double> { 150, 250 };
			var brigadeDaysCounts = new List<int> { 4, 5 };
			double firmPrice = 30000;
			double profitPercent = 10;
			double architectorCost = 500;
			double constructorCost = 400;
			double engineerCost = 300;
			double apartmentArea = 150;

			var calculator = new CalculatorEngine(
				materialCosts,
				materialQuantities,
				brigadeRates,
				brigadeDaysCounts,
				firmPrice,
				profitPercent,
				architectorCost,
				constructorCost,
				engineerCost,
				apartmentArea
			);

			double totalMaterialCost = 0;
			double totalBrigadeCost = 0;
			double totalWorkPayment = 0;
			double companyIncome = 0;

			// Act
			double totalCost = calculator.CalculateAll(ref totalMaterialCost, ref totalBrigadeCost, ref totalWorkPayment, ref companyIncome);

			// Assert
			double expectedTotalMaterialCost = 100 * 2 + 200 * 3;
			double expectedTotalBrigadeCost = 150 * 4 + 250 * 5;
			double expectedCompanyIncome = firmPrice + firmPrice * profitPercent / 100;
			double expectedWorkerPaymentByArea = (architectorCost + constructorCost + engineerCost) * apartmentArea;
			double expectedTotalCost = expectedTotalMaterialCost + expectedTotalBrigadeCost + expectedCompanyIncome + expectedWorkerPaymentByArea;

			Assert.AreEqual(expectedTotalMaterialCost, totalMaterialCost, 0.001);
			Assert.AreEqual(expectedTotalBrigadeCost, totalBrigadeCost, 0.001);
			Assert.AreEqual(expectedCompanyIncome, companyIncome, 0.001);
			Assert.AreEqual(expectedWorkerPaymentByArea, totalWorkPayment, 0.001);
			Assert.AreEqual(expectedTotalCost, totalCost, 0.001);
		}
	}
}
