namespace AuthService.Domain.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
        void MarkAsDeleted();
    }
}
