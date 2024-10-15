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
                    // HandleDeleteRequest(req, resp);
                    break;

                case "POST":
                    await PostUserService.HandlePostRequest(req, resp);
                    break;

                default:
                    // HandleUnknownRequest(resp);
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

        private static void ServeRegisterFile(HttpListenerResponse resp, string filePath)
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
                ServeRegisterFile(resp, @"..\..\..\View\cadastro.html");
            }
            else
            {
                ServeHtmlFile(resp, @"..\..\..\View\index.html");
            }


        }
    }

}

