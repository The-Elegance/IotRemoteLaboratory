using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Domain.Role;
using Microsoft.EntityFrameworkCore;
using IotRemoteLab.Domain.Stand.Benchboards;
using IotRemoteLab.Domain.Schedule;
using IotRemoteLab.Domain;

namespace IotRemoteLab.Persistence;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ScheduleBase> Schedule { get; set; }
    public DbSet<Stand> Stands { get; set; }
    public DbSet<Mcu> Mcus { get; set; }
    public DbSet<McuFramework> McuFrameworks { get; set; }
    public DbSet<Benchboard> Benchboards { get; set; }
    public DbSet<BenchboardPort> BenchboardPort { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<AcademyGroup> AcademyGroup { get; set; }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamHolderSchedule>();
        modelBuilder.Entity<UserHolderSchedule>();

        modelBuilder.Entity<Stand>()
            .HasMany(r => r.AvailableUarts)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "StandUart",
                r => r.HasOne<Uart>()
                    .WithMany()
                    .HasForeignKey("UartId")
                    .HasConstraintName("FK_StandUart_Uart")
                    .OnDelete(DeleteBehavior.Cascade),
                a => a.HasOne<Stand>()
                    .WithMany()
                    .HasForeignKey("StandId")
                    .HasConstraintName("FK_StandUart_Stand")
                    .OnDelete(DeleteBehavior.Cascade)
            );

        modelBuilder.Entity<Team>()
            .HasMany(r => r.Members)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "TeamUser",
                r => r.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_TeamUser_User")
                    .OnDelete(DeleteBehavior.Cascade),
                a => a.HasOne<Team>()
                    .WithMany()
                    .HasForeignKey("TeamId")
                    .HasConstraintName("FK_TeamUser_Team")
                    .OnDelete(DeleteBehavior.Cascade)
            );

        modelBuilder.Entity<ScheduleBase>()
            .HasMany(r => r.Stands)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ScheduleStand",
                r => r.HasOne<Stand>()
                    .WithMany()
                    .HasForeignKey("StandId")
                    .HasConstraintName("FK_ScheduleStand_Stand")
                    .OnDelete(DeleteBehavior.Cascade),
                a => a.HasOne<ScheduleBase>()
                    .WithMany()
                    .HasForeignKey("ScheduleId")
                    .HasConstraintName("FK_ScheduleStand_Schedule")
                    .OnDelete(DeleteBehavior.Cascade)
            );

        modelBuilder.Entity<University>()
            .HasMany(r => r.Groups)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UniversityAcademyGroup",
                a => a.HasOne<AcademyGroup>()
                    .WithMany()
                    .HasForeignKey("AcademyGroupId")
                    .HasConstraintName("FK_UniversityAcademyGroup_AcademyGroup")
                    .OnDelete(DeleteBehavior.Cascade),
                u => u.HasOne<University>()
                    .WithMany()
                    .HasForeignKey("UniversityId")
                    .HasConstraintName("FK_UniversityAcademyGroup_University")
                    .OnDelete(DeleteBehavior.Cascade)
            );

        base.OnModelCreating(modelBuilder);
    }
}