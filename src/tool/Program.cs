// Copyright (c) Microsoft Corporation. All rights reserved.
ServiceCollection registrations = new();

registrations.AddSingleton<ILoggerFactory, LoggerFactory>();
registrations.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
registrations.AddSingleton<ICosmosContext, CosmosContext>();
registrations.AddSingleton<IDataSource<Product>, ProductsDataSource>();
registrations.AddSingleton<IDataSource<Employee>, EmployeesDataSource>();
registrations.AddSingleton<ICosmosDataGenerator<Product>, ProductsCosmosDataGenerator>();
registrations.AddSingleton<ICosmosDataGenerator<Employee>, EmployeesCosmosDataGenerator>();
registrations.AddSingleton<ICosmosClientBuilderFactory, CosmosClientBuilderFactory>();

ServiceCollectionRegistrar registrar = new(registrations);

CommandApp app = new(registrar);

app.SetDefaultCommand<GenerateDataCommand>()
    .WithDescription("Generate and seed fictitious data in an Azure Cosmos DB for NoSQL account.");

app.Configure(config =>
{
    config.SetApplicationName("cosmicworks");
    config.AddExample(["--help"]);
    config.AddExample(["--version"]);
    config.AddExample(["--emulator"]);
    config.AddExample(["--emulator", "--number-of-employees", "0"]);
    config.AddExample(["--emulator", "--number-of-products", "0"]);
    config.AddExample(["--emulator", "--number-of-employees", "1000"]);
    config.AddExample(["--emulator", "--number-of-products", "500"]);
    config.AddExample(["--emulator", "--number-of-employees", "200", "--number-of-products", "1000"]);
    config.AddExample(["--rbac", "--endpoint", "\"<API-NOSQL-ENDPOINT>\""]);
    config.AddExample(["--rbac", "--endpoint", "\"<API-NOSQL-ENDPOINT>\"", "--number-of-employees", "0"]);
    config.AddExample(["--rbac", "--endpoint", "\"<API-NOSQL-ENDPOINT>\"", "--number-of-products", "0"]);
    config.AddExample(["--rbac", "--endpoint", "\"<API-NOSQL-ENDPOINT>\"", "--number-of-employees", "100"]);
    config.AddExample(["--rbac", "--endpoint", "\"<API-NOSQL-ENDPOINT>\"", "--number-of-products", "500"]);
    config.AddExample(["--rbac", "--endpoint", "\"<API-NOSQL-ENDPOINT>\"", "--number-of-employees", "200", "--number-of-products", "1000"]);
    config.AddExample(["--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\""]);
    config.AddExample(["--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"", "--number-of-employees", "0"]);
    config.AddExample(["--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"", "--number-of-products", "0"]);
    config.AddExample(["--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"", "--number-of-employees", "100"]);
    config.AddExample(["--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"", "--number-of-products", "500"]);
    config.AddExample(["--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"", "--number-of-employees", "200", "--number-of-products", "1000"]);
});

return await app.RunAsync(args);