using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.Services;

namespace TaskApp.ViewModels.Categories
{
    public partial class AddCategoryViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private Category _newCategory = new Category();

        [RelayCommand]
        private async Task AddCategory()
        {
            try
            {
                var currentUser = _dbService.GetUser(App.LoggedInUserame);
                if (currentUser == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Logged in user not found.", "OK");
                    return;
                }
                NewCategory.UserId = currentUser.Id;
                if (string.IsNullOrWhiteSpace(NewCategory.Name))
                {
                    await Shell.Current.DisplayAlert("Error", "Please fill in all fields.", "OK");
                    return;
                }
                await _dbService.AddCategory(NewCategory);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.InnerException.Message, "OK");
            }
        }

        public AddCategoryViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }
    }
}
