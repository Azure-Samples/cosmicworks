// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;

/// <summary>
/// Represents a data source that provides read-only access to a collection of items.
/// </summary>
/// <typeparam name="T">
/// The type of items in the data source.
/// </typeparam>
public interface IDataSource<T>
{
    /// <summary>
    /// Gets a read-only list of items from the data source.
    /// </summary>
    /// <param name="count">
    /// The optional number of items to retrieve.
    /// If not specified, all items will be retrieved by default.
    /// </param>
    /// <returns>
    /// A read-only list of items.
    /// </returns>
    IReadOnlyList<T> GetItems(int count = default);
}