using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Threading;
using TaskApp.Models;
using TaskApp.Services;
using TaskApp.Views.Login;

namespace TaskApp.ViewModels.Register
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _confirmPassword;

        [ObservableProperty]
        private bool _isFormValid;

        public RegisterViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [RelayCommand]
        private async Task CreateUser()
        {
            try
            {
                ValidateForm();

                if (!IsFormValid)
                {
                    await Shell.Current.DisplayAlert("Error", "Please fill in all fields correctly.", "OK");
                    return;
                }

                if (Password != ConfirmPassword)
                {
                    await Shell.Current.DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }

                User newUser = new User { Username = Username, Password = Password };
                await _dbService.AddUser(newUser);
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.InnerException.Message, "OK");
            }
        }

        private void ValidateForm()
        {
            IsFormValid = !string.IsNullOrWhiteSpace(Username) &&
                          !string.IsNullOrWhiteSpace(Password) &&
                          !string.IsNullOrWhiteSpace(ConfirmPassword);
        }

        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
