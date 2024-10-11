using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Server.Interface;

namespace Server.Service.User
{
    public class UserService
    {
        public async Task<UserRecord> CreateUserAsync(UserRecordArgs UserArgs)
        {
            return await FirebaseAuth.DefaultInstance.CreateUserAsync(UserArgs);
        }

        public async Task<UserRecord> GetUserByIdAsync(string uid)
        {
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }

        public async Task DeleteUserAsync(string uid)
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
        }
    }
}
