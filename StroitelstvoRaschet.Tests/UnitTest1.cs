using System;
using NUnit.Framework;

namespace StroitelstvoRaschet.Tests
{
	public class Tests
	{

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void MaterialCostTest_ValidInput()
		{
			const double K = 50;
			const double M = 2000;
			const double expected = 100000;

			var result = StroitelstvoRaschet.Library.Calculate.MaterialCost(K, M);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void MaterialCostTest_InvalidK()
		{
			const double K = 0;
			const double M = 2000;

			Assert.Throws<Exception>(() => StroitelstvoRaschet.Library.Calculate.MaterialCost(K, M));
		}

		[Test]
		public void MaterialCostTest_InvalidM()
		{
			const double K = 50;
			const double M = 0;

			Assert.Throws<Exception>(() => StroitelstvoRaschet.Library.Calculate.MaterialCost(K, M));
		}
	}
}