using TaskApp.ViewModels.Authentication;

namespace TaskApp.Views.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        BindingContext = loginViewModel;
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}