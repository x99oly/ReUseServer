using System;
using System.Net;

// Personal Namespaces
using Server.Service.Controller;
using Server.Domain.Interface;

namespace Server.Service.Host
{
    class HttpServer : IServidor
    {
        IController controller = new MainController();

        /// <summary>
        /// Inicia o serverHTTP e fornece os links das páginas no console.
        /// </summary>
        public void StartServer(string url)
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add($"{url}"); // Endereço raiz da aplicação / servidor
            listener.Start();
            Console.WriteLine($"Server is listening on {url}..." +
                $"\nCheck our home page: {url}index" +
                $"\nCheck our register page: {url}cadastro"+
                $"\nCheck our user list page: {url}users"+
                $"\nCheck our address list page: {url}address");

            StartServerListener(listener);

        }

        private void StartServerListener(HttpListener listener)
        {
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                controller.ProcessRequest(context);
            }
        }

    }
       
}
