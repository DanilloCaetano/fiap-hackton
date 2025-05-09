namespace Integration
{
    public interface IIntegrationService
    {
        Task<T?> SendRequestWithPolicy<T>(Func<Task<T>> call);
    }
}
