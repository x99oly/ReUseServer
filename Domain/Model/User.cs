using FirebaseAdmin.Auth;

namespace Server.Domain.DTO
{
    internal class User
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? PhotoUrl { get; set; }
        public bool Disabled { get; set; }

        public User(String nome, string email, string senha)
        {
            DisplayName = nome;
            Email = email;
            Password = senha;

            EmailVerified = false;
            PhoneNumber = null;
            PhotoUrl = null;
            Disabled = false;
        }

        /// <summary>
        /// Converte o obj UserDto em UserRecordsArgs (exigido pelo firebase)
        /// </summary>
        /// <returns>UserRecordsArgs object</returns>
        public UserRecordArgs ToUserRecordArgs()
        {
            return new UserRecordArgs()
            {
                Email = this.Email,
                EmailVerified = this.EmailVerified,
                PhoneNumber = this.PhoneNumber,
                Password = this.Password,
                DisplayName = this.DisplayName,
                PhotoUrl = this.PhotoUrl,
                Disabled = this.Disabled,
            };
        }
    }
}
