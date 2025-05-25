// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Services;

internal sealed class ServiceCollectionRegistrar(
    IServiceCollection serviceCollection
) : ITypeRegistrar
{
    public ITypeResolver Build() => new TypeResolver(serviceCollection.BuildServiceProvider());

    public void Register(Type service, Type implementation) => serviceCollection.AddTransient(service, implementation);

    public void RegisterInstance(Type service, object implementation) => serviceCollection.AddSingleton(service, implementation);

    public void RegisterLazy(Type service, Func<object> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        serviceCollection.AddSingleton(service, _ => factory());
    }
}