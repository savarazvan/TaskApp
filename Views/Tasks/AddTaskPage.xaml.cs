using TaskApp.ViewModels;
using TaskApp.ViewModels.Tasks;

namespace TaskApp.Views.Tasks;

public partial class AddTaskPage : ContentPage
{
    public AddTaskPage(AddTaskViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}