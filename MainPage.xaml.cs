using TaskApp.ViewModels.MainPage;
using TaskApp.Views.Categories;
using TaskApp.Views.Login;
using TaskApp.Views.Tasks;

namespace TaskApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            if (App.LoggedInUserame == null)
            {
                Shell.Current.GoToAsync(nameof(LoginPage), true);
            }
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

        public void OnAddButtonClicked(object sender, EventArgs e)
        {
            this.ActionButtons.IsVisible = !this.ActionButtons.IsVisible;
            plusButton.Text = this.ActionButtons.IsVisible ? "-" : "+";
        }
        public async void OnAddTaskClicked(object sender, EventArgs e)
        {
            try
            {
                this.ActionButtons.IsVisible = false;
                this.plusButton.Text = "+";
                await Shell.Current.GoToAsync(nameof(AddTaskPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            try
            {
                this.ActionButtons.IsVisible = false;
                this.plusButton.Text = "+";
                await Shell.Current.GoToAsync(nameof(AddCategoryPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
