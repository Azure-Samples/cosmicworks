// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Models;

internal class Entity<T> : IReadOnlyList<T>
{
    private readonly List<T> items = [];

    private readonly IDeserializer deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    public Entity(string fileName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        bool filter(string name) => name.Contains(fileName, StringComparison.OrdinalIgnoreCase);

        string? resource = assembly.GetManifestResourceNames().FirstOrDefault(filter)
            ?? throw new FileNotFoundException($"The resource {fileName} was not found in the assembly {assembly.FullName}.", nameof(fileName));

        using Stream? stream = assembly.GetManifestResourceStream(resource)
            ?? throw new FileLoadException($"The resource {resource} could not be loaded.", nameof(fileName));
        using StreamReader reader = new(stream);

        string yaml = reader.ReadToEnd();
        IReadOnlyList<T> data = deserializer.Deserialize<T[]>(yaml);
        items.Clear();
        items.AddRange(data);
    }

    public T this[int index] => items[index];

    public int Count => items.Count;

    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
}