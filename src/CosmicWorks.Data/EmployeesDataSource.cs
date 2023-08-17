using System.Text.Json;
using CosmicWorks.Data.Extensions;
using CosmicWorks.Data.Models;
using Raw = CosmicWorks.Data.Models.Raw;

namespace CosmicWorks.Data;

public sealed class EmployeesDataSource : IDataSource<Employee>
{
    public async Task<IReadOnlyList<Employee>> GenerateAsync(int count = 200)
    {
        int generatedEmployeesCount = count switch
        {
            > 200 => throw new ArgumentOutOfRangeException(nameof(count), "You cannot generate more than 200 employees."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one employee"),
            _ => count
        };

        using FileStream reader = File.OpenRead(Path.Combine("Source", @"people.json"));
        var results = await JsonSerializer.DeserializeAsync<IReadOnlyList<Raw.Person>>(reader, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        IEnumerable<Employee> employees = (results ?? Enumerable.Empty<Raw.Person>()).OrderBy(i => Guid.NewGuid()).ToEmployees();

        return employees.Take(count).ToList();
    }
}
