using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Converters;

internal class TransactionsToGroupCostConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return null;
        var transactions = value as Collection<Transaction>;
        var volumes = transactions
            .GroupBy(x => x.Category)
            .Select(g => new VolumesConverted { Category = g.Key, Volume = g.Sum(s => s.Cost) })
            .ToList();
        return volumes;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
