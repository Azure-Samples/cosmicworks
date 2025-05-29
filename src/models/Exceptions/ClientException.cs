// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models.Exceptions;

/// <summary>
/// Represents an exception that occurs when there is a client-side error.
/// </summary>
public sealed class ClientException : Exception
{
    /// <inheritdoc />
    public ClientException() { }

    /// <inheritdoc />
    public ClientException(string message)
        : base(message) { }

    /// <inheritdoc />
    public ClientException(string message, Exception inner)
        : base(message, inner) { }
}