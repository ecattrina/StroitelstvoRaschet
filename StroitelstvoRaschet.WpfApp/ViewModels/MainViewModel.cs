using CocreteCalculator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace FinancialManagement.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private readonly MainWindow _currentWindow;
		public MainViewModel(MainWindow mainWindow)
		{
			_currentWindow = mainWindow;
		}

		public event PropertyChangedEventHandler PropertyChanged;

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

		private RelayCommand newMaterialCommand;

		public RelayCommand NewMaterialButton_Click
		{
			get
			{
				return newMaterialCommand ??
				  (newMaterialCommand = new RelayCommand(obj =>
				  {
					  NewMaterialWindow newMaterialWindow = new NewMaterialWindow();
					  newMaterialWindow.Show();
					  _currentWindow.Close();
				  }));
			}
		}
	}
}
