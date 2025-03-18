using AuthService.Domain.Interfaces;

namespace AuthService.Domain.Common
{
    public abstract class BaseSoftDeletableEntity : BaseEntity, ISoftDeletable
    {
        public bool IsDeleted { get; private set; } = false;

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
