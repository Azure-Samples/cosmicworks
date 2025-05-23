// Copyright (c) Microsoft Corporation. All rights reserved.
ServiceCollection registrations = new();

registrations.RegisterGeneratorServices();

ServiceCollectionRegistrar registrar = new(registrations);

CommandApp app = new(registrar);

app.ConfigureCommands();

return await app.RunAsync(args);