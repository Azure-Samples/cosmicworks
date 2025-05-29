# `CosmicWorks` command-line tool for .NET

CosmicWorks is an open source set of tools and libraries to generate data quickly for your proof of concept or sample applications.

## NuGet link

<https://www.nuget.org/packages/cosmicworks>

## Install

```bash
dotnet tool install --global cosmicworks --version 3.x --prerelease
```

## usage

```bash
cosmicworks generate employees

cosmicworks generate products
```

## Arguments

| | Description | Remarks |
| --- | --- | --- |
| **`--connection-string` (`-c`)** | Connection string for an Azure Cosmos DB for NoSQL account. | *You may need to escape connection string characters or enclose the value in quotes within specific operation system shells. If not specified, the CLI will prompt you for a connection string value.* |
| **`--emulator` (`-e`)** | Use emulators connection string | *This argument has a higher precedent than `--connection-string`.* |
| **`--quantity`** | Number of items to generate | |
| **`--disable-hierarchical-partition-keys`** | Disables the creation of hierarchical partition keys | *This argument is useful for working in environments, like the emulator, where hierarchical partition keys are not supported.* |
| **`--endpoint`** | Endpoint for an Azure Cosmos DB for NoSQL account. | |
| **`--help` (`-h`)** | Renders help information and examples. | |
| **`--version` (`-v`)** | Renders version information. | |

## Examples

- Generate all data for the `products` dataset in the [Azure Cosmos DB emulator](https://learn.microsoft.com/azure/cosmos-db/emulator-linux).

    ```bash
    cosmicworks generate products --emulator
    ```

- Generate a subset of data in the `employees` dataset in an account using Microsoft Entra ID authentication.

    ```bash
    cosmicworks generate employees --endpoint "<ACCOUNT_ENDPOINT>" --quantity 50
    ```

- Generate data in a live [Azure Cosmos DB for NoSQL](https://learn.microsoft.com/azure/cosmos-db/nosql) account using a connection string and a custom database and container name.

    ```bash
    cosmicworks generate products --connection-string "<ACCOUNT_CONNECTION_STRING>" --database-name "<DATABASE_NAME>" --container-name "<CONTAINER_NAME>"
    ```

> [!IMPORTANT]
> If you enable Microsoft Entra ID authentication, the database and container resources are **NOT** created on your behalf.

## Related

- [`CosmicWorks.Data` fictituous data library](https://www.nuget.org/packages/cosmicworks.data)
- [`CosmicWorks.Generator` data seeding library](https://www.nuget.org/packages/cosmicworks.generator)
- [`CosmicWorks.Models` model library](https://www.nuget.org/packages/cosmicworks.models)
