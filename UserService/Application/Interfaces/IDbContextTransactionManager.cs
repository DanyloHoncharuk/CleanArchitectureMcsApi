namespace UserService.Application.Interfaces
{
    public interface IDbContextTransactionManager
    {
        Task SaveChangesAsync();
    }
}
