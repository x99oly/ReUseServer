using Server.Domain.Interface;
using System.Net;
using System.Text;

namespace Server.Service.Controller
{
    internal class MainController : IController
    {
        public async Task ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest req = context.Request;
            HttpListenerResponse resp = context.Response;

            IController controller = null;

            // Verifica o caminho da URL para decidir qual controlador usar
            if (!req.Url.AbsolutePath.StartsWith("/api"))
            {
                HandleGetRequest(req, resp);
            }
            else if (req.Url.AbsolutePath.StartsWith("/api/users"))
            {
                controller = new UserController();
                await controller.ProcessRequest(context);

            }
            else if (req.Url.AbsolutePath.StartsWith("/api/address"))
            {
                controller = new AddressController();
                await controller.ProcessRequest(context);
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
            else if (req.Url.AbsolutePath == "/address")
            {
                ServeHtmlFile(resp, @"..\..\..\View\address.html");
            }
            else
            {
                ServeHtmlFile(resp, @"..\..\..\View\index.html");
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

    }
}
