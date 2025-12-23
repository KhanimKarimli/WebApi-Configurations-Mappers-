namespace WebApiWithMappers.Entities.Common
{
    public class BaseEntity
    {
		public Guid Id { get; set; }
		public DateTime CreatedTime { get; set; }
		public DateTime UpdatedTime { get; set; }
	}
}
