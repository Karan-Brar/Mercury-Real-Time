using MercuryMVC.Models;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

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

        }
    }
}
