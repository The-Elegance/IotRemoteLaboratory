using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Blazor.Models
{
    public class AuthorizationUserData
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(1)]
        public string Password { get; set; }
    }
}
