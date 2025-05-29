// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Services;

internal sealed class TypeResolver(
    IServiceProvider serviceProvider
) : ITypeResolver
{
    public object? Resolve(Type? type) =>
        (type is null) ? null
        : serviceProvider.GetService(type) ?? throw new InvalidOperationException($"Type '{type.FullName}' could not be resolved.");
}