using CocreteCalculator.ViewModels;
using System.Windows;

namespace CocreteCalculator.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			MainViewModel mainViewModel = new MainViewModel(this);

			DataContext = mainViewModel;
		}


	}

	
}
