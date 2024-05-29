using MyFinance.Models;
using MyFinance.Services;

namespace MyFinance.Pages;

public partial class ActionPage : ContentPage
{
    private bool _isEdit = true;

	public ActionPage(bool isEdit)
	{
		InitializeComponent();
        _isEdit = isEdit;
        if (isEdit == false)
        {
            ShareData.Transaction = new Transaction();
        }
		Init();
	}

	private void Init()
	{
		if (_isEdit)
		{
            TypeProperty.ValueProperty = ShareData.Transaction.Type.ToString();
            CostProperty.ValueProperty = ShareData.Transaction.Cost.ToString();
            CategoryProperty.ValueProperty = ShareData.Transaction.Category.ToString();
            DateProperty.ValueProperty = ShareData.Transaction.Date.ToString();
        }
    }

    private async void ButtonSafe_Clicked(object sender, EventArgs e)
    {
        ShareData.Transaction.Type = TypeProperty.ValueProperty;
        ShareData.Transaction.Cost = Convert.ToDouble(CostProperty.ValueProperty);
        ShareData.Transaction.Category = CategoryProperty.ValueProperty;
        ShareData.Transaction.Date = DateTime.Parse(DateProperty.ValueProperty);
        await Navigation.PopAsync();
    }

    private async void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        ShareData.Transaction = null;
        await Navigation.PopAsync();
    }
}