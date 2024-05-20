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
    private readonly string _baseAddress = 
        DeviceInfo.Platform == DevicePlatform.Android 
        ? "https://192.168.174.121:7008" 
        : "https://localhost:7008";

    public async Task<bool> Login(string email, string password)
    {
        var uri = new Uri($"{_baseAddress}/login");
        try
        {
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
                //Debug.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ОШИБКА!!! --- {ex.Message}");
        }

        // Debug
        if (DeviceInfo.Platform == DevicePlatform.Android) return true; 

        return false;
    }
}
