using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ViewModels;

class MainPageViewModel : INotifyPropertyChanged
{


	private ObservableCollection<Transaction> transactions;
	public ObservableCollection<Transaction> Transactions
	{
		get => transactions;
		set
		{
			if (transactions == value) return;
			transactions = value;
			OnPropertyChanged(nameof(Transactions));
		}
	}

	public Command DetailCommand => new(f => { }, f => true);

	public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
