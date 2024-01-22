namespace FamilyPlanning_API.Contracts.WebUser
{
    public class CreateUserRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; }
        public string Surname { get; set; } = null!;
        public DateTime Birthdate { get; set; }
    }
}
