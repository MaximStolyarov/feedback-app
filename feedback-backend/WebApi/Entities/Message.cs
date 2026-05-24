namespace WebApi.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public int ContactId { get; set;  }
        public Contact Contact { get; set; } = null!;

        public int ThemeId { get; set; }
        public MessageTheme Theme { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
