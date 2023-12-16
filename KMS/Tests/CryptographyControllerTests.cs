using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using KMS.Controllers;
using KMS.Factory;
using Microsoft.Extensions.DependencyInjection;
using KMS.CryptographyProviders;

public class CryptographyControllerTests
{
    private readonly CryptographyController _controller;

    public CryptographyControllerTests()
    {
        // Create a new service collection
        var services = new ServiceCollection();

        // Add the necessary services and dependencies
        services.AddSingleton<CryptographyProviderFactory>(); // Add any other services needed for the controller
        services.AddSingleton<AESProvider>(); // Add any other services needed for the controller
        services.AddSingleton<RSAProvider>(); // Add any other services needed for the controller
        services.AddSingleton<CryptographyProviderFactory>(); // Add any other services needed for the controller
        services.AddTransient<CryptographyController>();
        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Resolve the controller from the service provider
        _controller = serviceProvider.GetRequiredService<CryptographyController>();
    }


    [Fact]
    public void TestKeyLifeCycle_WhenCalled_ReturnsOkResult()
    {
        /* 

        var result = _controller.CreateKey("AES", 256);
        Assert.IsType<OkObjectResult>(result);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var keyId = Assert.IsType<string>(okResult.Value);

        var encrypted = _controller.Encrypt("AES", keyId, new byte[] {1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4});
        Assert.IsType<OkObjectResult>(encrypted);

        var decrypted = _controller.Decrypt("AES", keyId, new byte[] { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4});
        Assert.IsType<OkObjectResult>(decrypted);

        var activated = _controller.ActivateKey("AES", keyId);
        Assert.IsType<OkObjectResult>(activated);

        var infos = _controller.GetKeyInfo("AES", keyId);
        Assert.IsType<OkObjectResult>(infos);

        var deactivated = _controller.DeactivateKey("AES", keyId);
        Assert.IsType<OkObjectResult>(deactivated);
        var revoked = _controller.RevokeKey("AES", keyId);
        Assert.IsType<OkObjectResult>(revoked);

        var archived = _controller.ArchiveKey("AES", keyId);
        Assert.IsType<OkObjectResult>(archived);

        var recovered = _controller.RecoverKey("AES", keyId);
        Assert.IsType<OkObjectResult>(recovered);

        var destroyed = _controller.DestroyKey("AES", keyId);
        Assert.IsType<OkObjectResult>(destroyed);

        */

    }
}
