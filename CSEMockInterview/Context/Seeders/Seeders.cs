using Microsoft.EntityFrameworkCore;
using CSEMockInterview.Models;
using Microsoft.AspNetCore.Identity;

namespace CSEMockInterview.Context.Seeders
{
    public static class Seeders
    {
        public static void DataSeeder(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<Users>();

            var user = new Users
            {
                //    Id = new Guid("3e6e8842-9b66-432b-84dc-2294524f0063").ToString(),
                //    FirstName = "Juan",
                //    MiddleName = "Lopez",
                //    LastName = "Dela Cruz",
                //    Email = "juan@gmail.com",
                //    NormalizedEmail = "JUAN@GMAIL.COM",
                //    UserName = "Juan",
                //    NormalizedUserName = "JUAN"
                //};

                //user.PasswordHash = hasher.HashPassword(user, "admin123");

                //modelBuilder.Entity<Users>().HasData(user);
            };
    }

    }

}

