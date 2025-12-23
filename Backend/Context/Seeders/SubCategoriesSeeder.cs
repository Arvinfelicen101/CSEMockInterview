using Backend.Models;
using Microsoft.EntityFrameworkCore;
 

namespace CSEMockInterview.Context.Seeders
{
    public static class SubCategoriesSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategories>().HasData(
                // Verbal Ability (CategoryId = 1)
                new SubCategories { Id = 1, SubCategoryName = "Nouns", CategoryId = 1 },
                new SubCategories { Id = 2, SubCategoryName = "Gender", CategoryId = 1 },
                new SubCategories { Id = 3, SubCategoryName = "Grammatical Number", CategoryId = 1 },
                new SubCategories { Id = 4, SubCategoryName = "Verbs", CategoryId = 1 },
                new SubCategories { Id = 5, SubCategoryName = "Tenses", CategoryId = 1 },
                new SubCategories { Id = 6, SubCategoryName = "Pronouns", CategoryId = 1 },
                new SubCategories { Id = 7, SubCategoryName = "Adjectives", CategoryId = 1 },
                new SubCategories { Id = 8, SubCategoryName = "Adverbs", CategoryId = 1 },
                new SubCategories { Id = 9, SubCategoryName = "Prepositions", CategoryId = 1 },
                new SubCategories { Id = 10, SubCategoryName = "Conjunctions", CategoryId = 1 },
                new SubCategories { Id = 11, SubCategoryName = "Interjections", CategoryId = 1 },
                new SubCategories { Id = 12, SubCategoryName = "Articles", CategoryId = 1 },
                new SubCategories { Id = 13, SubCategoryName = "Subject-Verb Agreement", CategoryId = 1 },
                new SubCategories { Id = 14, SubCategoryName = "Sentence Construction", CategoryId = 1 },
                new SubCategories { Id = 15, SubCategoryName = "Sentence Structure", CategoryId = 1 },
                new SubCategories { Id = 16, SubCategoryName = "Affixes", CategoryId = 1 },
                new SubCategories { Id = 17, SubCategoryName = "Punctuations", CategoryId = 1 },
                new SubCategories { Id = 18, SubCategoryName = "Correct Usage", CategoryId = 1 },
                new SubCategories { Id = 19, SubCategoryName = "Error Identification", CategoryId = 1 },
                new SubCategories { Id = 20, SubCategoryName = "Synonyms", CategoryId = 1 },
                new SubCategories { Id = 21, SubCategoryName = "Antonyms", CategoryId = 1 },
                new SubCategories { Id = 22, SubCategoryName = "Analogy", CategoryId = 1 },
                new SubCategories { Id = 23, SubCategoryName = "Paragraph Organization", CategoryId = 1 },
                new SubCategories { Id = 24, SubCategoryName = "Reading Comprehension", CategoryId = 1 },

                // Analytical Ability (CategoryId = 2)
                new SubCategories { Id = 25, SubCategoryName = "Logical Reasoning", CategoryId = 2 },
                new SubCategories { Id = 26, SubCategoryName = "Flowchart", CategoryId = 2 },
                new SubCategories { Id = 27, SubCategoryName = "Problem Solving", CategoryId = 2 },
                new SubCategories { Id = 28, SubCategoryName = "Special Topic", CategoryId = 2 },
                new SubCategories { Id = 29, SubCategoryName = "Cognitive Reasoning", CategoryId = 2 },

                // Clerical Ability (CategoryId = 3)
                new SubCategories { Id = 30, SubCategoryName = "Vocabulary and Spelling", CategoryId = 3 },
                new SubCategories { Id = 31, SubCategoryName = "Alphabetizing", CategoryId = 3 },

                // Numerical Ability (CategoryId = 4)
                new SubCategories { Id = 32, SubCategoryName = "Divisibility Rules", CategoryId = 4 },
                new SubCategories { Id = 33, SubCategoryName = "Multiples and Factors", CategoryId = 4 },
                new SubCategories { Id = 34, SubCategoryName = "Integers", CategoryId = 4 },
                new SubCategories { Id = 35, SubCategoryName = "PEMDAS", CategoryId = 4 },
                new SubCategories { Id = 36, SubCategoryName = "Decimals", CategoryId = 4 },
                new SubCategories { Id = 37, SubCategoryName = "Fractions", CategoryId = 4 },
                new SubCategories { Id = 38, SubCategoryName = "Operations on Fractions", CategoryId = 4 },
                new SubCategories { Id = 39, SubCategoryName = "Percent", CategoryId = 4 },
                new SubCategories { Id = 40, SubCategoryName = "Ratio and Proportion", CategoryId = 4 },
                new SubCategories { Id = 41, SubCategoryName = "Averages", CategoryId = 4 },
                new SubCategories { Id = 42, SubCategoryName = "Tables, Charts, and Graphs", CategoryId = 4 },
                new SubCategories { Id = 43, SubCategoryName = "Linear Equations", CategoryId = 4 },
                new SubCategories { Id = 44, SubCategoryName = "Solving Mathematical Expressions", CategoryId = 4 },
                new SubCategories { Id = 45, SubCategoryName = "Writing Mathematical Expressions", CategoryId = 4 },
                new SubCategories { Id = 46, SubCategoryName = "Word Problem Techniques", CategoryId = 4 },
                new SubCategories { Id = 47, SubCategoryName = "Number Problems", CategoryId = 4 },
                new SubCategories { Id = 48, SubCategoryName = "Odd, Even, and Consecutive Integers", CategoryId = 4 },
                new SubCategories { Id = 49, SubCategoryName = "Age Problems", CategoryId = 4 },
                new SubCategories { Id = 50, SubCategoryName = "Work Problems", CategoryId = 4 },
                new SubCategories { Id = 51, SubCategoryName = "Geometry Problems", CategoryId = 4 },

                // General Information (CategoryId = 5)
                new SubCategories { Id = 52, SubCategoryName = "The 1987 Constitution", CategoryId = 5 },
                new SubCategories { Id = 53, SubCategoryName = "Republic Act No. 6713", CategoryId = 5 },
                new SubCategories { Id = 54, SubCategoryName = "Peace and Human Rights Issues and Concepts", CategoryId = 5 },
                new SubCategories { Id = 55, SubCategoryName = "Environmental Management and Protection", CategoryId = 5 }
            );
        }
    }
}
