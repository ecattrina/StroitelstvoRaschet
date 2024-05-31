using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroitelstvoRaschet.Library
{
	public class Calculate
	{
		public static double MaterialCost(double K, double M)
		{
			if (K <= 0)
			{
				throw new Exception("Введите стоимость единицы материала");
			}
			if (M <= 0)
			{
				throw new Exception("Введите количество материала");
			}
			double materialCost = K * M;
			return materialCost;
		}

		public static double LaborCost(double R, double T)
		{
			if (R <= 0)
			{
				throw new Exception("Введите ставку оплаты труда рабочего в час");
			}
			if (T <= 0)
			{
				throw new Exception("Введите количество часов");
			}
			double laborCost = R * T;
			return laborCost;
		}

		public static double EquipmentRentCost(double O, double A)
		{
			if (O <= 0)
			{
				throw new Exception("Введите стоимость аренды оборудования в день");
			}
			if (A <= 0)
			{
				throw new Exception("Введите количество дней");
			}
			double rentCost = O * A;
			return rentCost;
		}

		public static double CompanyIncome(double S, double Pr)
		{
			if (S <= 0)
			{
				throw new Exception("Введите сумму контракта");
			}
			if (Pr <= 0)
			{
				throw new Exception("Введите процент прибыли, который компания хочет получить");
			}
			double income = S * Pr;
			return income;
		}

		public static double WorkerPaymentByArea(double Ar, double Ko, double In, double De, double Sr)
		{
			if (Ar < 0)
			{
				throw new Exception("Введите корректную оплату для архитектора");
			}
			if (Ko < 0)
			{
				throw new Exception("Введите корректную оплату для конструктора");
			}
			if (In < 0)
			{
				throw new Exception("Введите корректную оплату для инженера");
			}
			if (De < 0)
			{
				throw new Exception("Введите корректную оплату для дизайнера интерьера");
			}
			if (Sr <= 0)
			{
				throw new Exception("Введите планируемую площадь дома");
			}
			double workerPayment = (Ar + Ko + In + De) * Sr;
			return workerPayment;
		}

		public static double TotalCost(double K, double M, double R, double T, double O, double A, double S, double Pr, double Ar, double Ko, double In, double De, double Sr)
		{	
			double materialCost = MaterialCost(K, M);
			double laborCost = LaborCost(R, T) + WorkerPaymentByArea(Ar, Ko, In, De, Sr);
			double rentCost = EquipmentRentCost(O, A);
			double companyIncome = CompanyIncome(S, Pr);
			double totalCost = materialCost + laborCost + rentCost + companyIncome;
			return totalCost;
		}


	}

}
