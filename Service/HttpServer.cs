using System;
using System.IO;
using System.Net;
using System.Text;

namespace Server.Service
{
    class HttpServer
    {
        public static void StartHttpServer()
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8001/"); // Endereço raiz da aplicação / servidor
            listener.Start();
            Console.WriteLine("Server is listening on http://localhost:8001/..." +
                "\nCheck our home page: http://localhost:8001/index");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                ProcessRequest(context);
            }
        }

        private static void ProcessRequest(HttpListenerContext context)
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

        private static void RespondWithUserList(HttpListenerResponse resp)
        {
            string jsonResponse = "[{\"id\": 1, \"name\": \"John Doe\"}, {\"id\": 2, \"name\": \"Jane Doe\"}]"; // Exemplo de resposta
            byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);
            resp.ContentType = "application/json";
            resp.ContentLength64 = buffer.Length;
            resp.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private static void HandleGetRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            if (req.Url.AbsolutePath == "/index")
            {
                ServeHtmlFile(resp, @"..\..\..\View\index.html");
            }
            else if (req.Url.AbsolutePath == "/users")
            {
                RespondWithUserList(resp);
            }
            else
            {
                HandleUnknownRequest(resp);
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

        private static void HandlePostRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            if (req.Url.AbsolutePath == "/users")
            {
                using var reader = new StreamReader(req.InputStream, req.ContentEncoding);
                string body = reader.ReadToEnd();

                // Adicione a lógica para criar um novo usuário usando os dados do body
                Console.WriteLine($"New user data: {body}");

                // Responde o front
                byte[] buffer = Encoding.UTF8.GetBytes("{\"message\":\"User created successfully.\"}");
                resp.ContentType = "application/json";
                resp.ContentLength64 = buffer.Length;
                resp.OutputStream.Write(buffer, 0, buffer.Length);

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
