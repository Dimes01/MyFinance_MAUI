using MyFinance.Models;

namespace MyFinance.Pages;

public partial class ActionPage : ContentPage
{
	public ActionPage(Transaction transaction = null)
	{
		InitializeComponent();
		Init(transaction);
	}

	private void Init(Transaction transaction)
	{
		if (transaction is not null)
		{
			IdProperty.ValueProperty = transaction.Id.ToString();
            TypeProperty.ValueProperty = transaction.Type.ToString();
            CostProperty.ValueProperty = transaction.Cost.ToString();
            CategoryProperty.ValueProperty = transaction.Category.ToString();
        }
    }

	
}