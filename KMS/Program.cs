using KMS.Controllers;
using KMS.CryptographyProviders;
using KMS.Factory;

var builder = WebApplication.CreateBuilder(args);


// Register CryptographyProviderFactory as a singleton
builder.Services.AddSingleton<CryptographyProviderFactory>();

builder.Services.AddSingleton<KeyStoreManager>();

// Register providers as Transient or Scoped based on your needs
builder.Services.AddTransient<AESProvider>();
builder.Services.AddTransient<RSAProvider>();

builder.Services.AddCors(options => {
    options.AddPolicy(name: "AllowMyReactApp",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000") // Your React app's URL
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
                });

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

app.UseCors("AllowMyReactApp");

app.Run();


