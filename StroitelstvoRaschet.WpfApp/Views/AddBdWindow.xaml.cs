using CocreteCalculator.ViewModels;
using StroitelstvoRaschet.WpfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StroitelstvoRaschet.WpfApp.Views
{
	/// <summary>
	/// Логика взаимодействия для AddBdWindow.xaml
	/// </summary>
	public partial class AddBdWindow : Window
	{
		//public AddBdWindow()
		//{
		//	InitializeComponent();

		//	AddBdWindow addBdWindow = new AddBdWindow(this);

		//	DataContext = addBdWindow; 
		//}
		public AddBdWindow(MainViewModel mainViewModel)
		{
			InitializeComponent();

			DbViewModel dbViewModel = new DbViewModel(this, mainViewModel);

			DataContext = dbViewModel;
		}

	}
}
