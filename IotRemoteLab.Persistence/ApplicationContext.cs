﻿using IotRemoteLab.Domain.Team;
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
}