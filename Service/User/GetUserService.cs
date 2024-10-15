using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using Server.Domain.DTO;
using Server.Service.Request;
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
        public static async Task HandleGetAllUsersRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string jsonString = await UserService.JsonToString(req, resp);

            try
            {
                List<ExportedUserRecord> users = await UserService.GetAllUsersAsync();
                string jsonResponse = JsonConvert.SerializeObject(users);

                resp.ContentType = "application/json";
                await ResponseHandle.WriteSuccessResponse(resp, jsonResponse);
            }
            catch (Exception ex)
            {
                await ResponseHandle.WriteNotFoundResponse(resp);
            }
            finally
            {
                /// Fecha o objeto de resposta para liberar recursos
                resp.Close();
            }
        }
    }
}
