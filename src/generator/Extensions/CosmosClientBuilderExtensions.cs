// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Extensions;

/// <summary>
/// Extensions for creating a <see cref="CosmosClientBuilder"/> using the <see cref="ConnectionOptions"/> type. 
/// </summary>
public static class CosmosClientBuilderExtensions
{
    private static readonly TokenCredential roleBasedAccessControlCredential = new DefaultAzureCredential();

    /// <inheritdoc />
    public static CosmosClientBuilder ToCosmosClientBuilder(this ConnectionOptions options) => options switch
    {
        { Type: ConnectionType.ResourceOwnerPasswordCredential } => new CosmosClientBuilder(options.Credential),
        { Type: ConnectionType.MicrosoftEntra } => new CosmosClientBuilder(options.Credential, roleBasedAccessControlCredential),
        _ => throw new NotImplementedException()
    };
}