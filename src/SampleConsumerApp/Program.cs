using Azure.Identity;
using Microsoft.Extensions.Configuration;

var keyVaultUri = new Uri(Environment.GetEnvironmentVariable("AZURE_KEY_VAULT_URI"));

var configuration = new ConfigurationBuilder()
    .AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential())
    .Build();

Console.WriteLine(configuration.GetDebugView());