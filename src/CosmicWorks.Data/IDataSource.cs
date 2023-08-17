namespace CosmicWorks.Data;

public interface IDataSource<T>
{
    Task<IReadOnlyList<T>> GenerateAsync(int count);
}