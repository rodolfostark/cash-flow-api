using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace WebApi.Test.Users.Register;
public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    public RegisterUserTest(WebApplicationFactory<Program> webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        _httpClient.PostAsJsonAsync(, request);
    }
}
