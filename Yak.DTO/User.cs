namespace Yak.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public User() { }

        public User(Database.Entities.User user)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
        }
    }
}
