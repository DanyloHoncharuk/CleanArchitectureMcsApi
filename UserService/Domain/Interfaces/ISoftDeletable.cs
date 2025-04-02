namespace UserService.Domain.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
        void MarkAsDeleted();
    }
}
