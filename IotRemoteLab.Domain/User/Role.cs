using Microsoft.AspNetCore.Identity;

namespace IotRemoteLab.Domain.User;

public class Role : IdentityRole<Guid>
{
    public  IReadOnlyList<User> Users { get; set; }
}