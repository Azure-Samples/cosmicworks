// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Tests.Unit;

public class EntityTests
{
    [Fact]
    public void MissingEmbeddedFileTest()
    {
        // Arrange
        string filename = "<invalid-file-name>";

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() =>
        {
            new Entity<dynamic>(filename);
        });
    }

    [Fact]
    public void ValidEmbeddedFilesTest()
    {
        // Arrange
        string filename = "people.yaml";

        // Act
        Entity<dynamic> entity = new(filename);

        // Assert
        Assert.NotNull(entity);
        Assert.NotEmpty(entity);
    }
}