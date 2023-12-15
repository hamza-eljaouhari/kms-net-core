using KMS.Controllers;
using KMS.CryptographyProviders;
using KMS.Factory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register cryptographic providers as singletons
builder.Services.AddSingleton<AESProvider>();
builder.Services.AddSingleton<RSAProvider>();
// Register other providers as needed...

// Register CryptographyProviderFactory as a singleton
builder.Services.AddSingleton<CryptographyProviderFactory>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


