using CosmicWorks.Tool.Commands;
using Spectre.Console.Cli;

return await new CommandApp<GenerateDataCommand>()
    .RunAsync(args);