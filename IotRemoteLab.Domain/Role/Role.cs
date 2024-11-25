namespace IotRemoteLab.Domain.Role;

public class Role
{
    public Guid Id { get; set; } 
    public string Name { get; set; } 
    public  IReadOnlyList<User> Users { get; set; }
}