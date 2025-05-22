// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Models;

internal sealed record Person(
    string Id,
    string First,
    string Last,
    string Region,
    string Department,
    string PhoneNumberSuffix
)
{
    public Person() : this(default!, default!, default!, default!, default!, default!)
    { }
}