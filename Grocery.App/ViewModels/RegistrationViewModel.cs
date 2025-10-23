using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Text.RegularExpressions;

namespace Grocery.App.ViewModels
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string passwordRepetition = "";

        // 👇 renamed from ErrorMessage → RegisterMessage
        [ObservableProperty]
        private string registerMessage = "";

        public RegistrationViewModel(IAuthService authService, GlobalViewModel global)
        {
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        private void Register()
        {
            RegisterMessage = ""; // reset elke keer

            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(PasswordRepetition))
            {
                RegisterMessage = "Vul alle velden in.";
                return;
            }

            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                RegisterMessage = "Voer een geldig e-mailadres in.";
                return;
            }

            if (Password.Length < 6)
            {
                RegisterMessage = "Het wachtwoord moet minimaal 6 tekens bevatten.";
                return;
            }

            if (Password != PasswordRepetition)
            {
                RegisterMessage = "De wachtwoorden komen niet overeen.";
                return;
            }

            Client? authenticatedClient = _authService.Registration(Name, Email, Password);

            if (authenticatedClient == null)
            {
                RegisterMessage = "Registratie mislukt. Probeer een ander e-mailadres.";
                return;
            }

            _global.Client = authenticatedClient;
            Application.Current.MainPage = new AppShell();
        }
    }
}
