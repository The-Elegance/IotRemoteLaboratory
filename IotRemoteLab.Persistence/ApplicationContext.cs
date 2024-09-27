using IotRemoteLab.Domain;
using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.Team;
using IotRemoteLab.Domain.User;
using Microsoft.EntityFrameworkCore;
using IotRemoteLab.Domain.Stand.Benchboards;

namespace IotRemoteLab.Persistence;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Schedule> Schedule { get; set; }
    public DbSet<Stand> Stands { get; set; }
    public DbSet<Mcu> Mcus { get; set; }
    public DbSet<McuFramework> McuFrameworks { get; set; }
    public DbSet<Benchboard> Benchboards { get; set; }
    public DbSet<BenchboardPort> BenchboardPort { get; set; }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}