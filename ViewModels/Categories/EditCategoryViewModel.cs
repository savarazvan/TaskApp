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
    public partial class EditCategoryViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private Category _categoryToEdit;

        [RelayCommand]
        private async Task SaveCategory()
        {
            try
            {
                var currentUser = _dbService.GetUser(App.LoggedInUserame);
                if (currentUser == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Logged in user not found.", "OK");
                    return;
                }
                if (string.IsNullOrWhiteSpace(CategoryToEdit.Name))
                {
                    await Shell.Current.DisplayAlert("Error", "Please fill in all fields.", "OK");
                    return;
                }

                await _dbService.UpdateCategory(CategoryToEdit);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        [RelayCommand]
        private async Task DeleteCategory()
        {
            try
            {
                var currentUser = _dbService.GetUser(App.LoggedInUserame);
                if (currentUser == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Logged in user not found.", "OK");
                    return;
                }
                await _dbService.DeleteCategory(CategoryToEdit);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public EditCategoryViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
            GetCategoryToEdit();
        }
        public async void GetCategoryToEdit()
        {
            var currentUser = await _dbService.GetUser(App.LoggedInUserame);
            if (currentUser == null) { 
                await Shell.Current.DisplayAlert("Error", "Logged in user not found.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            foreach(var category in currentUser.Categories)
            {
                if(category.Id == App.SelectedCategoryId)
                {
                    CategoryToEdit = category;
                    break;
                }
            }
        }
    }
}
