using CocreteCalculator.Models;
using CocreteCalculator.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CocreteCalculator.ViewModels
{
	public class NewMaterialViewModel : INotifyPropertyChanged
	{
		private NewMaterialWindow _currentWindow;
		private MainViewModel _mainViewModel;
		public NewMaterialViewModel(NewMaterialWindow newMaterialWindow, MainViewModel mainViewModel)
		{
			_currentWindow = newMaterialWindow;
			_mainViewModel = mainViewModel;

			using (var db = new ConcreteCalculatorDbContext())
			{
				var brigadesFromDb = db.Brigades.ToList();
				_brigades = new ObservableCollection<Brigade>(brigadesFromDb);
			}
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
					OnPropertyChanged("Name");
				}
			}
		}

		private ObservableCollection<Brigade> _brigades;
		public ObservableCollection<Brigade> Brigades
		{
			get { return _brigades; }
			set
			{
				_brigades = value;
				OnPropertyChanged("Brigades");
				if (_selectedBrigade != null && _brigades != null)
				{
					var selectedBrigadeId = _selectedBrigade.Id;
					var brigadeIndex = _brigades.Select((brigade, index) => new { Brigade = brigade, Index = index })
											   .FirstOrDefault(item => item.Brigade.Id == selectedBrigadeId)?.Index;
					SelectedIndex = brigadeIndex ?? -1;
				}
			}
		}

		private int _selectedIndex;
		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				if (_selectedIndex != value)
				{
					_selectedIndex = value;
					OnPropertyChanged("SelectedIndex");
				}
			}
		}

		private Brigade _selectedBrigade;
		public Brigade SelectedBrigade
		{
			get { return _selectedBrigade; }
			set
			{
				if (_selectedBrigade != value)
				{
					_selectedBrigade = value;
					OnPropertyChanged("SelectedBrigade");
					if (_selectedBrigade != null && _brigades != null)
					{
						var selectedBrigadeId = _selectedBrigade.Id;
						var brigadeIndex = _brigades.Select((brigade, index) => new { Brigade = brigade, Index = index })
												   .FirstOrDefault(item => item.Brigade.Id == selectedBrigadeId)?.Index;
						SelectedIndex = brigadeIndex ?? -1; // Устанавливаем индекс, если категория найдена, иначе -1
					}
				}
			}
		}

		private RelayCommand cancelCommand;
		private RelayCommand addCommand;

		public RelayCommand NewMaterialButton_Click
		{
			get
			{
				return addCommand ??
				  (addCommand = new RelayCommand(obj =>
				  {
					  using (var db = new ConcreteCalculatorDbContext())
					  {
						  Material newMaterial = new Material
						  {
							  Name = Name,
						  };

						  // Добавляем материал в контекст базы данных
						  db.Materials.Add(newMaterial);

						  // Сохраняем изменения
						  db.SaveChanges();

						  _mainViewModel._materialsList.Add(newMaterial);
						  _mainViewModel.OnPropertyChanged("Materials");

						  // Для каждой выбранной бригады создаем MaterialBrigade и добавляем его в контекст базы данных
						  foreach (var brigadeName in Items)
						  {
							  var brigade = db.Brigades.FirstOrDefault(b => b.Name == brigadeName);
							  if (brigade != null)
							  {
								  var materialBrigade = new MaterialBrigade
								  {
									  MaterialId = newMaterial.Id,
									  BrigadeId = brigade.Id
								  };
								  db.MaterialBrigades.Add(materialBrigade);
							  }
						  }
						  // Сохраняем изменения в связях MaterialBrigade
						  db.SaveChanges();
					  }

					  _currentWindow.Close();
				  }));
			}
		}

		// здесь хранятся Name бригад
		private ObservableCollection<string> _items = new ObservableCollection<string>();
		public ObservableCollection<string> Items
		{
			get { return _items; }
			set
			{
				_items = value;
				OnPropertyChanged("Items");
			}
		}

		private RelayCommand addBrigadeCommand;

		public RelayCommand AddBrigadeButton_Click
		{
			get
			{
				return addBrigadeCommand ??
				  (addBrigadeCommand = new RelayCommand(obj =>
				  {
					  if (SelectedBrigade != null && !Items.Contains(SelectedBrigade.Name))
					  {
						  Items.Add(SelectedBrigade.Name);
					  }
				  }));
			}
		}
	}
}
