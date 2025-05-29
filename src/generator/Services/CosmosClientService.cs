// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Services;

/// <inheritdoc />
public sealed class CosmosClientService : ICosmosClientService
{
    private CosmosClient? _client;

    /// <inheritdoc />
    public CosmosClient GetCosmosClient(ConnectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        return _client ??= options.Emulator ?
            options.ToEmulatorCompatibleCosmosClient()
            : options
                .ToCosmosClientBuilder()
                .WithSerializerOptions(new CosmosSerializationOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                })
                .WithBulkExecution(true)
                .WithThrottlingRetryOptions(TimeSpan.FromSeconds(30), 30)
                .Build();
    }
}