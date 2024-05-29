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


    private ObservableCollection<Transaction> transactions = [];
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


	private Transaction actionTransaction;
	public Transaction ActionTransaction
	{
		get => actionTransaction;
		set
		{
			if (actionTransaction == value) return;
			actionTransaction = value;
			ActionWithTransaction();
			IsMakingTransaction = IsEditingTransaction = IsRemovingTransaction = false;
			OnPropertyChanged(nameof(ActionTransaction));
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
		ActionWithTransaction = AddTransaction; 
		IsMakingTransaction = true;
	});
    public Command EditTransactionCommand => new(c => 
	{
        ActionWithTransaction = EditTransaction;
		IsEditingTransaction = true;
    });
    public Command RemoveTransactionCommand => new(c => 
	{
        ActionWithTransaction = RemoveTransaction;
		IsRemovingTransaction = true;
    });


	private void AddTransaction() 
	{
		ActionTransaction.Id = NextID++;
		Transactions.Add(ActionTransaction);
	}
    private void EditTransaction()
    {
        if (ActionTransaction is not null)
			Transactions[PositionSelectedTransaction] = ActionTransaction;
    }
    private void RemoveTransaction()
    {
        if (ActionTransaction is not null)
			Transactions.Remove(ActionTransaction);
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
