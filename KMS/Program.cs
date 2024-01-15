using KMS.Controllers;
using KMS.CryptographyProviders;
using KMS.Factory;

var builder = WebApplication.CreateBuilder(args);

// Register CryptographyProviderFactory as a singleton
builder.Services.AddSingleton<CryptographyProviderFactory>();

// Register KeyStoreManager as a singleton
builder.Services.AddSingleton<KeyStoreManager>();

// Register providers as Transient or Scoped based on your needs
builder.Services.AddTransient<AESProvider>();
builder.Services.AddTransient<RSAProvider>();

// Setup CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowMyReactApp", builderCors =>
    {
        builderCors.WithOrigins("http://localhost:3000") // Your React app's URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
    });
});

// Register controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI support
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// Add UseRouting middleware
app.UseRouting();

// Apply CORS policy
app.UseCors("AllowMyReactApp");

// Add UseAuthorization middleware
app.UseAuthorization();

// Map controllers
app.MapControllers();

