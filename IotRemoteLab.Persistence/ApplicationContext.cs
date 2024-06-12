using IotRemoteLab.Domain;
using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.Team;
using IotRemoteLab.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.Persistence;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Schedule> Schedule => Set<Schedule>();
    public DbSet<Stand> Stands => Set<Stand>();
}