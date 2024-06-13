using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Blazor.Models
{
	public class RegistrationUserData
	{
		[MinLength(4)]
		public string Login { get; set; }
		[MinLength(1)]
		public string Name { get; set; }
		[MinLength(1)]
		public string Surname { get; set; }
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }
		[MinLength(1)]
		public string Password { get; set; }
	}
}
