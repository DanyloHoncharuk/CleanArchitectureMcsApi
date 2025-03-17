using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Common
{
    public abstract class BaseEntity
    {
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateDate { get; set; } = null;
    }
}
