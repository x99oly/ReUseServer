using System;
using System.Collections.Generic;
using System.Linq;
using FirebaseAdmin.Auth;

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
        public async Task<UserRecord> CreateUserAsync(UserRecordArgs UserArgs)
        {
            return await FirebaseAuth.DefaultInstance.CreateUserAsync(UserArgs);
        }

        /// <summary>
        /// Chama o FirebaseAdmin para recuperar os dados do usuário.
        /// Retorna metadados
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>UserRecord obj</returns>
        public async Task<UserRecord> GetUserByIdAsync(string uid)
        {
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }

        /// <summary>
        /// Chama o FirebaseAdmin para deletar um usuário da base de dados.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>Se falhar: Error</returns>
        public async Task DeleteUserAsync(string uid)
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
        }
    }
}
