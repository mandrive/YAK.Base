namespace Yak.DTO
{
    public class Vote
    {
        public int Id { get; set; }
        public bool PointValue { get; set; }

        public User User { get; set; }

        public Vote()
        {
        }

        public Vote(Database.Entities.Vote vote)
        {
            Id = vote.Id;
            PointValue = vote.PointValue;
            User = vote.User != null ? new User(vote.User) : null;
        }
    }
}
