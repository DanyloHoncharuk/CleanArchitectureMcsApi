using UserService.Domain.Interfaces;

namespace UserService.Domain.Common
{
    public class BaseSoftDeletableEntity : BaseEntity, ISoftDeletable
    {
        public bool IsDeleted { get; private set; } = false;

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
