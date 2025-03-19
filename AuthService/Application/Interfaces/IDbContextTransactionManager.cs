namespace AuthService.Application.Interfaces
{
    public interface IDbContextTransactionManager
    {
        Task SaveChangesAsync();
    }
}
