namespace Notes.Api.Repository.Entities
{
    public interface IEntity
    {
        public int Id { get; set; }
    }

    public class Note : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
