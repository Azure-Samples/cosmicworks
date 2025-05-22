// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Tests.Unit;

public class EntityTests
{
    [Fact]
    public void MissingFileTest()
    {
        // Arrange
        string filename = "<invalid-file-name>";

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() =>
        {
            new Entity<dynamic>(filename);
        });
    }
}