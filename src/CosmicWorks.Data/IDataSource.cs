namespace CosmicWorks.Data;

public interface IDataSource<T>
{
    Task<IReadOnlyList<T>> GetItemsAsync(int count);
}