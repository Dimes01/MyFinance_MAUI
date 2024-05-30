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
	private ObservableCollection<Transaction> transactions = [
        new Transaction { Id = 1, Type = "Доход", Cost = 6500, Category = "Зарплата", Date = DateTime.Parse("05.06.2024 12:05:42") },
        new Transaction { Id = 2, Type = "Доход", Cost = 5000, Category = "Перевод", Date = DateTime.Parse("04.06.2024 10:05:22") },
        new Transaction { Id = 3, Type = "Доход", Cost = 3000, Category = "Перевод", Date = DateTime.Parse("03.06.2024 10:05:22") },
        new Transaction { Id = 4, Type = "Расход", Cost = 3000, Category = "Покупка", Date = DateTime.Parse("06.06.2024 11:05:42") }
    ];
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

	public Command DetailCommand => new(async f =>
	{
		if (f is not string namePage) return;
		var navigationParameters = new Dictionary<string, object>
		{
			{ nameof(Transactions), Transactions }
		};
		try
		{
            await Shell.Current.GoToAsync($"//Tabs/{namePage}", false, navigationParameters);
        }
        catch (Exception)
		{
		}
	});


	public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
