using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
 
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }

        public ICollection<UserAnswers> UserAnswersCollection { get; } = new List<UserAnswers>();
        public ICollection<Results> ResultsCollection { get; } = new List<Results>();
    }
}
