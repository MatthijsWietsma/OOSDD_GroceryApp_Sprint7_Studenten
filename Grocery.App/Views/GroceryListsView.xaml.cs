using Grocery.App.ViewModels;

namespace Grocery.App.Views;

public partial class GroceryListsView : ContentPage
{
    public GroceryListsView(GroceryListViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        // ?? IMPORTANT: ToolbarItem is not in the visual tree ? set its BindingContext manually
        foreach (var item in ToolbarItems)
            item.BindingContext = BindingContext;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GroceryListViewModel bindingContext)
        {
            bindingContext.OnAppearing();

        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is GroceryListViewModel bindingContext)
        {
            bindingContext.OnDisappearing();
        }
    }
}