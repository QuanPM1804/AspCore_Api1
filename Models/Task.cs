namespace AspCore_Api1.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Tilte {  get; set; }
        public bool IsCompleted { get; set; }
    }
}
