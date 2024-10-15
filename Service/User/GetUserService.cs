using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using Server.Domain.DTO;
using Server.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Service.User
{
    internal class GetUserService
    {
        /// <summary>
        /// A função recebe uma solicitação HTTP, chama o serviço que recupera todos os usuários do Firebase, 
        /// e retorna a lista de usuários em formato JSON. Caso ocorra algum erro, retorna uma resposta "Not Found".
        /// O ciclo de vida da resposta é encerrado no bloco "finally" com a chamada ao método `resp.Close()`.
        /// </summary>
        /// <param name="req">Requisição HTTP recebida pelo servidor</param>
        /// <param name="resp">Resposta HTTP a ser enviada de volta ao cliente</param>
        /// <returns>Task assíncrona que processa a requisição</returns>
        public static async Task HandleGetAllUsersRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string jsonString = await UserService.JsonToString(req, resp);

            try
            {
                List<ExportedUserRecord> users = await UserService.GetAllUsersAsync();

                string jsonResponse = JsonConvert.SerializeObject(users);
                resp.ContentType = "application/json";

                await ResponseHandle.WriteSuccessResponse(resp, jsonResponse);
            }
            catch (Exception ex)
            {
                await ResponseHandle.WriteNotFoundResponse(resp);
            }
            finally
            {
                // Fecha a resposta HTTP para liberar os recursos
                resp.Close();
            }
        }

        /// <summary>
        /// Manipula a requisição HTTP para obter um usuário com base no UID.
        /// </summary>
        /// <param name="req">Requisição HTTP recebida pelo servidor, contendo o UID.</param>
        /// <param name="resp">Resposta HTTP a ser enviada de volta ao cliente.</param>
        /// <returns>Task assíncrona que processa a requisição.</returns>
        public static async Task HandleGetUserByIdRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string uid = req.QueryString["uid"]; // Obtém o UID da query string

            if (string.IsNullOrEmpty(uid))
            {
                await ResponseHandle.WriteBadRequestResponse(resp, "Usuário não encontrado.");
                return;
            }

            try
            {
                UserRecord user = await UserService.GetUserByIdAsync(uid);
                string jsonResponse = JsonConvert.SerializeObject(user);

                resp.ContentType = "application/json";
                await ResponseHandle.WriteSuccessResponse(resp, jsonResponse);
            }
            catch (Exception ex)
            {
                await ResponseHandle.WriteNotFoundResponse(resp);
            }
            finally
            {
                resp.Close();
            }
        }

        /// <summary>
        /// Manipula a requisição HTTP para obter um usuário com base no email.
        /// </summary>
        /// <param name="req">Requisição HTTP recebida pelo servidor, contendo o email.</param>
        /// <param name="resp">Resposta HTTP a ser enviada de volta ao cliente.</param>
        /// <returns>Task assíncrona que processa a requisição.</returns>
        public static async Task HandleGetUserByEmailRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string email = req.QueryString["email"]; // Obtém o email da query string

            if (string.IsNullOrEmpty(email))
            {
                await ResponseHandle.WriteBadRequestResponse(resp, "Usuário não encontrado.");
                return;
            }

            try
            {
                UserRecord user = await UserService.GetUserByEmailAsync(email);
                string jsonResponse = JsonConvert.SerializeObject(user);

                resp.ContentType = "application/json";
                await ResponseHandle.WriteSuccessResponse(resp, jsonResponse);
            }
            catch (Exception ex)
            {
                await ResponseHandle.WriteNotFoundResponse(resp);
            }
            finally
            {
                resp.Close();
            }
        }

        /// <summary>
        /// Manipula a requisição HTTP para obter um usuário com base no número de telefone.
        /// </summary>
        /// <param name="req">Requisição HTTP recebida pelo servidor, contendo o número de telefone.</param>
        /// <param name="resp">Resposta HTTP a ser enviada de volta ao cliente.</param>
        /// <returns>Task assíncrona que processa a requisição.</returns>
        public static async Task HandleGetUserByPhoneRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string phoneNumber = req.QueryString["phone"]; // Obtém o número de telefone da query string

            if (string.IsNullOrEmpty(phoneNumber))
            {
                await ResponseHandle.WriteBadRequestResponse(resp, "Usuário não encontrado.");
                return;
            }

            try
            {
                UserRecord user = await UserService.GetUserByPhone(phoneNumber);
                string jsonResponse = JsonConvert.SerializeObject(user);

                resp.ContentType = "application/json";
                await ResponseHandle.WriteSuccessResponse(resp, jsonResponse);
            }
            catch (Exception ex)
            {
                await ResponseHandle.WriteNotFoundResponse(resp);
            }
            finally
            {
                resp.Close();
            }
        }
    }


}
