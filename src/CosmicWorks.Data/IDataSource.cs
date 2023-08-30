namespace CosmicWorks.Data;

public interface IDataSource<T>
{
    IReadOnlyList<T> GetItems(int count);
}