using Bogus;
using DNABank.API.Models;
using DNABank.Application.Dtos;
using DNABank.Application.Persistence;
using DNABank.Domain.Entities;
using DNABank.Domain.Entities.Base;
using DNABank.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DNABank.APITests
{
    public class AccountsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;
        private readonly Mock<IDNABankDbContext> _dnaBankDbContextMock = new();
        private readonly Faker _fakerObject;
        private readonly string _baseApiUrl;

        public AccountsControllerTests(WebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.Replace(ServiceDescriptor.Scoped(typeof(IDNABankDbContext),
                                                _ => _dnaBankDbContextMock.Object));
                });
            });
            _fakerObject = new Faker("en");
            _baseApiUrl = "/api/v1/Accounts";
        }

        [Fact]
        public async Task CreateAccount_ShouldReturn201Created()
        {
            // arrange       
            var accountId = _fakerObject.Random.Guid();
            var requestObject = new AccountRequest() { FullName = _fakerObject.Name.FullName(), AccountType = AccountType.Current };
            var exceptedUrl = $"{_baseApiUrl}/{accountId}";

            var client = _applicationFactory.CreateClient();
            var dataAsString = JsonSerializer.Serialize(requestObject);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            _dnaBankDbContextMock.Setup(m => m.BankAccounts.AddAsync(It.IsAny<BankAccount>(), default)).
                Callback<BankAccount, CancellationToken>((u, ct) =>
                {
                    u.Id = accountId;
                });
            _dnaBankDbContextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            // act
            var response = await client.PostAsync(_baseApiUrl, content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(exceptedUrl, response.Headers.Location?.AbsolutePath);
        }

        [Fact]
        public async Task CreateAccount_FullnameSetToNull_ShouldReturn400BadRequest()
        {
            // arrange       
            var requestObject = new AccountRequest() { FullName = null, AccountType = AccountType.Current };

            var client = _applicationFactory.CreateClient();
            var dataAsString = JsonSerializer.Serialize(requestObject);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            // act
            var response = await client.PostAsync(_baseApiUrl, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ForExistingId_ShouldReturn200Ok()
        {
            // arrange
            var id = _fakerObject.Random.Guid();
            var currentAccount = new CurrentAccount()
            {
                AccountNumber = _fakerObject.Finance.Account(),
                AccountType = AccountType.Current,
                FullName = _fakerObject.Name.FullName(),
                IsFrozen = false,
                HasOverdraft = false,
                OverdraftLimit = 0.0M
            };
            currentAccount.Id = id;

            var client = _applicationFactory.CreateClient();
            _dnaBankDbContextMock.Setup(m => m.BankAccounts.FindAsync(It.IsAny<object[]>())).ReturnsAsync(currentAccount);

            // act
            var response = await client.GetAsync($"{_baseApiUrl}/{id}");
            var currentAccountDto = await response.Content.ReadFromJsonAsync<CurrenAccountDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(currentAccountDto);
        }

        [Fact]
        public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
        {
            // arrange
            var id = _fakerObject.Random.Guid();
            var client = _applicationFactory.CreateClient();
            _dnaBankDbContextMock.Setup(m => m.BankAccounts.FindAsync(It.IsAny<object[]>())).ReturnsAsync((CurrentAccount?)null);

            // act
            var response = await client.GetAsync($"{_baseApiUrl}/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ForEmptyId_ShouldReturn400BadRequest()
        {
            // arrange
            var client = _applicationFactory.CreateClient();
            _dnaBankDbContextMock.Setup(m => m.BankAccounts.FindAsync(It.IsAny<object[]>())).ReturnsAsync((CurrentAccount?)null);

            // act
            var response = await client.GetAsync($"{_baseApiUrl}/{Guid.Empty}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAccountStatus_ForExistingId_ShouldReturn200Ok()
        {
            // arrange
            var id = _fakerObject.Random.Guid();
            var currentAccount = new CurrentAccount()
            {
                AccountNumber = _fakerObject.Finance.Account(),
                AccountType = AccountType.Current,
                FullName = _fakerObject.Name.FullName(),
                IsFrozen = false,
                HasOverdraft = false,
                OverdraftLimit = 0.0M
            };
            currentAccount.Id = id;

            _dnaBankDbContextMock.Setup(m => m.BankAccounts.FindAsync(It.IsAny<object[]>())).ReturnsAsync(currentAccount);
            _dnaBankDbContextMock.Setup(m => m.BankAccounts.Update(It.IsAny<BankAccount>())).Callback<BankAccount>(r => currentAccount = (CurrentAccount)r);
            _dnaBankDbContextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);
            var queryParam = "isFrozen=true";
            var client = _applicationFactory.CreateClient();

            // act
            var response = await client.PutAsync($"{_baseApiUrl}/{id}?{queryParam}", null);
            var result = await response.Content.ReadFromJsonAsync<bool>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAccountStatus_ForNonExistingId_ShouldReturn404NotFound()
        {
            // arrange
            _dnaBankDbContextMock.Setup(m => m.BankAccounts.FindAsync(It.IsAny<object[]>())).ReturnsAsync((CurrentAccount?)null);
            var id = _fakerObject.Random.Guid();
            var queryParam = "isFrozen=true";
            var client = _applicationFactory.CreateClient();

            // act
            var response = await client.PutAsync($"{_baseApiUrl}/{id}?{queryParam}", null);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}