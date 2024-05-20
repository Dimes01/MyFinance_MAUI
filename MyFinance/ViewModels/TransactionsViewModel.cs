using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ViewModels;

class TransactionsViewModel : INotifyPropertyChanged
{
	private delegate void MethodForTransaction();
	private long NextID { get; set; } = 1;
	private MethodForTransaction ActionWithTransaction { get; set; }
	private int PositionSelectedTransaction {  get; set; }


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


	private HashSet<string> categories;
	public HashSet<string> Categories
	{
		get => categories;
		set
		{
			if (categories == value) return;
			categories = value;
			OnPropertyChanged(nameof(Categories));
		}
	}


	private bool isMakingTransaction;
	public bool IsMakingTransaction
	{
		get => isMakingTransaction;
		set
		{
			if (isMakingTransaction == value) return;
			isMakingTransaction = value;
			OnPropertyChanged(nameof(IsMakingTransaction));
		}
	}


	private bool isEditingTransaction;
	public bool IsEditingTransaction
	{
		get => isEditingTransaction;
		set
		{
			if (isEditingTransaction == value) return;
			isEditingTransaction = value;
			OnPropertyChanged(nameof(IsEditingTransaction));
		}
	}


	private bool isRemovingTransaction;
	public bool IsRemovingTransaction
	{
		get => isRemovingTransaction;
		set
		{
			if (isRemovingTransaction == value) return;
			isRemovingTransaction = value;
			OnPropertyChanged(nameof(IsRemovingTransaction));
		}
	}


	private Transaction newTransaction;
	public Transaction NewTransaction
	{
		get => newTransaction;
		set
		{
			if (newTransaction == value) return;
			newTransaction = value;
			ActionWithTransaction();
			OnPropertyChanged(nameof(NewTransaction));
		}
	}


	private Transaction selectedTransaction;
	public Transaction SelectedTransaction
	{
		get => selectedTransaction;
		set
		{
			if (selectedTransaction == value) return;
			selectedTransaction = value;
			PositionSelectedTransaction = Transactions.IndexOf(selectedTransaction);
			OnPropertyChanged(nameof(SelectedTransaction));
		}
	}


	public Command AddTransactionCommand => new(c => 
	{ 
		IsMakingTransaction = true;
		ActionWithTransaction = AddTransaction; 
	});
    public Command EditTransactionCommand => new(c => 
	{
		IsEditingTransaction = true;
        ActionWithTransaction = EditTransaction;
    });
    public Command RemoveTransactionCommand => new(c => 
	{
		IsRemovingTransaction = true;
        ActionWithTransaction = RemoveTransaction;
    });


	private void AddTransaction() 
	{
		NewTransaction.Id = NextID++;
		Transactions.Add(NewTransaction);
	}
	private void EditTransaction() => Transactions[PositionSelectedTransaction] = SelectedTransaction;
	private void RemoveTransaction() => Transactions.Remove(SelectedTransaction);


    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
