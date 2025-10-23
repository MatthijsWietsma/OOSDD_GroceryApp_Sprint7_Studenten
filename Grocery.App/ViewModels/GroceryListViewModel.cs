using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.App.Views;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        private readonly IGroceryListService _groceryListService;

        [ObservableProperty]
        private Client client;

        public ObservableCollection<GroceryList> GroceryLists { get; set; }

        public GroceryListViewModel(IGroceryListService groceryListService, GlobalViewModel global)
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            GroceryLists = new(_groceryListService.GetAll());

            Client = global.Client;
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> parameter = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(GroceryListItemsView)}?Titel={groceryList.Name}", true, parameter);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists = new(_groceryListService.GetAll());
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }
    }
}
