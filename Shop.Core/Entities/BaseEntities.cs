namespace Shop.Core.Entities;

public abstract class BaseEntities
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    public DateTime Deleted { get; set; } = DateTime.Now;
    public virtual bool IsDeleted { get; set; }
}
