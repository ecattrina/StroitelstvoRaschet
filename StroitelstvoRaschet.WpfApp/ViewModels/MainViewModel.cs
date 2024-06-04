using CocreteCalculator.Models;
using CocreteCalculator.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using OfficeOpenXml;
using System.IO;
using System.Windows;
using StroitelstvoRaschet.WpfApp.Views;
using CalculatorLib;

namespace CocreteCalculator.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
    {
        private readonly MainWindow _currentWindow;
        public MainViewModel(MainWindow mainWindow)
        {
            _currentWindow = mainWindow;

            using (var db = new ConcreteCalculatorDbContext())
            {
                var materialsFromDb = db.Materials.ToList();
                _materialsList = new ObservableCollection<Material>(materialsFromDb);
            }

            using (var db = new ConcreteCalculatorDbContext())
            {
                var brigadesFromDb = db.Brigades.ToList();
                _brigadesList = new ObservableCollection<Brigade>(brigadesFromDb);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

		private RelayCommand _openAddToDbWindowCommand;

		public RelayCommand OpenAddToDbWindowCommand
		{
			 get
    {
        return _openAddToDbWindowCommand ?? (_openAddToDbWindowCommand = new RelayCommand(obj =>
        {
            // Открытие окна AddBdWindow и передача MainViewModel в конструктор
            AddBdWindow addBdWindow = new AddBdWindow(this);
            addBdWindow.Show();
        }));
    }
		}

		public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ICommand mUpdater;

        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                // Code implementation for execution
            }
        }


        

        public ObservableCollection<Material> _materialsList;

        public ObservableCollection<Material> Materials
        {
            get { return _materialsList; }
            set
            {
                _materialsList = value;
                OnPropertyChanged("Categories");
            }
        }

        public ObservableCollection<Brigade> _brigadesList;

        public ObservableCollection<Brigade> Brigades
        {
            get { return _brigadesList; }
            set
            {
                _brigadesList = value;
                OnPropertyChanged("Brigades");
            }
        }

        FirmType firmType = FirmType.Alyans;

        public FirmType FirmType
        {
            get { return firmType; }
            set
            {
                if (firmType == value)
                    return;

                firmType = value;
                OnPropertyChanged("FirmType");
                OnPropertyChanged("IsAlyans");
                OnPropertyChanged("IsMegapolis");
                OnPropertyChanged("IsUmstroy");
                OnPropertyChanged("GetResult");
            }
        }

        public bool IsAlyans
        {
            get { return FirmType == FirmType.Alyans; }
            set { FirmType = value ? FirmType.Alyans : FirmType; }
        }

        public bool IsMegapolis
        {
            get { return FirmType == FirmType.Megapolis; }
            set { FirmType = value ? FirmType.Megapolis : FirmType; }
        }

        public bool IsUmstroy
        {
            get { return FirmType == FirmType.Umstroy; }
            set { FirmType = value ? FirmType.Umstroy : FirmType; }
        }

        public string GetResult
        {
            get
            {
                switch (FirmType)
                {
                    case FirmType.Alyans:
                        return Convert.ToInt32(FirmType.Alyans).ToString();
                    case FirmType.Megapolis:
                        return Convert.ToInt32(FirmType.Megapolis).ToString();
                    case FirmType.Umstroy:
                        return Convert.ToInt32(FirmType.Umstroy).ToString();
                }
                return "";
            }
        }

        private Material _selectedMaterial;

        public Material SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                _selectedMaterial = value;
                OnPropertyChanged("SelectedMaterial");
                // Обновление списка бригад при изменении выбранного материала
                UpdateBrigades();
            }
        }

        private void UpdateBrigades()
        {
            if (SelectedMaterial != null)
            {
                using (var db = new ConcreteCalculatorDbContext())
                {
                    // Фильтрация списка бригад по выбранному материалу
                    Brigades = new ObservableCollection<Brigade>(
                        from materialBrigade in db.MaterialBrigades
                        where materialBrigade.MaterialId == SelectedMaterial.Id
                        select materialBrigade.Brigade);
                }
            }
            else
            {
                // Если материал не выбран, отобразите все доступные бригады
                using (var db = new ConcreteCalculatorDbContext())
                {
                    // Фильтрация списка бригад по выбранному материалу
                    Brigades = new ObservableCollection<Brigade>(
                    db.Brigades);
                }
            }
        }

        private ObservableCollection<Material> _selectedMaterialsList = new ObservableCollection<Material>();

        public ObservableCollection<Material> SelectedMaterialsList
        {
            get { return _selectedMaterialsList; }
            set
            {
                _selectedMaterialsList = value;
                OnPropertyChanged("SelectedMaterialsList");
            }
        }

        private ObservableCollection<Brigade> _selectedBrigadesList = new ObservableCollection<Brigade>();
        public ObservableCollection<Brigade> SelectedBrigadesList
        {
            get { return _selectedBrigadesList; }
            set
            {
                _selectedBrigadesList = value;
                OnPropertyChanged("SelectedBrigadesList");
            }
        }

        private Brigade _selectedBrigade;
        public Brigade SelectedBrigade
        {
            get { return _selectedBrigade; }
            set
            {
                _selectedBrigade = value;
                OnPropertyChanged("SelectedBrigade");
            }
        }

    
		private RelayCommand _addCommand;
		public RelayCommand AddButton_Click
		{
			get
			{
				return _addCommand ?? (_addCommand = new RelayCommand(obj =>
				{
					// Проверка, что выбран материал
					if (SelectedMaterial != null && SelectedBrigade != null)
					{
						// Проверка, что выбранный материал еще не добавлен в список
						if (!SelectedMaterialsList.Any(material => material.Name == SelectedMaterial.Name))
						{
							// Добавляем выбранный материал в список
							SelectedMaterialsList.Add(new Material
							{
								Name = SelectedMaterial.Name,
								Price = Price,
								Quantity = Quantity,
								Total = Price * Quantity
							});
						}

						// Проверка, что выбранная бригада еще не добавлена в список
						if (!SelectedBrigadesList.Any(brigade => brigade.Name == SelectedBrigade.Name))
						{
							// Добавляем выбранную бригаду в список
							SelectedBrigadesList.Add(new Brigade
							{
								Name = SelectedBrigade.Name,
								Rate = Rate,
								DaysCount = DaysCount,
								Total = Rate * DaysCount
							});
						}
						else
						{
							// Если бригада уже есть в списке, найдем ее и обновим ее значения
							var existingBrigade = SelectedBrigadesList.First(brigade => brigade.Name == SelectedBrigade.Name);
							existingBrigade.Rate = Rate;
							existingBrigade.DaysCount = DaysCount;
							existingBrigade.Total = Rate * DaysCount;
						}
					}
					else
					{
						MessageBox.Show("Не выбрана бригада или материал.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
				}));
			}
		}


		private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        private int _rate;
        public int Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                OnPropertyChanged("Rate");
            }
        }

        private int _daysCount;
        public int DaysCount
        {
            get { return _daysCount; }
            set
            {
                _daysCount = value;
                OnPropertyChanged("DaysCount");
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private int _total;
        public int Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }

        //итоговая стоимость проекта
        private double _totalCost;
        public double TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                OnPropertyChanged("TotalCost");
            }
        }

        private int _profitPercent;
        public int ProfitPercent
        {
            get { return _profitPercent; }
            set
            {
                _profitPercent = value;
                OnPropertyChanged("ProfitPercent");
            }
        }

        private int _architectureCost;
        public int ArchitectureCost
        {
            get { return _architectureCost; }
            set
            {
                _architectureCost = value;
                OnPropertyChanged("ArchitectureCost");
            }
        }

        private int _constructorCost;
        public int ConstructorCost
        {
            get { return _constructorCost; }
            set
            {
                _constructorCost = value;
                OnPropertyChanged("ConstructorCost");
            }
        }

        private int _engineerCost;
        public int EngineerCost
        {
            get { return _engineerCost; }
            set
            {
                _engineerCost = value;
                OnPropertyChanged("EngineerCost");
            }
        }

        private int _apartmentArea;
        public int ApartmentArea
        {
            get { return _apartmentArea; }
            set
            {
                _apartmentArea = value;
                OnPropertyChanged("ApartmentArea");
            }
        }

        private RelayCommand _calculateCommand;
        public RelayCommand CalculateButton_Click
        {
            get
            {
                return _calculateCommand ?? (_calculateCommand = new RelayCommand(obj =>
                {
                    List<double> materialCosts = _selectedMaterialsList.Select(material => material.Price).ToList();
                    List<int> materialQuantities = _selectedMaterialsList.Select(material => material.Quantity).ToList();

                    List<double> brigadesRates = _selectedBrigadesList.Select(material => material.Rate).ToList();
                    List<int> brigadesQuantities = _selectedBrigadesList.Select(brigade => brigade.DaysCount).ToList();

                    CalculatorEngine calculatorEngine = new CalculatorEngine(
                        materialCosts,
                        materialQuantities,
                        brigadesRates,
                        brigadesQuantities,
                        ((int)firmType),
                        ProfitPercent,
                        ArchitectureCost,
                        ConstructorCost,
                        EngineerCost,
                        ApartmentArea
                    );
                    
                    TotalCost = calculatorEngine.CalculateTotalCost();
                }));
            }
        }

        private RelayCommand _exportCommand;
        public RelayCommand ExportButton_Click
        {
            get
            {
                return _exportCommand ?? (_exportCommand = new RelayCommand(obj =>
                {
                    List<double> materialCosts = _selectedMaterialsList.Select(material => material.Price).ToList();
                    List<int> materialQuantities = _selectedMaterialsList.Select(material => material.Quantity).ToList();

                    List<double> brigadesRates = _selectedBrigadesList.Select(material => material.Rate).ToList();
                    List<int> brigadesQuantities = _selectedBrigadesList.Select(brigade => brigade.DaysCount).ToList();

                    CalculatorEngine calculatorEngine = new CalculatorEngine(
                        materialCosts,
                        materialQuantities,
                        brigadesRates,
                        brigadesQuantities,
                        ((int)firmType),
                        ProfitPercent,
                        ArchitectureCost,
                        ConstructorCost,
                        EngineerCost,
                        ApartmentArea
                    );

                    double totalMaterialCost = 0;
                    double totalBrigadeCost = 0;
                    double totalWorkPayment = 0;
                    double companyIncome = 0;

                    double totalCost = calculatorEngine.CalculateAll(
                        ref totalMaterialCost, ref totalBrigadeCost, ref totalWorkPayment, ref companyIncome
                    );

                    try
                    {
                        // Создаем новый Excel пакет
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        ExcelPackage excelPackage = new ExcelPackage();

                        // Добавляем лист в книгу Excel
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Данные");

                        // Записываем данные в ячейки Excel
                        worksheet.Cells["A1"].Value = "Стоимость материалов";
                        worksheet.Cells["B1"].LoadFromCollection(_selectedMaterialsList.Select(material => material.Price));

                        worksheet.Cells["A2"].Value = "Количество материалов";
                        worksheet.Cells["B2"].LoadFromCollection(_selectedMaterialsList.Select(material => material.Quantity));

                        worksheet.Cells["A3"].Value = "Ставки бригад";
                        worksheet.Cells["B3"].LoadFromCollection(_selectedBrigadesList.Select(material => material.Rate));

                        worksheet.Cells["A4"].Value = "Количество дней работы бригад";
                        worksheet.Cells["B4"].LoadFromCollection(_selectedBrigadesList.Select(brigade => brigade.DaysCount));

                        worksheet.Cells["A5"].Value = "Застройщик";
                        worksheet.Cells["B5"].Value = GetFirmName;

                        worksheet.Cells["A6"].Value = "Процент прибыли";
                        worksheet.Cells["B6"].Value = ProfitPercent;

                        worksheet.Cells["A7"].Value = "Стоимость архитектора";
                        worksheet.Cells["B7"].Value = ArchitectureCost;

                        worksheet.Cells["A8"].Value = "Стоимость конструктора";
                        worksheet.Cells["B8"].Value = ConstructorCost;

                        worksheet.Cells["A9"].Value = "Стоимость инженера";
                        worksheet.Cells["B9"].Value = EngineerCost;

                        worksheet.Cells["A10"].Value = "Площадь квартиры";
                        worksheet.Cells["B10"].Value = ApartmentArea;

                        worksheet.Cells["A11"].Value = "Стоимость материалов (общая)";
                        worksheet.Cells["B11"].Value = totalMaterialCost;


                        worksheet.Cells["A12"].Value = "Оплата бригады (общая)";
                        worksheet.Cells["B12"].Value = totalBrigadeCost;

             
                        worksheet.Cells["A13"].Value = "Прибыль фирмы";
                        worksheet.Cells["B13"].Value = companyIncome;

                        worksheet.Cells["A14"].Value = "Оплата работников (общая)";
                        worksheet.Cells["B14"].Value = totalWorkPayment;

                        
                        worksheet.Cells["A15"].Value = "Общая стоимость";
                        worksheet.Cells["B15"].Value = totalCost;


						worksheet.Cells["A17"].Value = "Оплата работников (общая), % от суммы";
						worksheet.Cells["B17"].Value = 100 * totalWorkPayment / totalCost;

						worksheet.Cells["A18"].Value = "Стоимость бригады (общая), % от суммы";
						worksheet.Cells["B18"].Value = 100 * totalBrigadeCost / totalCost;


						worksheet.Cells["A19"].Value = "Стоимость материалов (общая), % от суммы";
						worksheet.Cells["B19"].Value = 100 * totalMaterialCost / totalCost;

						worksheet.Cells["A20"].Value = "Прибыль фирмы, % от суммы";
						worksheet.Cells["B20"].Value = 100 * companyIncome / totalCost;



						// Сохраняем файл Excel
						var dialog = new Microsoft.Win32.SaveFileDialog();
                        dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        dialog.FileName = "ExportedData.xlsx";

                        if (dialog.ShowDialog() == true)
                        {
                            FileInfo excelFile = new FileInfo(dialog.FileName);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Данные успешно экспортированы в Excel!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Произошла ошибка при экспорте данных в Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }
        public string GetFirmName
        {
            get
            {
                switch (FirmType)
                {
                    case FirmType.Alyans:
                        return "Альянс";
                    case FirmType.Megapolis:
                        return "Мегаполис";
                    case FirmType.Umstroy:
                        return "Умстрой";
                }
                return "";
            }
        }
    }
}
