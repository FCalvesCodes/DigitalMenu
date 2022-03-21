namespace DigitalMenu.Domain.Core.Entities
{
    public interface IEntity<TId>
         where TId : struct
    {
        TId Id { get; set; }
    }
}
