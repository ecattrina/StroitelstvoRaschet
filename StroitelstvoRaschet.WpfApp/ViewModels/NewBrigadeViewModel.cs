using CocreteCalculator.Views;
using CocreteCalculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;


namespace CocreteCalculator.ViewModels
{
    public class NewBrigadeViewModel
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

                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
                          _mainViewModel.OnPropertyChanged("Brigades");
                      }

                      _currentWindow.Close();
                  }));
            }
        }
    }
}
