using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Esta configuración permitirá en Producción usar el Azure Key Vault (AKV) como un proveedor de configuración
if (builder.Environment.IsProduction())
{
    var keyVaultName = builder.Configuration.GetValue<string>("keyVaultName")!; //obtenemos el nombre de nuestro key vault, este es el que creamos en Azure
    var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/"); //url a través de la cual podré consultar mi servicio de Key Vault

    //para agregar el AKV como un proveedor de configuracion //keyVaultUri url para leer valores desde el AKV
    builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential(), //para poder conectarnos al AKV usando nuestras credenciales de Azure
       new AzureKeyVaultConfigurationOptions
       {
           ReloadInterval = TimeSpan.FromMinutes(1)
       }); 
}



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
