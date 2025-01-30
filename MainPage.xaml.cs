using TaskApp.Views.Login;
using TaskApp.Views.Tasks;

namespace TaskApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            if (App.LoggedInUserame == null)
            {
                Shell.Current.GoToAsync(nameof(LoginPage), true);
            }
            InitializeComponent();
        }
        public void OnAddButtonClicked(object sender, EventArgs e)
        {
            this.ActionButtons.IsVisible = !this.ActionButtons.IsVisible;
            plusButton.Text = this.ActionButtons.IsVisible ? "V" : "+";
        }
        public async void OnAddTaskClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AddTaskPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
