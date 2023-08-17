using CosmicWorks.Data.Extensions;
using CosmicWorks.Data.Models;

namespace CosmicWorks.Data;

public sealed class EmployeesDataSource : IDataSource<Employee>
{
    public async Task<IReadOnlyList<Employee>> GetItemsAsync(int count = 200)
    {
        int generatedEmployeesCount = count switch
        {
            > 200 => throw new ArgumentOutOfRangeException(nameof(count), "You cannot generate more than 200 employees."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one employee"),
            _ => count
        };

        await Task.Delay(TimeSpan.FromSeconds(1));

        IEnumerable<Employee> employees = Raw.People.Get()
            .OrderBy(i => Guid.NewGuid())
            .ToEmployees();

        return employees.Take(count).ToList();
    }
}
