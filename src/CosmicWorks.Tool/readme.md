# ``CosmicWorks`` command-line tool for .NET

> *We are rebuilding cosmicworks as an open-source set of libraries and tools! Watch this repo and follow along as we work to v2 of this tool.*

## NuGet link

<https://www.nuget.org/packages/cosmicworks>

## Install

```powershell
dotnet tool install cosmicworks --global --prerelease
```

## Examples

```powershell
cosmicworks --emulator

cosmicworks --connection-string "<CONNECTION_STRING>"
```

## Arguments

| | Description | Remarks |
| --- | --- | --- |
| **``--connection-string`` (``-c``)** | Connection string to an Azure Cosmos DB for NoSQL account | *You may need to escape connection string characters or enclose the value in quotes within specific operation system shells. If not specified, the CLI will prompt you for a connection string value.* |
| **``--emulator`` (``-e``)** | Use emulators connection string | *This setting has a higher precedent than ``--connection-string``. |
| **``--number-of-products``** | Number of product items to generate | *This setting defaults to ``200``. If set to ``0``, the corresponding container will be skipped. You must set at least this setting or ``--number-of-employees`` to a positive integer value. |
| **``--number-of-employees``** | Number of product items to generate | *This setting defaults to ``1000``. If set to ``0``, the corresponding container will be skipped. You must set at least this setting or ``--number-of-products`` to a positive integer value. |

## Related

- [``CosmicWorks.Data`` fictituous data library](https://www.nuget.org/packages/cosmicworks.data)
- [``CosmicWorks.Generator`` data seeding library](https://www.nuget.org/packages/cosmicworks.generator)
