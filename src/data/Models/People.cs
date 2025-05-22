// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Models;

internal sealed class People : Entity<Person>, IReadOnlyList<Person>
{
    public People()
        : base($"{nameof(People).ToLowerInvariant()}.yaml")
    { }
}