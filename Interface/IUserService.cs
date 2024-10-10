using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interface
{
    public interface IUserService
    {
        Task<UserRecord> CreateUserAsync(UserRecordArgs userArgs);
        Task<UserRecord> GetUserByIdAsync(string uid);
        Task DeleteUserAsync(string uid);
    }
}
