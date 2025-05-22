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
        string target = Path.Combine("raw", fileName);

        if (File.Exists(target))
        {
            string yaml = File.ReadAllText(target);
            IReadOnlyList<T> data = deserializer.Deserialize<T[]>(yaml);
            items.Clear();
            items.AddRange(data);
        }
    }

    public T this[int index] => items[index];

    public int Count => items.Count;

    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
}