using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using System.Net;
using Server.Service.Request;

namespace Server.Service.User
{
    /// <summary>
    /// Classe responsável por gerenciar requisições para criação de usuários.
    /// </summary>
    public class PostUserService
    {
        /// <summary>
        /// Método assíncrono que lida com requisições HTTP do tipo POST para criar um novo usuário.
        /// </summary>
        /// <param name="req">O objeto HttpListenerRequest que representa a requisição HTTP recebida.</param>
        /// <param name="resp">O objeto HttpListenerResponse que representa a resposta HTTP a ser enviada ao cliente.</param>
        public static async Task HandlePostRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string jsonString = await JsonToString(req, resp);

            UserRecordArgs user = ReturnUser(jsonString);

            var register = new UserService();

            try
            {
                await register.CreateUserAsync(user);
                await ResponseHandle.WriteSuccessResponse(resp, "Usuário Criado com sucesso!");
            }
            catch (Exception ex)
            {
                await ResponseHandle.WriteBadRequestResponse(resp, $"Falha ao criar usuário: {ex}");
            }
            finally
            {
                /// Fecha o objeto de resposta para liberar recursos
                resp.Close();
            }
        }

        /// <summary>
        /// Converte uma string JSON em um objeto UserRecordArgs.
        /// </summary>
        /// <param name="jsonString">A string JSON que representa os dados do usuário.</param>
        /// <returns>Um objeto UserRecordArgs correspondente aos dados do usuário.</returns>
        private static UserRecordArgs ReturnUser(string jsonString)
        {
            Domain.DTO.User? user = JsonConvert.DeserializeObject<Domain.DTO.User>(jsonString);

            return user.ToUserRecordArgs();
        }

        /// <summary>
        /// Lê o corpo da requisição HTTP e o retorna como uma string.
        /// </summary>
        /// <param name="req">O objeto HttpListenerRequest que representa a requisição HTTP.</param>
        /// <param name="resp">O objeto HttpListenerResponse que representa a resposta HTTP.</param>
        /// <returns>A string contendo o corpo da requisição.</returns>
        private static async Task<string> JsonToString(HttpListenerRequest req, HttpListenerResponse resp)
        {
            using var reader = new StreamReader(req.InputStream, req.ContentEncoding);
            return await reader.ReadToEndAsync();
        }
    }
}
