using TaskApp.ViewModels.Register;

namespace TaskApp.Views.Register;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
        BindingContext = registerViewModel;
    }
}