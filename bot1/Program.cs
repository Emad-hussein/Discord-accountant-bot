
using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CountBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private int _count = 0;
        private const char COMMAND_PREFIX = '!';

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            _client.MessageReceived += HandleMessage;

            var token = "MTA4NTY0NDk5OTQ2MDQwMTIxMw.GhXFqr.EpEIFJPL4DdWyJG3xEItQwpYga4OfZXaDO727M";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task HandleMessage(SocketMessage message)
        {
            if (message.Author.IsBot) return;

            if (message.Content.StartsWith(COMMAND_PREFIX))
            {
                var commandBody = message.Content.Substring(1).ToLower();
                if (commandBody == "count")
                {
                    _count++;
                    await message.Channel.SendMessageAsync($"Count is now {_count}");
                }
                else if (commandBody.StartsWith("charge "))
                {
                    string name = commandBody.Substring(7);
                    _count++;
                    await message.Channel.SendMessageAsync($"Charging {name}... Count is now {_count}");
                }
            }
        }
    }
}


// MTA4NTY0NDk5OTQ2MDQwMTIxMw.GhXFqr.EpEIFJPL4DdWyJG3xEItQwpYga4OfZXaDO727M