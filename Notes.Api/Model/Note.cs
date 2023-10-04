namespace Notes.Api.Model
{
    public enum NoteTypes
    {
        None = 0,
        Normal = 1,
        Angry = 2
    }

    public class NoteModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public NoteTypes NoteType { get; set; }

        public string AdditionalNotes { get; set; }
    }
}
