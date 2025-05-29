// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models.Extensions;

/// <summary>
/// Extensions for the <see cref="IConnectionConfiguration"/> interface.
/// </summary>
public static class IConnectionConfigurationExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static string? GetCredential(this IConnectionConfiguration settings) => settings?.Emulator ?? false ? "<emulator>" : settings?.Endpoint ?? settings?.ConnectionString ?? "<connection-string>";
}