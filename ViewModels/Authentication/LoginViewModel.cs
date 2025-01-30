using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using TaskApp.Models;
using TaskApp.Services;
using TaskApp.Views.Register;

namespace TaskApp.ViewModels.Authentication
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private bool _isFormValid;

        public LoginViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [RelayCommand]
        private async Task Login()
        {
            try
            {
                ValidateForm();

                bool isAuthenticated = await AuthenticateUser(Username, Password);

                if (isAuthenticated)
                {
                    App.LoggedInUserame = Username;
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid username or password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        [RelayCommand]
        private async Task Register()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(RegisterPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private async Task<bool> AuthenticateUser(string username, string password)
        {
            User foundUser = await _dbService.GetUser(username);
            if (foundUser == null)
            {
                return false;
            }
            return foundUser.Password == password;
        }
        private void ValidateForm()
        {
            IsFormValid = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
