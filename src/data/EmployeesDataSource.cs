// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;

/// <summary>
/// A data source that generates items of type <see cref="Employee"/>.
/// </summary>
public sealed class EmployeesDataSource : IDataSource<Employee>
{
    /// <inheritdoc/>
    public IReadOnlyList<Employee> GetItems(int count = 234)
    {
        int generatedEmployeesCount = count switch
        {
            > 234 => throw new ArgumentOutOfRangeException(nameof(count), "You cannot generate more than 234 employees."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one employee"),
            _ => count
        };

        return [.. Raw.People.Get()
            .OrderBy(i => i.Id)
            .Take(generatedEmployeesCount)
            .ToEmployees()];
    }
}