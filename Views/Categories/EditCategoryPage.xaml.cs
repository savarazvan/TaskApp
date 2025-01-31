using TaskApp.ViewModels.Categories;

namespace TaskApp.Views.Categories;

public partial class EditCategoryPage : ContentPage
{
	public EditCategoryPage(EditCategoryViewModel editCategoryViewModel)
	{
		InitializeComponent();
        BindingContext = editCategoryViewModel;
    }
}