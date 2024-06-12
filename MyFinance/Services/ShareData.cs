using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Services;

public class ShareData
{
    // TODO: Исключить использование static на внедрение зависимостей через AddSingleton в MauiProgram
    public Transaction Transaction { get; set; }
}
