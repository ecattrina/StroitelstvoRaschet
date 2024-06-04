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
    public partial class NewBrigadeWindow : Window
    {
        public NewBrigadeWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();

            NewBrigadeViewModel newBrigadeViewModel = new NewBrigadeViewModel(this, mainViewModel);

            DataContext = newBrigadeViewModel;
        }
		private void NewBrigadeButton_Click(object sender, RoutedEventArgs e)
		{
			// Логика добавления бригады

			// Скрыть окно, но не завершать приложение
			this.Hide();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Отменить закрытие окна
			e.Cancel = true;

			// Скрыть окно вместо закрытия
			this.Hide();
		}
	}
}
