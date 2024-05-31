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
	public class MaterialViewModel : INotifyPropertyChanged
	{
		public MaterialViewModel()
		{

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
		public static string GetMonthName(int month)
		{
			return CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[month - 1];
		}

		private RelayCommand calcCommand;

		public RelayCommand CalculateButton_Click
		{
			get
			{
				return calcCommand ??
				  (calcCommand = new RelayCommand(obj =>
				  {

				  }));
			}
		}
	}
}
