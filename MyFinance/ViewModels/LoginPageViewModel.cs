using MyFinance.Pages;
using MyFinance.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ViewModels
{
    internal class LoginPageViewModel : INotifyPropertyChanged
    {
        private AuthService _authService;

        public LoginPageViewModel(AuthService authService)
        {
            _authService = authService;
        }


        private string email;
        public string Email
        {
            get => email;
            set
            {
                if (email == value) return;
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }


        private string passwordHash;
        public string PasswordHash
        {
            get => passwordHash;
            set
            {
                if (passwordHash == value) return;
                passwordHash = value;
                OnPropertyChanged(nameof(PasswordHash));
            }
        }



        public Command LoginCommand => new(async f =>
        {
            if (_authService.Login(Email, PasswordHash).Result)
            {
                await Shell.Current.GoToAsync($"//Tabs/{nameof(MainPage)}");
            }
        });


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
