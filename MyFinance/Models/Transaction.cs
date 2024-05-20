using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Models;

public class Transaction
{
    public long Id { get; set; }
    public string Type { get; set; }
    public double Cost { get; set; }
    public string Category { get; set; }
}
