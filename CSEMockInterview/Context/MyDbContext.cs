//using System;
//using System.Collections.Generic;
//using CSEMockInterview.Migrations;
using CSEMockInterview.Context.Seeders;
using CSEMockInterview.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Results = CSEMockInterview.Models.Results;


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
    public DbSet<Category> Category { get; set; }
    public DbSet<SubCategories> SubCategory { get; set; }
    public DbSet<Paragraphs> Paragraph { get; set; }
    public DbSet<Questions> Question { get; set; }
    public DbSet<YearPeriods> YearPeriod { get; set; }
    public DbSet<Choices> Choice { get; set; }
    public DbSet<UserAnswers> UserAnswer { get; set; }
    public DbSet<Results> Result  { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Seeders.Seeders.DataSeeder(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasMany(e => e.SubCategories)
            .WithOne(e => e.categoryNavigation)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired();

        modelBuilder.Entity<SubCategories> ()
            .HasMany(e => e.QuestionsCollection)
            .WithOne(e => e.SubCategoryNavigation)
            .HasForeignKey(e => e.SubCategoryId)
            .IsRequired();

        modelBuilder.Entity<Paragraphs>() 
            .HasMany(e => e.QuestionsCollection)
            .WithOne(e => e.ParagraphNavigation)
            .HasForeignKey(e => e.ParagraphId)
            .IsRequired(false);

        modelBuilder.Entity<Questions>()
            .HasMany(e => e.ChoicesCollection)
            .WithOne(e => e.QuestionsNavigation)
            .HasForeignKey(e => e.QuestionId)
            .IsRequired();

        modelBuilder.Entity<YearPeriods>()
            .HasMany(e => e.QuestionsCollection)
            .WithOne(e => e.YearPeriodNavigation)
            .HasForeignKey(e => e.YearPeriodId)
            .IsRequired();

        modelBuilder.Entity<Choices>()
            .HasMany(e => e.UserAnswersCollection)
            .WithOne(e => e.ChoicesNavigation)
            .HasForeignKey(e => e.Answer)
            .IsRequired();

        modelBuilder.Entity<Users>()
            .HasMany(e => e.UserAnswersCollection)
            .WithOne(e => e.UserNavigation)
            .HasForeignKey (e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<Users>()
            .HasMany(e => e.ResultsCollection)
            .WithOne (e => e.UsersNavigation)
            .HasForeignKey (e => e.UserId)
            .IsRequired();
    }

  
}
