namespace CosmicWorks.Tool.Interfaces;

public interface IGeneratorService<T> where T : class
{
    Task<IReadOnlyCollection<T>> GenerateDataAsync(int count);
}
