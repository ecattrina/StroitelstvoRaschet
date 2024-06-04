using CocreteCalculator.ViewModels;
using CocreteCalculator;
using StroitelstvoRaschet.WpfApp.Views;
using System.Windows;
using CocreteCalculator.Views;

public class DbViewModel
{
	private Window _currentWindow;
	private MainViewModel _mainViewModel;

	public DbViewModel(Window currentWindow, MainViewModel mainViewModel)
	{
		_currentWindow = currentWindow;
		_mainViewModel = mainViewModel;
	}

	private RelayCommand newMaterialCommand;
	public RelayCommand NewMaterialButton_Click
	{
		get
		{
			return newMaterialCommand ??
			  (newMaterialCommand = new RelayCommand(obj =>
			  {
				  NewMaterialWindow newMaterialWindow = new NewMaterialWindow(_mainViewModel);
				  newMaterialWindow.Show();
				  _currentWindow.Hide();
			  }));
		}
	}

	private RelayCommand newBrigadeCommand;
	public RelayCommand NewBrigadeButton_Click
	{
		get
		{
			return newBrigadeCommand ??
			  (newBrigadeCommand = new RelayCommand(obj =>
			  {
				  NewBrigadeWindow newBrigadeWindow = new NewBrigadeWindow(_mainViewModel);
				  newBrigadeWindow.Show();
				  _currentWindow.Hide();
			  }));
		}
	}
}
