using MyFinance.Models;
using MyFinance.Services;
using MyFinance.ViewModels;

namespace MyFinance.Pages;

public partial class IncomesPage : ContentPage
{
	private TransactionsViewModel viewModel;

	public IncomesPage()
	{
		InitializeComponent();
		InitBindings();
		
	}

	private void InitBindings()
	{
		viewModel = BindingContext as TransactionsViewModel;
		SetBinding(IsMakingTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.IsMakingTransaction), Mode = BindingMode.TwoWay });
		SetBinding(IsEditingTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.IsEditingTransaction), Mode = BindingMode.TwoWay });
		SetBinding(IsRemovingTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.IsRemovingTransaction), Mode = BindingMode.TwoWay });
		SetBinding(ActionTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.ActionTransaction), Mode = BindingMode.TwoWay });
		SetBinding(SelectedTransactionProperty, new Binding { Source = viewModel, Path = nameof(viewModel.SelectedTransaction), Mode = BindingMode.TwoWay });
	}


    private static readonly BindableProperty SelectedTransactionProperty = BindableProperty.Create(nameof(SelectedTransaction), typeof(Transaction), typeof(IncomesPage));
    private Transaction SelectedTransaction
	{
		get => (Transaction)GetValue(SelectedTransactionProperty);
		set => SetValue(SelectedTransactionProperty, value);
	}


    private static readonly BindableProperty ActionTransactionProperty = BindableProperty.Create(nameof(ActionTransaction), typeof(Transaction), typeof(IncomesPage));
    private Transaction ActionTransaction
	{
		get => (Transaction)GetValue(ActionTransactionProperty);
		set => SetValue(ActionTransactionProperty, value);
	}


	private static readonly BindableProperty IsMakingTransactionProperty = BindableProperty.Create(nameof(IsMakingTransaction), typeof(bool), typeof(IncomesPage),
		propertyChanged: OnIsMakingPropertyChanged);
	private bool IsMakingTransaction
	{
		get => (bool)GetValue(IsMakingTransactionProperty);
		set => SetValue(IsMakingTransactionProperty, value);
	}
#pragma warning disable CRR0033 // The void async method should be in a try/catch block
    private async static void OnIsMakingPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
	{
        if ((bool)newValue == false) return;
        if (bindableObject is not IncomesPage page) return;
		var actionPage = new ActionPage(false);
        await page.Navigation.PushAsync(actionPage);
	}
#pragma warning restore CRR0033 // The void async method should be in a try/catch block


    private static readonly BindableProperty IsEditingTransactionProperty = BindableProperty.Create(nameof(IsEditingTransaction), typeof(bool), typeof(IncomesPage),
		propertyChanged: OnIsEditingPropertyChanged);
    private bool IsEditingTransaction
	{
		get => (bool)GetValue(IsEditingTransactionProperty);
		set => SetValue(IsEditingTransactionProperty, value);
	}
    private static async void OnIsEditingPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
		if ((bool)newValue == false) return; 
        if (bindableObject is not IncomesPage page) return;
		if (page.SelectedTransaction is null) return;
		ShareData.Transaction = page.SelectedTransaction;
        var actionPage = new ActionPage(true);
        await page.Navigation.PushAsync(actionPage);
    }


    private static readonly BindableProperty IsRemovingTransactionProperty = BindableProperty.Create(nameof(IsRemovingTransaction), typeof(bool), typeof(IncomesPage),
		propertyChanged: OnIsRemovingTransactionPropertyChanged);
    private bool IsRemovingTransaction
	{
		get => (bool)GetValue(IsRemovingTransactionProperty);
		set => SetValue(IsRemovingTransactionProperty, value);
	}
    private static async void OnIsRemovingTransactionPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
        if ((bool)newValue == false) return;
        if (bindableObject is not IncomesPage page) return;
		if (page.SelectedTransaction is null) return;
		if (await page.DisplayAlert("Удаление", "Вы действительно хотите удалить транзакцию?", "Да", "Нет"))
			page.ActionTransaction = page.SelectedTransaction;
		else
			page.ActionTransaction = null;
    }

    protected override void OnAppearing()
    {
		ActionTransaction = ShareData.Transaction;
    }
}