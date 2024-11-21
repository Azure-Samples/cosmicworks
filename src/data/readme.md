# `CosmicWorks.Data` fictituous data library

CosmicWorks is an open source set of tools and libraries to generate data quickly for your proof of concept or sample applications.

## Overview

This library generates fictious data used in the **CosmicWorks** sample dataset.

> 💡 The **CosmicWorks** sample data is partially derived from [AdventureWorksLT](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/adventure-works).

## Usage

```csharp
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Models;

// Generate employees
IReadOnlyList<Product> products = await new ProductsDataSource().GetItemsAsync(
    count: 1000
);

// Generate products
IReadOnlyList<Employee> employees = await new EmployeesDataSource().GetItemsAsync(
    count: 200
);
```

## Data available

| | Description |
| --- | --- |
| **Employees** | Fictituous employee records based on a diverse set of names. |
| **Products** | Fictituous product records based on <https://adventure-works.com>. |

## Related

- [`CosmicWorks` data generation CLI tool](https://www.nuget.org/packages/cosmicworks)
- [`CosmicWorks.Generator` data seeding library](https://www.nuget.org/packages/cosmicworks.generator)
- [`CosmicWorks.Models` model library](https://www.nuget.org/packages/cosmicworks.models)
