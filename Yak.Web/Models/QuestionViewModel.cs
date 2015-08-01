using System.Linq;
using Yak.DTO;

namespace Yak.Web.Models
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }

        public QuestionViewModel()
        {
        }

        public QuestionViewModel(Question question)
        {
            Question = question;
        }

        public bool HasUserAlreadyVotedUp(int userId)
        {
            return Question.Votes.Any(v => v.User.Id == userId && v.PointValue);
        }

        public bool HasUserAlreadyVotedDown(int userId)
        {
            return Question.Votes.Any(v => v.User.Id == userId && !v.PointValue);
        }
    }
}