namespace DigitalMenu.Application.Common.Models
{
    public interface IEntityDto<TId>
        where TId : struct
    {
        public TId Id { get; set; }
    }
}
