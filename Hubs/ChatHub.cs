using MercuryMVC.Models;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace MercuryMVC.Hubs
{
    public class ChatHub : Hub
    {
        private ChattingContext context;

        public ChatHub(ChattingContext context) : base()
        {
            this.context = context;
        }

        public async Task RetrieveData()
        {
            List<Message> messages = context.Messages.Include(m => m.AppUser).ToList();

            List<string> previousMessages = new List<string>();

            foreach (Message m in messages)
            {
                JObject messageDetails = new JObject();
                messageDetails["messageText"] = m.MessageText;
                messageDetails["sentBy"] = m.AppUser.UserName;

                string messageDetailsJson = messageDetails.ToString();

                previousMessages.Add(messageDetailsJson);
            }

            await Clients.Caller.SendAsync("LoadMessages", previousMessages);
        }

        public async Task SendMessage(string user, string message)
        {
            if (user.Trim() == "" || message.Trim() == "")
            {
                await Clients.Caller.SendAsync("ShowEmptyError");
            }
            else
            {
                Message messageSent = new Message { MessageText = message, TimeSent = DateTime.Now };

                if (context.AppUsers.Any(u => u.UserName == user))
                {
                    AppUser existingUser = context.AppUsers.Where(u => u.UserName == user).FirstOrDefault();
                    existingUser.Messages.Add(messageSent);
                }
                else
                {
                    AppUser newAppUser = new AppUser { UserName = user };
                    newAppUser.Messages.Add(messageSent);

                    context.AppUsers.Add(newAppUser);
                }

                context.SaveChanges();
                await Clients.All.SendAsync("ReceiveMessage", user, message);
                Console.WriteLine("Receive message invoked!");
            }
        }
    }
}
