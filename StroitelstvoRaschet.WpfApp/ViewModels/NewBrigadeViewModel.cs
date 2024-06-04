using CocreteCalculator.Views;
using CocreteCalculator.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CocreteCalculator.ViewModels
{
	public class NewBrigadeViewModel : INotifyPropertyChanged
	{
		private NewBrigadeWindow _currentWindow;
		private MainViewModel _mainViewModel;

		public NewBrigadeViewModel(NewBrigadeWindow newBrigadeWindow, MainViewModel mainViewModel)
		{
			_currentWindow = newBrigadeWindow;
			_mainViewModel = mainViewModel;
		}

		public RelayCommand CancelButton_Click
		{
			get
			{
				return cancelCommand ??
				  (cancelCommand = new RelayCommand(obj =>
				  {
					  _currentWindow.Close();
				  }));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					_name = value;
					OnPropertyChanged(nameof(Name));
				}
			}
		}

		private RelayCommand cancelCommand;
		private RelayCommand addCommand;

		public RelayCommand NewBrigadeButton_Click
		{
			get
			{
				return addCommand ??
				  (addCommand = new RelayCommand(obj =>
				  {
					  using (var db = new ConcreteCalculatorDbContext())
					  {
						  Brigade newBrigade = new Brigade
						  {
							  Name = Name,
						  };

						  // Добавляем категорию в контекст базы данных
						  db.Brigades.Add(newBrigade);

						  // Сохраняем изменения
						  db.SaveChanges();

						  _mainViewModel._brigadesList.Add(newBrigade);
						  _mainViewModel.OnPropertyChanged(nameof(_mainViewModel.Brigades));
					  }

					  _currentWindow.Close();
				  }));
			}
		}
	}
}
