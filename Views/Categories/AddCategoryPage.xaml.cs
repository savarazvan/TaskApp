using TaskApp.ViewModels.Categories;

namespace TaskApp.Views.Categories;

public partial class AddCategoryPage : ContentPage
{
	public AddCategoryPage(AddCategoryViewModel addCategoryViewModel)
	{
		InitializeComponent();
        BindingContext = addCategoryViewModel;
    }
}