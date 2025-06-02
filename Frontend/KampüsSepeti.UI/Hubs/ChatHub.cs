using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace KampüsSepeti.UI.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, string> _userConnections = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, string> _connectionToUser = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendPrivateMessage(string toUserId, string connectionId, string message)
        {
            var fromUserId = Context.User?.Claims.FirstOrDefault(c => c.Type == "email")?.Value
                          ?? Context.User?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
                          ?? Context.User?.Identity?.Name;

            if (string.IsNullOrEmpty(fromUserId) || string.IsNullOrEmpty(toUserId))
                return;

            // Mesajı dosyaya kaydet
            await SavePrivateMessageToFile(fromUserId, toUserId, message);

            if (_userConnections.TryGetValue(toUserId, out var targetConnectionId))
            {
                // Sadece alıcıya gönder
                await Clients.Client(targetConnectionId).SendAsync("ReceivePrivateMessage", fromUserId, message);
                // Gönderene de gönder
                await Clients.Caller.SendAsync("ReceivePrivateMessage", fromUserId, message);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Sistem", $"{toUserId} kullanıcısı çevrimdışı.");
            }
        }

        private async Task SavePrivateMessageToFile(string fromUser, string toUser, string message)
        {
            var users = new List<string> { fromUser.ToLower(), toUser.ToLower() };
            users.Sort();
            var fileName = $"chat_{users[0]}_{users[1]}.json";
            var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
            var filePath = Path.Combine(dataPath, fileName);

            List<ChatMessageDto> messages = new List<ChatMessageDto>();
            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                messages = JsonSerializer.Deserialize<List<ChatMessageDto>>(json) ?? new List<ChatMessageDto>();
            }
            messages.Add(new ChatMessageDto
            {
                From = fromUser,
                Message = message,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            var updatedJson = JsonSerializer.Serialize(messages);
            await File.WriteAllTextAsync(filePath, updatedJson);
        }

        private class ChatMessageDto
        {
            public string From { get; set; }
            public string Message { get; set; }
            public string Timestamp { get; set; }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == "email")?.Value
                        ?? Context.User?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
                        ?? Context.User?.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Sistem", "Giriş yapmadan sohbet edemezsiniz.");
                Context.Abort();
                return;
            }

            _userConnections.TryAdd(userId, Context.ConnectionId);
            _connectionToUser.TryAdd(Context.ConnectionId, userId);

            await base.OnConnectedAsync();
            await Clients.All.SendAsync("UserConnected", userId);
            await Clients.All.SendAsync("UpdateUserList", _userConnections.Keys);
            
            // Kullanıcı connection ID'lerini gönder
            var userConnectionMap = _userConnections.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            await Clients.All.SendAsync("UpdateUserConnectionIds", userConnectionMap);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == "email")?.Value
                      ?? Context.User?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
                      ?? Context.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections.TryRemove(userId, out _);
                _connectionToUser.TryRemove(Context.ConnectionId, out _);
                await Clients.All.SendAsync("UserDisconnected", userId);
                await Clients.All.SendAsync("UpdateUserList", _userConnections.Keys);
                
                // Güncel connection ID'leri gönder
                var userConnectionMap = _userConnections.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                await Clients.All.SendAsync("UpdateUserConnectionIds", userConnectionMap);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public Task<string[]> GetUserList()
        {
            return Task.FromResult(_userConnections.Keys.ToArray());
        }
    }
} 