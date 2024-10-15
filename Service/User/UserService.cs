using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;

namespace Server.Service.User
{
    public class UserService
    {
        /// <summary>
        /// Chama o firebaseAdmin e cria um objeto usuário a partirdas informações do UserRecordsArgs convertido do UserDto.
        /// Não retorna metadados do usuário.
        /// </summary>
        /// <param name="UserArgs"></param>
        /// <returns>FirebaseUser object</returns>
        public static async Task<UserRecord> CreateUserAsync(UserRecordArgs UserArgs)
        {
            return await FirebaseAuth.DefaultInstance.CreateUserAsync(UserArgs);
        }

        /// <summary>
        /// Chama o FirebaseAdmin para recuperar os dados do usuário.
        /// Retorna metadados
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>UserRecord obj</returns>
        public static async Task<UserRecord> GetUserByIdAsync(string uid)
        {
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }

        /// <summary>
        /// Chama o FirebaseAdmin para deletar um usuário da base de dados.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>Se falhar: Error</returns>
        public static async Task DeleteUserAsync(string uid)
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
        }


        /// <summary>
        /// Lê o corpo da requisição HTTP e o retorna como uma string.
        /// </summary>
        /// <param name="req">O objeto HttpListenerRequest que representa a requisição HTTP.</param>
        /// <param name="resp">O objeto HttpListenerResponse que representa a resposta HTTP.</param>
        /// <returns>A string contendo o corpo da requisição.</returns>
        public static async Task<string> JsonToString(HttpListenerRequest req, HttpListenerResponse resp)
        {
            using var reader = new StreamReader(req.InputStream, req.ContentEncoding);
            return await reader.ReadToEndAsync();
        }
    }
}
