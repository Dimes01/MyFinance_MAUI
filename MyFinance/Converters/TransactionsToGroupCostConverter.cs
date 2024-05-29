using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Converters;

internal class TransactionsToGroupCostConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType is not IList<double>) return null;
        if (value is null) return null;
        var transactions = value as List<Transaction>;
        var volumes = transactions.GroupBy(x => x.Category).Select(g => (double)(g.Sum(s => s.Cost))).ToList();
        return volumes;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
