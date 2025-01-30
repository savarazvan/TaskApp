using TaskApp.Views.Login;
using TaskApp.Views.Register;
using TaskApp.Views.Tasks;

namespace TaskApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AddTaskPage), typeof(AddTaskPage));
		Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    }
}