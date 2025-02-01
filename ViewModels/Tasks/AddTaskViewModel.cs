using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskApp.Models;
using TaskApp.Services;

namespace TaskApp.ViewModels.Tasks
{
    public partial class AddTaskViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private TaskItem _newTask = new TaskItem
        {
            DueDate = DateTime.Today.AddDays(1)
        };

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

        public AddTaskViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
            Initialize();
        }

        private void Initialize()
        {
            LoadPriorities();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            var currentUser = await _dbService.GetUser(App.LoggedInUserame);
            if(currentUser == null)
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

                NewTask.CategoryId = SelectedCategory.Id;
                NewTask.PriorityId = SelectedPriority.Id;
                NewTask.UserId = currentUser.Id;

                await _dbService.AddTask(NewTask);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private bool ValidateForm()
        {
            return !string.IsNullOrWhiteSpace(NewTask.Title) &&
                         SelectedCategory != null &&
                         SelectedPriority != null;
        }
    }
}
