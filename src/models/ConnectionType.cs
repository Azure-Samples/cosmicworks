// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

/// <summary>
/// Represents the type of connection to be used.
/// </summary>
public enum ConnectionType
{
    /// <summary>
    /// The connection is made using a resource owner password credential (ROPC) in the connection string.
    /// </summary>
    ResourceOwnerPasswordCredential,

    /// <summary>
    /// The connection is made using Microsoft Entra authentication and Azure role-based access control (RBAC) and the account endpoint.
    /// </summary>
    MicrosoftEntra
}