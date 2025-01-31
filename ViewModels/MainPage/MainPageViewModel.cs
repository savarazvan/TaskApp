using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.Services;
using TaskApp.Views.Categories;
using TaskApp.Views.Tasks;

namespace TaskApp.ViewModels.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private List<Category> _categories;

        [ObservableProperty]
        private Category _selectedCategory;

        public MainPageViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
            Shell.Current.Navigated += LoadCategories;
        }

        private async void LoadCategories(object? sender, ShellNavigatedEventArgs e)
        {

            if (e.Current.Location.OriginalString != "//MainPage")
                return;

            var currentUser = await _dbService.GetUser(App.LoggedInUserame);
            if (currentUser == null)
                return;

            var userId = currentUser.Id;
            Categories = await _dbService.GetCategories(userId);
            foreach (var category in Categories)
            {
                var tasks = category.Tasks;
                tasks = tasks.OrderBy(t => t.DueDate).ToList();
                tasks = tasks.OrderBy(t => t.PriorityId).ToList();
                category.Tasks = tasks;
            }
        }

        [RelayCommand]
        private async Task EditCategory(Category category)
        {
            App.SelectedCategoryId = category.Id;
            await Shell.Current.GoToAsync(nameof(EditCategoryPage));
        }

        [RelayCommand]
        private async Task EditTask(TaskItem task)
        {
            App.SelectedTaskId = task.Id;
            await Shell.Current.GoToAsync(nameof(EditTaskPage));
        }

    }
}
