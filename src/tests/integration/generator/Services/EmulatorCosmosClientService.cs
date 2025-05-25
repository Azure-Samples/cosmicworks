// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Tests.Integration.Services;

internal sealed class EmulatorCosmosClientService : ICosmosClientService
{
    private CosmosClient? _client;

    public CosmosClient GetCosmosClient(ConnectionOptions options) => _client ??= new CosmosClient(
        Constants.EmulatorCredential,
        new CosmosClientOptions
        {
            SerializerOptions = new CosmosSerializationOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
            },
            ConnectionMode = ConnectionMode.Gateway,
            AllowBulkExecution = true,
            MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(30),
            MaxRetryAttemptsOnRateLimitedRequests = 30,
            ServerCertificateCustomValidationCallback = (_, _, _) => true
        }
    );
}