using TaskApp.ViewModels.Tasks;

namespace TaskApp.Views.Tasks;

public partial class EditTaskPage : ContentPage
{
	public EditTaskPage(EditTaskViewModel editTaskViewModel)
	{
		InitializeComponent();
        BindingContext = editTaskViewModel;
    }
}