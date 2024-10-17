using Server.Service.User;
using System.Net;
using Server.Domain.Interface;

namespace Server.Service.Controller
{
    internal class AddressController : IController
    {
        public async Task ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest req = context.Request;
            HttpListenerResponse resp = context.Response;

            switch (req.HttpMethod)
            {
                case "GET":
                    HandleGetRequest(req, resp);
                    break;

                case "DELETE":
                    HandleDeleteRequest(req, resp);
                    break;

                case "POST":
                    HandlePostRequest(req, resp);
                    break;

                case "PUT":
                    HandlePutRequest(req, resp);
                    break;

                default:
                    HandleGetRequest(req, resp);
                    break;
            }
        }    

        public void HandleGetRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {            
            if (req.Url.AbsolutePath == "/api/users")
            {
                resp.ContentType = "application/json";
                GetUserService.HandleGetAllUsersRequest(req, resp);
            }
            // Novo endpoint para buscar usuário por ID
            else if (req.Url.AbsolutePath == "/api/users/id")
            {
                resp.ContentType = "application/json";
                GetUserService.HandleGetUserByIdRequest(req, resp);
            }
            // Novo endpoint para buscar usuário por email
            else if (req.Url.AbsolutePath == "/api/users/email")
            {
                resp.ContentType = "application/json";
                GetUserService.HandleGetUserByEmailRequest(req, resp);
            }
            // Novo endpoint para buscar usuário por telefone
            else if (req.Url.AbsolutePath == "/api/users/phone")
            {
                resp.ContentType = "application/json";
                GetUserService.HandleGetUserByPhoneRequest(req, resp);
            }
        }

        public async void HandlePutRequest(HttpListenerRequest req, HttpListenerResponse resp) { }
        public async void HandleDeleteRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            await DeleteUserService.HandleDeleteRequest(req, resp);
        }
        public async void HandlePostRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            await PostUserService.HandlePostRequest(req, resp);
        }

    }
}
