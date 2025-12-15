//using System;
//using System.Collections.Generic;
//using CSEMockInterview.Migrations;
using CSEMockInterview.Context.Seeders;
using CSEMockInterview.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CSEMockInterview.Context;

public partial class MyDbContext : IdentityDbContext<Users, IdentityRole, string>
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<Users> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Seeders.Seeders.DataSeeder(modelBuilder);
    }

  
}
