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
        /// Chama o FirebaseAdmin para recuperar os dados do usuário com base no ID do usuário (UID).
        /// </summary>
        /// <param name="uid">ID único do usuário no Firebase</param>
        /// <returns>Retorna um objeto <see cref="UserRecord"/> contendo os dados do usuário.</returns>
        public static async Task<UserRecord> GetUserByIdAsync(string uid)
        {
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }

        /// <summary>
        /// Chama o FirebaseAdmin para recuperar os dados do usuário com base no endereço de e-mail.
        /// </summary>
        /// <param name="email">Endereço de e-mail do usuário registrado no Firebase</param>
        /// <returns>Retorna um objeto <see cref="UserRecord"/> contendo os dados do usuário.</returns>
        public static async Task<UserRecord> GetUserByEmailAsync(string email)
        {
            return await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
        }

        /// <summary>
        /// Chama o FirebaseAdmin para recuperar os dados do usuário com base no número de telefone.
        /// </summary>
        /// <param name="phoneNumber">Número de telefone do usuário registrado no Firebase</param>
        /// <returns>Retorna um objeto <see cref="UserRecord"/> contendo os dados do usuário.</returns>
        public static async Task<UserRecord> GetUserByPhone(string phoneNumber)
        {
            return await FirebaseAuth.DefaultInstance.GetUserByPhoneNumberAsync(phoneNumber);
        }



        /// <summary>
        /// Recupera todos os usuários do Firebase Authentication.
        /// </summary>
        /// <returns>Uma lista de objetos UserRecord representando todos os usuários.</returns>
        public static async Task<List<ExportedUserRecord>> GetAllUsersAsync()
        {
            var allUsers = new List<ExportedUserRecord>();

            // Inicia a listagem de usuários a partir do início, 1000 de cada vez.
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
            var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();

            while (await responses.MoveNextAsync())
            {
                ExportedUserRecords response = responses.Current;

                allUsers.AddRange(response.Users);
            }

            return allUsers;
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
