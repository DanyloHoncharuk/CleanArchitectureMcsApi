namespace AuthService.Domain.Common
{
    public abstract class BaseSoftDeletableEntity : BaseEntity, ISoftDeletable
    {
        public bool IsDeleted { get; set; } = false;
    }
}
