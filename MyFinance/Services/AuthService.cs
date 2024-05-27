using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyFinance.Services;

internal class AuthService
{
    private HttpClient _httpClient = new();
    private readonly string _baseAddress = 
        DeviceInfo.Platform == DevicePlatform.Android 
        ? "https://192.168.1.3:7008" 
        : "https://localhost:7008";
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task<bool> Login(User user)
    {
        //var uri = new Uri($"{_baseAddress}/login");
        //try
        //{
        //    var json = JsonSerializer.Serialize(user, options: jsonSerializerOptions);
        //    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync(uri, stringContent);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return true;
        //        //Debug.WriteLine(await response.Content.ReadAsStringAsync());
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Debug.WriteLine($"ОШИБКА!!! --- {ex.Message}");
        //}

        //return false;

        return true;
    }
}
