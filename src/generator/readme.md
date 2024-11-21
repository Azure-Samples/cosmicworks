# `CosmicWorks.Generator` data seeding library

CosmicWorks is an open source set of tools and libraries to generate data quickly for your proof of concept or sample applications.

## Overview

Populates an Azure Cosmos DB for NoSQL container with fictiuous data generated using `CosmicWorks.Data`.

> 💡 The **CosmicWorks** sample data is partially derived from [AdventureWorksLT](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/adventure-works).

## Usage

```csharp
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.DataSource;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.BuilderFactory;

CosmosClientBuilderFactoryOptions options = new()
{
  UseEmulator = true,
  UseRoleBasedAccessControl = false,
  Endpoint = null,
  ConnectionString = null
};

// Seed "employees" database
await new EmployeesCosmosDataGenerator(
    cosmosContext: new CosmosContext(),
    dataSource: new EmployeesDataSource()
).GenerateAsync(
    factoryOptions: options, 
    databaseName: "cosmicworks",
    containerName: "employees",
    count: 200,
    disableHierarchicalPartitionKeys: false
    onItemCreate: (result) => Console.WriteLine($"[NEW PRODUCT]\t{result}")
);

// Seed "products" database
await new ProductsCosmosDataGenerator(
    cosmosContext: new CosmosContext(),
    dataSource: new ProductsDataSource()
).GenerateAsync(
    factoryOptions: options,
    databaseName: "cosmicworks",
    containerName: "products",
    count: 1000,
    disableHierarchicalPartitionKeys: false
    onItemCreate: (result) => Console.WriteLine($"[NEW EMPLOYEE]\t{result}")
);
```

## Data generated

| | Description |
| --- | --- |
| **Employees** | Creates a container with fictiuous employee items and hierarchical partitoining based on `/company`, `/department`, and then `/territory`.
| **Products** | Creates a container with fictiuous product items and hierarchical partitoining based on `/category.name` and `/category.subCategory.name`.

## Sample data

Here's samples of the JSON data created using this tool with system-properties omitted:

### Employees

```json
{
  "name": {
    "first": "Karolina",
    "last": "Rocha"
  },
  "addresses": [
    {
      "name": "Headquarters",
      "street": "1234 Oak Street",
      "city": "Redmond",
      "state": "WA",
      "zipCode": "23052",
      "type": "work"
    }
  ],
  "company": "Adventure Works",
  "department": "Marketing",
  "emailAddress": "karolina@adventure-works.com",
  "phoneNumber": "425-555-0164",
  "territory": "Brazil",
  "type": "employee"
}
```

### Products

```json
{
  "name": "Taillights - Battery-Powered",
  "description": "Affordable light for safe night riding - uses 3 AAA batteries",
  "category": {
    "name": "Accessories",
    "subCategory": {
      "name": "Lights"
    }
  },
  "sku": "LT-T990",
  "tags": [
    "Accessories",
    "Lights"
  ],
  "price": 918.85
}
```

## Related

- [`CosmicWorks` data generation CLI tool](https://www.nuget.org/packages/cosmicworks)
- [`CosmicWorks.Data` fictituous data library](https://www.nuget.org/packages/cosmicworks.data)
- [`CosmicWorks.Models` model library](https://www.nuget.org/packages/cosmicworks.models)
