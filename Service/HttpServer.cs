using System;
using System.Net;
using System.Text;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
// Personal Namespaces
using Server.Service.User;
using Server.Interface;

namespace Server.Service
{
    class HttpServer : IServidor
    {
        /// <summary>
        /// Inicia o serverHTTP e fornece os links das páginas no console.
        /// </summary>
        public void StartServer(string url)
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add($"{url}"); // Endereço raiz da aplicação / servidor
            listener.Start();
            Console.WriteLine($"Server is listening on {url}..." +
                $"\nCheck our home page: {url}index"+
                $"\nCheck our register paga: {url}cadastro");

            StartServerListner(listener);
            
        }

        private void StartServerListner(HttpListener listener)
        {
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                ProcessRequest(context);
            }
        }

        private async static void ProcessRequest(HttpListenerContext context)
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

                default:
                    HandleUnknownRequest(resp);
                    break;
            }

            resp.OutputStream.Close();
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
                else
                {
                    RespondWithNotFound(resp);
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
            else
            {
                RespondWithNotFound(resp);
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

        private static void HandleDeleteRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            if (req.Url.AbsolutePath.StartsWith("/users/"))
            {
                string userId = req.Url.AbsolutePath.Substring("/users/".Length);
                // Adicione a lógica para deletar o usuário com o userId
                Console.WriteLine($"User {userId} deleted.");
                byte[] buffer = Encoding.UTF8.GetBytes($"User {userId} deleted.");
                resp.ContentLength64 = buffer.Length;
                resp.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                HandleUnknownRequest(resp);
            }
        }

        private static async void HandlePostRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            if (req.Url.AbsolutePath == "/users")
            {
                using var reader = new StreamReader(req.InputStream, req.ContentEncoding);
                string jsonString = await reader.ReadToEndAsync();

                Domain.DTO.User? user = JsonConvert.DeserializeObject<Domain.DTO.User>(jsonString);

                UserRecordArgs userRecordArgs = user.ToUserRecordArgs();

                var register = new UserService();

                try
                {
                    await register.CreateUserAsync(userRecordArgs);
                    resp.StatusCode = (int)HttpStatusCode.Created;
                    resp.StatusDescription = "Created";
                    await resp.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("{\"message\":\"Situado na mata\"}"));
                }
                catch (Exception ex)
                {
                    resp.StatusCode = (int)HttpStatusCode.BadRequest;
                    resp.StatusDescription = "Bad Request";
                    await resp.OutputStream.WriteAsync(Encoding.UTF8.GetBytes($"{{\"message\":\"{ex.Message}\"}}")); // Retorna mensagem em formato JSON
                }
                finally
                {
                    resp.Close();
                }
            }
            else
            {
                HandleUnknownRequest(resp);
            }
        }

        private static void HandleUnknownRequest(HttpListenerResponse resp)
        {
            byte[] buffer = Encoding.UTF8.GetBytes("Unknown request");
            resp.ContentLength64 = buffer.Length;
            resp.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private static void RespondWithNotFound(HttpListenerResponse resp)
        {
            byte[] errorBuffer = Encoding.UTF8.GetBytes("404 - File Not Found");
            resp.StatusCode = (int)HttpStatusCode.NotFound;
            resp.ContentLength64 = errorBuffer.Length;
            resp.OutputStream.Write(errorBuffer, 0, errorBuffer.Length);
        }
    }
}
