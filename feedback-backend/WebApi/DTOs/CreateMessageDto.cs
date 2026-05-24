using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class CreateMessageDto
    {
        [Required(ErrorMessage = "Имя обязательно.")]
        [MaxLength(100, ErrorMessage = "Имя не может быть длиннее 100 символов.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email.")]
        [MaxLength(200, ErrorMessage = "Email не может быть длиннее 200 символов.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефон обязателен.")]
        [RegularExpression(@"^\+7\d{10}$", ErrorMessage = "Телефон должен быть в формате +7XXXXXXXXXX.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тема обязательна.")]
        [Range(1, int.MaxValue, ErrorMessage = "Выберите корректную тему.")]
        public int ThemeId { get; set; }

        [Required(ErrorMessage = "Сообщение обязательно.")]
        [MinLength(5, ErrorMessage = "Сообщение слишком короткое (минимум 5 символов).")]
        [MaxLength(2000, ErrorMessage = "Сообщение не может быть длиннее 2000 символов.")]
        public string Content { get; set; } = string.Empty;
    }

    public class CreateMessageDtoResponse
    {
        public int MessageId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string ThemeName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
    }
}
