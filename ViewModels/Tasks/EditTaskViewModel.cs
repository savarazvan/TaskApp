using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.Services;

namespace TaskApp.ViewModels.Tasks
{
    public partial class EditTaskViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private TaskItem _taskToEdit;

        [ObservableProperty]
        private List<Category> _categories;

        [ObservableProperty]
        private List<Priority> _priorities;

        [ObservableProperty]
        private Category _selectedCategory;

        [ObservableProperty]
        private Priority _selectedPriority;

        [ObservableProperty]
        private bool _isFormValid;

        public DateTime TodayDate => DateTime.Today;

        public EditTaskViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
            Initialize();
        }

        private void Initialize()
        {
            LoadPriorities();
            LoadCategories();
            foreach(var category in Categories)
            {
                var task = category.Tasks.FirstOrDefault(t => t.Id == App.SelectedTaskId);
                if(task != null)
                {
                    TaskToEdit = task;
                    SelectedCategory = TaskToEdit.Category;
                    SelectedPriority = TaskToEdit.Priority;
                    break;
                }
            }
        }

        private async void LoadCategories()
        {
            var currentUser = await _dbService.GetUser(App.LoggedInUserame);
            if (currentUser == null)
            {
                await Shell.Current.DisplayAlert("Error", "Logged in user not found.", "OK");
                return;
            }
            var userId = currentUser.Id;
            Categories = await _dbService.GetCategories(userId);
        }

        private async void LoadPriorities()
        {
            Priorities = await _dbService.GetPriorities();
            SelectedPriority = Priorities.FirstOrDefault();
        }
        [RelayCommand]
        private async Task DeleteTask()
        {
            try
            {
                var confirm = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this task?", "Yes", "No");
                if (!confirm) return;

                var currentUser = await _dbService.GetUser(App.LoggedInUserame);
                if (currentUser == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Logged in user not found.", "OK");
                    return;
                }

                await _dbService.DeleteTask(TaskToEdit);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task SaveTask()
        {
            try
            {
                if (!ValidateForm())
                {
                    await Shell.Current.DisplayAlert("Error", "Please fill in all fields correctly.", "OK");
                    return;
                }

                var currentUser = await _dbService.GetUser(App.LoggedInUserame);
                if (currentUser == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Logged in user not found.", "OK");
                    return;
                }

                _taskToEdit.CategoryId = SelectedCategory.Id;
                _taskToEdit.PriorityId = SelectedPriority.Id;
                _taskToEdit.UserId = currentUser.Id;

                await _dbService.UpdateTask(_taskToEdit);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private bool ValidateForm()
        {
            return !string.IsNullOrWhiteSpace(_taskToEdit.Title) &&
                   SelectedCategory != null &&
                   SelectedPriority != null;
        }
    }
}
