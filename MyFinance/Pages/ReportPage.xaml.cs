
using MyFinance.Models;

namespace MyFinance.Pages;

public partial class ReportPage : ContentPage
{
	public ReportPage(IList<Transaction> transactions)
	{
		InitializeComponent();
		InitTopCategory(transactions);
		InitTopIncome(transactions);
		InitTopExpenditure(transactions);
	}

	// ��������� � ���������� ���������� ��������
	private void InitTopCategory(IList<Transaction> transactions)
	{
		var elems = transactions
			.GroupBy(t => t.Category)
			.Select(s => new { Category = s.Key, Cost = s.Sum(t => t.Cost) })
			.OrderBy(o => o.Cost).Take(1).FirstOrDefault();
		ValueTopCategory.Text = elems.Cost.ToString();
	}

	// ���������� � ���������� �������
	private void InitTopIncome(IList<Transaction> transactions)
	{
		var elems = transactions
			.Where(p => p.Category == "�����")
			.OrderBy(o => o.Cost)
			.Take(1).FirstOrDefault();
		TopIncome.ViewTransaction = elems;
	}

	// ���������� � ���������� ��������
	private void InitTopExpenditure(IList<Transaction> transactions)
	{
        var elems = transactions
            .Where(p => p.Category == "������")
            .OrderBy(o => o.Cost)
            .Take(1).FirstOrDefault();
		TopExpenditure.ViewTransaction = elems;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }
}