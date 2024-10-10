namespace Server.Domain.DTO
{
    internal class UserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; }
        public bool Disabled { get; set; }

        public UserDto(String nome, string email, string senha)
        {
            DisplayName = nome;
            Email = email;
            Password = senha;

            EmailVerified = false;
            PhoneNumber = null;
            PhotoUrl = null;
            Disabled = false;
        }
    }
}
