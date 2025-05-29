// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Extensions;

/// <summary>
/// Extensions for creating a <see cref="CosmosClientBuilder"/> using the <see cref="ConnectionOptions"/> type. 
/// </summary>
public static class CosmosClientBuilderExtensions
{
    private static readonly TokenCredential roleBasedAccessControlCredential = new DefaultAzureCredential();

    /// <summary>
    /// Creates a <see cref="CosmosClientBuilder"/> based on the provided <see cref="ConnectionOptions"/>.
    /// </summary>
    public static CosmosClientBuilder ToCosmosClientBuilder(this ConnectionOptions options) => options switch
    {
        { Type: ConnectionType.ResourceOwnerPasswordCredential } => new CosmosClientBuilder(options.Credential),
        { Type: ConnectionType.MicrosoftEntra } => new CosmosClientBuilder(options.Credential, roleBasedAccessControlCredential),
        _ => throw new NotImplementedException()
    };

    /// <summary>
    /// Creates a <see cref="CosmosClient"/> based on the provided <see cref="ConnectionOptions"/>.
    /// </summary>
    /// <remarks>
    /// This method is intended for use with the Azure Cosmos DB emulator, which requires specific settings to function correctly.
    /// </remarks>
    public static CosmosClient ToEmulatorCompatibleCosmosClient(this ConnectionOptions options) => new(
        options?.Credential,
        new CosmosClientOptions
        {
            SerializerOptions = new CosmosSerializationOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
            },
            ConnectionMode = ConnectionMode.Gateway,
            AllowBulkExecution = false,
            MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(30),
            MaxRetryAttemptsOnRateLimitedRequests = 30,
            ServerCertificateCustomValidationCallback = (_, _, _) => true
        }
    );
}