using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using KMS; // Replace with the actual namespace of your Startup class

public class CryptographyControllerTests
{
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public CryptographyControllerTests()
    {
        // Set up the test server
        _server = new TestServer(new WebHostBuilder().UseStartup<Startup>()); // Use your Startup class
        _client = _server.CreateClient();
    }

    private StringContent GetStringContent(object obj)
        => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

    [Theory]
    [InlineData("AES", 256)]
    [InlineData("RSA", 2048)]
    public async Task CreateKey_ReturnsKeyId(string algorithm, int keySize)
    {
        var response = await _client.PostAsync("/api/cryptography/create",
            GetStringContent(new { Algorithm = algorithm, KeySize = keySize }));

        response.EnsureSuccessStatusCode();
        var keyId = await response.Content.ReadAsStringAsync();
        Assert.NotNull(keyId);
    }

    // Dispose the TestServer and HttpClient
    public void Dispose()
    {
        _client.Dispose();
        _server.Dispose();
    }
}
