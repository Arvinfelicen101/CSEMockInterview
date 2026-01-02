using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
 
    public class Users : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? MiddleName { get; set; }

        public ICollection<UserAnswers> UserAnswersCollection { get; } = new List<UserAnswers>();
        public ICollection<Results> ResultsCollection { get; } = new List<Results>();
    }
}
