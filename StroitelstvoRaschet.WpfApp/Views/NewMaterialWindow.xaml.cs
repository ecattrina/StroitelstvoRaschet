using CocreteCalculator.Models;
using CocreteCalculator.ViewModels;
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

namespace CocreteCalculator.Views
{
    /// <summary>
    /// Interaction logic for NewMaterialWindow.xaml
    /// </summary>
    public partial class NewMaterialWindow : Window
    {
        private NewMaterialWindow _currentWindow;
        private MainViewModel _mainViewModel;
        public NewMaterialWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();

            DataContext = new NewMaterialViewModel(this, mainViewModel);
        }
    }
}
