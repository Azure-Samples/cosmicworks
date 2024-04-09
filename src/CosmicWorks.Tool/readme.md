# `CosmicWorks` command-line tool for .NET

CosmicWorks is an open source set of tools and libraries to generate data quickly for your proof of concept or sample applications.

## NuGet link

<https://www.nuget.org/packages/cosmicworks>

## Install

```bash
dotnet tool install --global cosmicworks
```

## Examples

- Generate all data in the [Azure Cosmos DB emulator](https://learn.microsoft.com/azure/cosmos-db/emulator).

    ```bash
    cosmicworks --emulator
    ```

- Generate all data in a live [Azure Cosmos DB for NoSQL](https://learn.microsoft.com/azure/cosmos-db/nosql/) account.

    ```bash
    cosmicworks --connection-string "<API_FOR_NOSQL_CONNECTION_STRING>"
    ```

- Generate a subset of data.

    ```bash
    cosmicworks --emulator --number-of-products 0 --number-of-employees 50
    ```

## Arguments

| | Description | Remarks |
| --- | --- | --- |
| **`--connection-string` (`-c`)** | Connection string to an Azure Cosmos DB for NoSQL account | *You may need to escape connection string characters or enclose the value in quotes within specific operation system shells. If not specified, the CLI will prompt you for a connection string value.* |
| **`--emulator` (`-e`)** | Use emulators connection string | *This setting has a higher precedent than `--connection-string`.* |
| **`--number-of-products`** | Number of product items to generate | *This setting defaults to `200`. If set to `0`, the corresponding container will be skipped. You must set at least this setting or `--number-of-employees` to a positive integer value.* |
| **`--number-of-employees`** | Number of product items to generate | *This setting defaults to `1000`. If set to `0`, the corresponding container will be skipped. You must set at least this setting or `--number-of-products` to a positive integer value.* |
| **`--disable-hierarchical-partition-keys`** | Disables the creation of hierarchical partition keys | *This setting is useful for working in environments, like the emulator, where hierarchical partition keys are not supported.* |
| **`--help` (`-h`)** | Renders help information and examples. | |
| **`--version` (`-v`)** | Renders version information. | |

## Related

- [`CosmicWorks.Data` fictituous data library](https://www.nuget.org/packages/cosmicworks.data)
- [`CosmicWorks.Generator` data seeding library](https://www.nuget.org/packages/cosmicworks.generator)
- [`CosmicWorks.Models` model library](https://www.nuget.org/packages/cosmicworks.models)
