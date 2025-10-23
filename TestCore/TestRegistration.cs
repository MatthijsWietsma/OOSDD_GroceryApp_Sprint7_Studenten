using System.Collections.ObjectModel;
using Grocery.Core.Data.Repositories;
using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Moq;
using Grocery.Core.Models;
using Grocery.Core.Services;

namespace TestCore;
public class TestRegistration
{

    [SetUp]
    public void Setup()
    {
    }

    private static readonly object[] RegisterReturnsClientCases =
    {

        //speciale tekens
        new object[]
        {
            "Matthijs",
            "matthijs@mail.com",
            "!@#$%^&*",
            new Client(4, "Matthijs", "matthijs@mail.com", "placeholder")
        },
        //normaal wachtwoord
        new object[]
        {
            "Wietsma",
            "wietsma@mail.com",
            "Password",
            new Client(4, "Wietsma", "wietsma@mail.com", "placeholder")
        },
        //namen met spatie
        new object[]
        {
            "Matthijs Wietsma",
            "matthijs.wietsma@mail.com",
            "SpatieTest",
            new Client(6, "Matthijs Wietsma", "matthijs.wietsma@mail.com", "placeholder")
        },
        //lange wachtwoord
        new object[]
        {
            "MatthijsWietsma",
            "matthijs.wietsma@mail.com",
            "SuperTofVetLangWachtwoordHolyMolyWatLangKappenNouStraksHashedHijNiet!",
            new Client(6, "MatthijsWietsma", "matthijs.wietsma@mail.com", "placeholder")
        },

    };

    [TestCaseSource(nameof(RegisterReturnsClientCases))]
    public void TestRegister(string name, string email, string password, Client? expectedClient)
    {
        // Arrange
        IClientRepository clientRepository = new ClientRepository();
        IClientService clientService = new ClientService(clientRepository);
        IAuthService authService = new AuthService(clientService);

        // Act
        Client? registeredClient = authService.Registration(name, email, password);

        // Assert
        if (expectedClient == null)
            Assert.Fail();

        if (PasswordHelper.VerifyPassword(password, registeredClient.Password) == false)
            Assert.Fail("Wachtwoord is niet gehashed");

        Assert.That((registeredClient.Name, registeredClient.EmailAddress), Is.EqualTo((expectedClient.Name, expectedClient.EmailAddress)));
    }
}