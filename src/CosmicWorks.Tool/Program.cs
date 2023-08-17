using CosmicWorks.Tool.Commands;

var registrations = new ServiceCollection();

registrations.AddSingleton<ILoggerFactory, LoggerFactory>();
registrations.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
registrations.AddSingleton<ICosmosContext, CosmosContext>();
registrations.AddSingleton<IDataSource<Product>, ProductDataSource>();
registrations.AddSingleton<IDataSource<Employee>, EmployeeDataSource>();
registrations.AddSingleton<ICosmosDataGenerator<Product>, ProductsCosmosDataGenerator>();
registrations.AddSingleton<ICosmosDataGenerator<Employee>, EmployeesCosmosDataGenerator>();

var registrar = new ServiceCollectionRegistrar(registrations);

CommandApp app = new(registrar);

app.SetDefaultCommand<GenerateDataCommand>()
    .WithDescription("Generate fictitious data for Azure Cosmos DB for NoSQL.");

app.Configure(config =>
{
    config.SetApplicationName("cosmicworks");
    config.AddExample(new[] { "--emulator" });
    config.AddExample(new[] { "--emulator", "--number-of-items", "100" });
    config.AddExample(new[] { "--connection-string", "<API-NOSQL-CONNECTION-STRING>" });
    config.AddExample(new[] { "--connection-string", "<API-NOSQL-CONNECTION-STRING>", "--number-of-items", "500" });
});

return await app.RunAsync(args);