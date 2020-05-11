namespace BeyondTheTutor.Models.ViewModels
{
    public class AllUsersViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string VNumber { get; set; } = "N/A";
    }
}
