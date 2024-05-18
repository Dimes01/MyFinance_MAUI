using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Services;

public class AuthService
{
    private HttpClient _httpClient = new();
    private readonly string _baseAddress = "https://localhost:7008/";

    public async Task<bool> TestAuth()
    {
        await Task.Delay(2000);
        return true;
    }

    public async Task Login(string email, string password)
    {
        var uri = new Uri($"{_baseAddress}login");
        try
        {
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(await response.Content.ReadAsStringAsync());
            }
        } 
        catch (Exception ex)
        {
            Debug.WriteLine($"ОШИБКА!!! --- {ex.Message}");
        }
    }
}
