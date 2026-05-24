namespace WebApi.Entities
{
    public class MessageTheme
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Message> Messages { get; set; } = new();
    }
}
