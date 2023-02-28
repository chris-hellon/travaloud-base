namespace Travaloud.Core.Models
{
	public class ValidateUserDTO
	{
		public string Username { get; set; }
		public string Password { get; set; }

        public ValidateUserDTO()
        {

        }

        public ValidateUserDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}

