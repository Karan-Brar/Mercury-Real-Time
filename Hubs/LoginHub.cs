using MercuryMVC.Models;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.Web.Helpers;

namespace MercuryMVC.Hubs
{
    public class LoginHub : Hub
    {
        private ChattingContext context;

        public LoginHub(ChattingContext context) : base()
        {
            this.context = context;
        }

        public async Task RegisterUser(string username, string pass, string confirmPass)
        {
            bool userExists = false;
            bool passMatch = true;

            if(context.AppUsers.Any(u => u.UserName == username))
            {
                userExists = true;
                await Clients.Caller.SendAsync("UserExistsError");
            }

            if(!userExists && !pass.Equals(confirmPass))
            {
                passMatch = false;
                await Clients.Caller.SendAsync("PassMatchError");
            }

            if(!userExists && passMatch)
            {
                string passHash = Crypto.HashPassword(pass);
                context.AppUsers.Add(new AppUser { UserName = username, Password = passHash });
                context.SaveChanges();
                await Clients.Caller.SendAsync("UserRegistered");
            }


        }
    }
}
