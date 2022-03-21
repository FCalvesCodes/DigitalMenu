namespace DigitalMenu.Domain.Core.Entities
{
    public class Entity<TId> : IEntity<TId>
        where TId : struct
    {
        public virtual TId Id { get; set; }
    }
}
