namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class NoteModel
    {
        public NoteModel()
        {

        }
        public NoteModel(string note) => Note = note;
        public string Note { get; set; }
    }
}
