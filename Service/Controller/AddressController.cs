using Server.Service.User;
using System.Net;
using Server.Domain.Interface;
using System.Text;
using Server.Domain.Model;
using System.Text.Json;

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
            string requestBody = null;
            using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
            {
                // Lê o corpo da requisição
                requestBody = await reader.ReadToEndAsync();
            }

            // Exibe os dados recebidos no console
            Console.WriteLine($"\nDados recebidos: {requestBody}\n" );

            // Agora cria o objeto Address manualmente
            var address = Address.ParseAddressJson(requestBody);

            // Exibe o endereço criado no console
            Console.WriteLine("Endereço recebido: " +
                $"\nUserId: {address.UserId}, " +
                $"\nCEP: {address.PostalCode}, " +
                $"\nRua: {address.Street}, " +
                $"\nNúmero: {address.Number}, " +
                $"\nBairro: {address.Neighborhood}, " +
                $"\nCidade: {address.City}, " +
                $"\nEstado: {address.State}, " +
                $"\nComplemento: {address.Complement}");

            // Resposta de sucesso
            resp.StatusCode = (int)HttpStatusCode.OK;
            resp.ContentType = "application/json";
            await resp.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("{\"message\":\"Dados recebidos com sucesso\"}"), 0, "{\"message\":\"Dados recebidos com sucesso\"}".Length);
            resp.Close();
        }

    }
}
