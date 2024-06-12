using MyFinance.Models;
using MyFinance.Services;

namespace MyFinance.Pages;

public partial class ActionPage : ContentPage, IQueryAttributable
{
    private bool _isEdit = true;
    private readonly ShareData _shareData;

	public ActionPage(ShareData shareData)
	{
		InitializeComponent();
        _shareData = shareData;
		Init();
	}

	private void Init()
	{
        if (_isEdit)
		{
            TypeProperty.ValueProperty = _shareData.Transaction.Type.ToString();
            CostProperty.ValueProperty = _shareData.Transaction.Cost.ToString();
            CategoryProperty.ValueProperty = _shareData.Transaction.Category.ToString();
            DateProperty.ValueProperty = _shareData.Transaction.Date.ToString();
        }
        else
        {
            _shareData.Transaction = new Transaction();
        }
    }

    private async void ButtonSafe_Clicked(object sender, EventArgs e)
    {
        _shareData.Transaction.Type = TypeProperty.ValueProperty;
        _shareData.Transaction.Cost = Convert.ToDouble(CostProperty.ValueProperty);
        _shareData.Transaction.Category = CategoryProperty.ValueProperty;
        _shareData.Transaction.Date = DateTime.Parse(DateProperty.ValueProperty);
        await Navigation.PopAsync();
    }

    private async void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        _shareData.Transaction = null;
        await Navigation.PopAsync();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _isEdit = (bool)query["IsEdit"];
    }
}