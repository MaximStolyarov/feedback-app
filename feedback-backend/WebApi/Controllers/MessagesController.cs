using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessagesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("themes")] 
        public async Task<IActionResult> GetThemes() 
        {
            var themes = await _context.MessageThemes
                .Select(t => new { t.Id, t.Name })
                .ToListAsync();
            return Ok(themes);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CreateMessageDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(x => x.Email == request.Email && x.Phone == request.Phone);

            if (contact is null)
            {
                contact = new Contact
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone
                };
                _context.Contacts.Add(contact);
            }

            var themeExist = await _context.MessageThemes.AnyAsync(x => x.Id == request.ThemeId);

            if (!themeExist)
                return BadRequest(new {error = "Указанная тема не найдена!"});

            var message = new Message
            {
                Text = request.Content,
                ThemeId = request.ThemeId,
                Contact = contact,
                CreatedAt = DateTime.UtcNow
            };
            _context.Messages.Add(message);

            await _context.SaveChangesAsync();

            await _context.Entry(message).Reference(m => m.Theme).LoadAsync();
            await _context.Entry(message).Reference(m => m.Contact).LoadAsync();

            var response = new CreateMessageDtoResponse
            {
                MessageId = message.Id,
                Text = message.Text,
                ThemeName = message.Theme.Name,
                CreatedAt = message.CreatedAt,
                ContactName = message.Contact.Name,
                ContactEmail = message.Contact.Email,
                ContactPhone = message.Contact.Phone
            };

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await _context.Messages
                .Include(m => m.Contact)
                .Include(m => m.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (message is null)
                return NotFound(new { error = $"Сообщение с id={id} не найдено." });

            var response = new CreateMessageDtoResponse
            {
                MessageId = message.Id,
                Text = message.Text,
                ThemeName = message.Theme.Name,
                CreatedAt = message.CreatedAt,
                ContactName = message.Contact.Name,
                ContactEmail = message.Contact.Email,
                ContactPhone = message.Contact.Phone
            };

            return Ok(response);
        }
    }
}
