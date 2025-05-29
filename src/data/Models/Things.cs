// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Models;

internal sealed class Things : Entity<Thing>, IReadOnlyList<Thing>
{
    public Things()
        : base($"{nameof(Things).ToLowerInvariant()}.yaml")
    { }
}