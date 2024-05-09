using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ViewModels;

abstract class TransactionsViewModel : INotifyPropertyChanged
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

	public abstract Command AddTransactionCommand { get; }
    public abstract Command EditTransactionCommand { get; }
    public abstract Command RemoveTransactionCommand { get; }


    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
