using MyFinance.Models;

namespace MyFinance.Controls;

public partial class TransactionProperty : ContentView
{
	public TransactionProperty()
	{
		InitializeComponent();
	}


	public static readonly BindableProperty ViewTransactionProperty = BindableProperty.Create(nameof(ViewTransaction), typeof(Transaction), typeof(TransactionProperty),
		propertyChanged: OnViewTransactionChanged);
	public Transaction ViewTransaction
	{
		get => (Transaction)GetValue(ViewTransactionProperty);
		set => SetValue(ViewTransactionProperty, value);
	}
	private static void OnViewTransactionChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is not TransactionProperty prop) return;
		if (newValue is not Transaction transaction) return;
		prop.Date.Text = transaction.Date.ToString();
		prop.Cost.Text = transaction.Cost.ToString();
		prop.Type.Text = transaction.Type.ToString();
		prop.Category.Text = transaction.Category.ToString();
	}
}