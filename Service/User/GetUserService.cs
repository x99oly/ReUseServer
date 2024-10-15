using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Service.User
{
    internal class GetUserService
    {
        public static async Task HandleGtRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string jsonString = await UserService.JsonToString(req, resp);

        }
    }
}
