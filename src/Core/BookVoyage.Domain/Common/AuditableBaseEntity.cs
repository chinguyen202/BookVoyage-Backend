namespace BookVoyage.Domain.Common;

public class AuditableBaseEntity: BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}