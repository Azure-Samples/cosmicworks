// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;

/// <summary>
/// A data source that generates items of type <see cref="Employee"/>.
/// </summary>
public sealed class EmployeesDataSource : IDataSource<Employee>
{
    /// <summary>
    /// The maximum number of employees that can be generated.
    /// </summary>
    public const int MaxEmployeesCount = 234;

    /// <inheritdoc />
    public IReadOnlyList<Employee> GetItems(int? count = MaxEmployeesCount)
    {
        int generatedEmployeesCount = count switch
        {
            null => MaxEmployeesCount,
            > MaxEmployeesCount => throw new ArgumentOutOfRangeException(nameof(count), $"You cannot generate more than {MaxEmployeesCount:N0} employees."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one employee"),
            not null => count.Value
        };

        return [.. new People()
            .OrderBy(i => i.Id)
            .Take(generatedEmployeesCount)
            .ToEmployees()];
    }
}