using Newtonsoft.Json;
using Server.Service.Request;
using System.Net;

namespace Server.Service.User
{
    internal class DeleteUserService
    {
        /// <summary>
        /// Manipula a requisição de exclusão de um usuário com base no UID.
        /// </summary>
        /// <param name="req">Requisição HTTP recebida</param>
        /// <param name="resp">Resposta HTTP enviada</param>
        public static async Task HandleDeleteRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string jsonString = await UserService.JsonToString(req, resp);

            // Tenta desserializar o UID do corpo da requisição
            string uid;
            try
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                uid = data["uid"];
            }
            catch (Exception)
            {
                await ResponseHandle.WriteBadRequestResponse(resp, "UID inválido ou ausente no corpo da requisição.");
                return;
            }

            try
            {
                await UserService.DeleteUserAsync(uid);
                await ResponseHandle.WriteSuccessResponse(resp, $"Usuário {uid} excluído com sucesso.");
            }
            catch (Exception ex)
            {
                await ResponseHandle.WriteBadRequestResponse(resp, $"Erro ao excluir o usuário: {ex.Message}");
            }
            finally
            {
                // Fecha o objeto de resposta para liberar recursos
                resp.Close();
            }
        }

    }
}
