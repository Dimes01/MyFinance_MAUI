using MyFinance.Models;

namespace MyFinance.Pages;

public partial class ActionPage : ContentPage
{
    private bool isAdd = false;
    public Transaction Transaction;

	public ActionPage(Transaction transaction = null)
	{
		InitializeComponent();
        if (transaction == null)
        {
            isAdd = true;
            Transaction = new Transaction();
        }
        else Transaction = transaction;
		Init();
	}

	private void Init()
	{
		if (isAdd == false)
		{
			IdProperty.ValueProperty = Transaction.Id.ToString();
            TypeProperty.ValueProperty = Transaction.Type.ToString();
            CostProperty.ValueProperty = Transaction.Cost.ToString();
            CategoryProperty.ValueProperty = Transaction.Category.ToString();
        }
    }

    private async void ButtonSafe_Clicked(object sender, EventArgs e)
    {
        Transaction.Id = Convert.ToInt64(IdProperty.ValueProperty);
        Transaction.Type = TypeProperty.ValueProperty;
        Transaction.Cost = Convert.ToDouble(CostProperty.ValueProperty);
        Transaction.Category = CategoryProperty.ValueProperty;
        await Navigation.PopAsync();
    }

    private async void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}