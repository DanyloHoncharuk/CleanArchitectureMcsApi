namespace AuthService.Domain.Common
{
    public abstract class BaseEntity
    {
        public DateTime CreationDate { get; private set; } = DateTime.UtcNow;

        public DateTime? UpdateDate { get; private set; } = null;

        public void Update()
        {
            UpdateDate = DateTime.UtcNow;
        }
    }
}
