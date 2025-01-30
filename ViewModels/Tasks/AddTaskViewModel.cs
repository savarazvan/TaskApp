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
        }

        [RelayCommand]
        private async Task LoadCategories()
        {
            // Replace with actual user ID management
            var userId = 1;
            Categories = await _dbService.GetCategories(userId);
            SelectedCategory = Categories.FirstOrDefault();
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
                if (!IsFormValid) return;

                NewTask.CategoryId = SelectedCategory.Id;
                NewTask.PriorityId = SelectedPriority.Id;
                NewTask.UserId = 1; // Replace with actual user ID

                await _dbService.AddTask(NewTask);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void ValidateForm()
        {
            IsFormValid = !string.IsNullOrWhiteSpace(NewTask.Title) &&
                         SelectedCategory != null &&
                         SelectedPriority != null;
        }
    }
}
