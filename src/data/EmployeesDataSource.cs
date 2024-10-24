﻿using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Extensions;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;

public sealed class EmployeesDataSource : IDataSource<Employee>
{
    public IReadOnlyList<Employee> GetItems(int count = 234)
    {
        int generatedEmployeesCount = count switch
        {
            > 234 => throw new ArgumentOutOfRangeException(nameof(count), "You cannot generate more than 234 employees."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one employee"),
            _ => count
        };

        return Raw.People.Get()
            .OrderBy(i => i.Id)
            .Take(generatedEmployeesCount)
            .ToEmployees()
            .ToList();
    }
}
