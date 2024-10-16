using Server.Service.User;
using System.Net;
using System.Text;

namespace Server.Service.Controller
{
    internal class UserController
    {
        public async void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest req = context.Request;
            HttpListenerResponse resp = context.Response;

            switch (req.HttpMethod)
            {
                case "GET":
                    HandleGetRequest(req, resp);
                    break;

                case "DELETE":
                    DeleteUserService.HandleDeleteRequest(req, resp);
                    break;

                case "POST":
                    await PostUserService.HandlePostRequest(req, resp);
                    break;

                default:
                    HandleGetRequest(req, resp);
                    break;
            }
        }
        private static void ServeHtmlFile(HttpListenerResponse resp, string filePath)
        {
            resp.ContentType = "text/html";

            if (File.Exists(filePath))
            {
                string htmlContent = File.ReadAllText(filePath);
                byte[] buffer = Encoding.UTF8.GetBytes(htmlContent);
                resp.ContentLength64 = buffer.Length;
                resp.OutputStream.Write(buffer, 0, buffer.Length);
            }

        }

        private static void HandleGetRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            if (req.Url.AbsolutePath == "/cadastro")
            {
                ServeHtmlFile(resp, @"..\..\..\View\cadastro.html");
            }
            else if (req.Url.AbsolutePath == "/users")
            {
                ServeHtmlFile(resp, @"..\..\..\View\users.html");
            }
            else if (req.Url.AbsolutePath == "/api/users")
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
            else
            {
                ServeHtmlFile(resp, @"..\..\..\View\index.html");
            }
        }

    }

}

