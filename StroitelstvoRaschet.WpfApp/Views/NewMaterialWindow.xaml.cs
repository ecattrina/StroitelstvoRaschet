using FinancialManagement.Models;
using FinancialManagement.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CocreteCalculator
{
	/// <summary>
	/// Interaction logic for NewMaterialWindow.xaml
	/// </summary>
	public partial class NewMaterialWindow : Window
	{
		public NewMaterialWindow()
		{
			InitializeComponent();

			MaterialViewModel materialViewModel = new MaterialViewModel();

			DataContext = materialViewModel;
		}
	}
}
