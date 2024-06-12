using MyFinance.Models;
using MyFinance.Services;
using MyFinance.ViewModels;

namespace MyFinance.Pages;

public partial class IncomesPage : ContentPage, IQueryAttributable
{
	private TransactionsViewModel viewModel;
    private readonly ShareData shareData;
	
	public IncomesPage(ShareData shareData)
	{
		InitializeComponent();
		InitBindings();
        this.shareData = shareData;
    }

	private void InitBindings()
	{
		viewModel = BindingContext as TransactionsViewModel;
		viewModel.IsIncomePage = true;
		SetBinding(IsMakingTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.IsMakingTransaction), Mode = BindingMode.TwoWay });
		SetBinding(IsEditingTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.IsEditingTransaction), Mode = BindingMode.TwoWay });
		SetBinding(IsRemovingTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.IsRemovingTransaction), Mode = BindingMode.TwoWay });
		SetBinding(ActionTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.ActionTransaction), Mode = BindingMode.TwoWay });
		SetBinding(SelectedTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.SelectedTransaction), Mode = BindingMode.TwoWay });
	}


    private static readonly BindableProperty SelectedTransactionProperty = BindableProperty.Create(nameof(SelectedTransaction), typeof(Transaction), typeof(IncomesPage));
    private static readonly BindableProperty ActionTransactionProperty = BindableProperty.Create(nameof(ActionTransaction), typeof(Transaction), typeof(IncomesPage));
    private static readonly BindableProperty IsMakingTransactionProperty = BindableProperty.Create(nameof(IsMakingTransaction), typeof(bool), typeof(IncomesPage),
		propertyChanged: OnIsMakingPropertyChanged);
	private static readonly BindableProperty IsEditingTransactionProperty = BindableProperty.Create(nameof(IsEditingTransaction), typeof(bool), typeof(IncomesPage),
		propertyChanged: OnIsEditingPropertyChanged);
    private static readonly BindableProperty IsRemovingTransactionProperty = BindableProperty.Create(nameof(IsRemovingTransaction), typeof(bool), typeof(IncomesPage),
		propertyChanged: OnIsRemovingTransactionPropertyChanged);

    private Transaction SelectedTransaction
	{
		get => (Transaction)GetValue(SelectedTransactionProperty);
		set => SetValue(SelectedTransactionProperty, value);
	}
    private Transaction ActionTransaction
	{
		get => (Transaction)GetValue(ActionTransactionProperty);
		set => SetValue(ActionTransactionProperty, value);
	}
	private bool IsMakingTransaction
	{
		get => (bool)GetValue(IsMakingTransactionProperty);
		set => SetValue(IsMakingTransactionProperty, value);
	}    
	private bool IsEditingTransaction
	{
		get => (bool)GetValue(IsEditingTransactionProperty);
		set => SetValue(IsEditingTransactionProperty, value);
	}
    private bool IsRemovingTransaction
	{
		get => (bool)GetValue(IsRemovingTransactionProperty);
		set => SetValue(IsRemovingTransactionProperty, value);
	}


    private async static void OnIsMakingPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
	{
        if ((bool)newValue == false) return;
        if (bindableObject is not IncomesPage page) return;
		var parameters = new Dictionary<string, object>
		{
			{ "IsEdit", false }
		};
		try
		{
			await Shell.Current.GoToAsync($"../ActionPage", parameters);
        }
        catch (Exception)
		{
			await page.DisplayAlert("Добавление", "Не удалось перейти на страницу создания транзакции", "Закрыть");
		}
	}

    private static async void OnIsEditingPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
		if ((bool)newValue == false) return; 
        if (bindableObject is not IncomesPage page) return;
		if (page.SelectedTransaction is null) return;
		page.shareData.Transaction = page.SelectedTransaction;
        var parameters = new Dictionary<string, object>
        {
            { "IsEdit", true }
        };
        try
        {
            await Shell.Current.GoToAsync($"../ActionPage", parameters);
        }
        catch (Exception)
        {
            await page.DisplayAlert("Редактирование", "Не удалось перейти на страницу создания транзакции", "Закрыть");
        }
    }

    private static async void OnIsRemovingTransactionPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
        if ((bool)newValue == false) return;
        if (bindableObject is not IncomesPage page) return;
		if (page.SelectedTransaction is null) return;
		try
		{
            if (await page.DisplayAlert("Удаление", "Вы действительно хотите удалить транзакцию?", "Да", "Нет"))
                page.ActionTransaction = page.SelectedTransaction;
            else
                page.ActionTransaction = null;
        }
		catch (Exception)
		{
			await page.DisplayAlert("Удаление", "Ошибка при удалении", "Закрыть");
		}

    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
		ActionTransaction = query.ContainsKey("Transactions") 
			? query["Transactions"] as Transaction 
			: new Transaction();
    }
}