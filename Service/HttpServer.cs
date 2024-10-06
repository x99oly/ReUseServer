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
                // Get the request context
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest req = context.Request;
                HttpListenerResponse resp = context.Response;

                // Check if the requested URL is '/index'
                if (req.Url.AbsolutePath == "/index")
                {
                    // Set the content type to HTML
                    resp.ContentType = "text/html";

                    // Read the index.html file
                    string serverRoot = AppDomain.CurrentDomain.BaseDirectory;
                    string htmlFilePath = @"..\..\..\View\index.html";
                    Console.WriteLine("Diretório atual: " + Environment.CurrentDirectory); // Captura o diretório que chamou o index

                    if (File.Exists(htmlFilePath))
                    {
                        string htmlContent = File.ReadAllText(htmlFilePath);
                        byte[] buffer = Encoding.UTF8.GetBytes(htmlContent);

                        // Write the HTML content to the response
                        resp.ContentLength64 = buffer.Length;
                        resp.OutputStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        // Handle file not found
                        byte[] errorBuffer = Encoding.UTF8.GetBytes("404 - File Not Found");
                        resp.StatusCode = (int)HttpStatusCode.NotFound;
                        resp.ContentLength64 = errorBuffer.Length;
                        resp.OutputStream.Write(errorBuffer, 0, errorBuffer.Length);
                    }
                }
                else
                {
                    // Handle other requests (you can expand this part)
                    byte[] buffer = Encoding.UTF8.GetBytes("Unknown request");
                    resp.ContentLength64 = buffer.Length;
                    resp.OutputStream.Write(buffer, 0, buffer.Length);
                }

                // Close the response
                resp.OutputStream.Close();
            }
        }
    }
}
