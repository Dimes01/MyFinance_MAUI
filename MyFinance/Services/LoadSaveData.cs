using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyFinance.Services;

internal class LoadSaveData(string pathToFile)
{
    private readonly string _pathToFile = pathToFile;

    public List<Transaction> LoadTransactions()
    {
        var data = File.ReadAllText(_pathToFile, Encoding.UTF8);
        var transactions = JsonSerializer.Deserialize<List<Transaction>>(data);
        return transactions;
    }

    public void SaveTransactions(List<Transaction> transactions)
    {
        var json = JsonSerializer.Serialize(transactions);
        File.WriteAllText(_pathToFile, json);
    }
}
