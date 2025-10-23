using Grocery.App.ViewModels;

namespace Grocery.App.Views;

public partial class RegistrationView : ContentPage
{
    public RegistrationView(RegistrationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}