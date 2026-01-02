using Backend.Models;
using Backend.Models.enums;
using Microsoft.EntityFrameworkCore;

namespace CSEMockInterview.Context.Seeders
{
    public class CategorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = Categories.Verbal },
                new Category { Id = 2, CategoryName = Categories.Analytical },
                new Category { Id = 3, CategoryName = Categories.Clerical },
                new Category { Id = 4, CategoryName = Categories.Numerical },
                new Category { Id = 5, CategoryName = Categories.General }
            );

            //category
        }
    }
}
