// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Extensions;

/// <summary>
/// Extensions for creating connection options using the <see cref="ConnectionOptions"/> type. 
/// </summary>
public static class ConnectionOptionsExtensions
{
    private const string emulatorConnectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

    /// <summary>
    /// Creates a <see cref="ConnectionOptions"/> instance using the specified configuration.
    /// </summary>
    /// <param name="configuration">
    /// The <see cref="IConnectionConfiguration"/> configuration to use.
    /// </param>
    /// <returns>
    /// An instance of <see cref="ConnectionOptions"/> based on the specified configuration.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Throws if the configuration is not recognized.
    /// </exception>
    public static ConnectionOptions ToConnectionOptions(this IConnectionConfiguration configuration) =>
        configuration switch
        {
            { Emulator: true } => FromEmulator(),
            { Endpoint: string endpoint } => FromEndpoint(endpoint),
            { ConnectionString: string connectionString } => FromConnectionString(connectionString),
            _ => throw new NotImplementedException()
        };

    /// <summary>
    /// Creates a <see cref="ConnectionOptions"/> instance using the specified endpoint.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ConnectionOptions"/>.
    /// </returns>
    /// <remarks>
    /// This connection will use Microsoft Entra authentication automatically.
    /// </remarks>
    public static ConnectionOptions FromEndpoint(string endpoint) => new()
    {
        Type = ConnectionType.MicrosoftEntra,
        Credential = endpoint
    };

    /// <summary>
    /// Creates a <see cref="ConnectionOptions"/> instance using the specified connection string.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ConnectionOptions"/>.
    /// </returns>
    /// <remarks>
    /// This is NOT recommended for production use. Use <see cref="FromEndpoint(string)"/> instead.
    /// </remarks>
    public static ConnectionOptions FromConnectionString(string connectionString) => new()
    {
        Type = ConnectionType.ResourceOwnerPasswordCredential,
        Credential = connectionString
    };

    /// <summary>
    /// Creates a <see cref="ConnectionOptions"/> instance for the emulator.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ConnectionOptions"/>.
    /// </returns>
    /// <remarks>
    /// This connection uses the fixed emulator connection string.
    /// </remarks>
    public static ConnectionOptions FromEmulator() => new()
    {
        Type = ConnectionType.ResourceOwnerPasswordCredential,
        Credential = emulatorConnectionString
    };
}