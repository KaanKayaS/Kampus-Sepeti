using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace KampüsSepeti.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatHistoryController : ControllerBase
    {
        private readonly string _chatDataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] string user1, [FromQuery] string user2)
        {
            if (string.IsNullOrEmpty(user1) || string.IsNullOrEmpty(user2))
                return BadRequest("Both user1 and user2 are required.");

            var fileName = GetChatFileName(user1, user2);
            var filePath = Path.Combine(_chatDataPath, fileName);
            if (!System.IO.File.Exists(filePath))
                return Ok(new List<ChatMessageDto>());

            var json = await System.IO.File.ReadAllTextAsync(filePath);
            var messages = JsonSerializer.Deserialize<List<ChatMessageDto>>(json) ?? new List<ChatMessageDto>();
            return Ok(messages);
        }

        private string GetChatFileName(string user1, string user2)
        {
            var users = new List<string> { user1.ToLower(), user2.ToLower() };
            users.Sort();
            return $"chat_{users[0]}_{users[1]}.json";
        }
    }

    public class ChatMessageDto
    {
        public string From { get; set; }
        public string Message { get; set; }
        public string Timestamp { get; set; }
    }
} 