using System.Linq;
using Yak.DTO;

namespace Yak.Web.Models
{
    public class AnswerViewModel
    {
        public Answer Answer { get; set; }

        public int QuestionId { get; set; }

        public AnswerViewModel()
        {
            Answer = new Answer();
        }

        public AnswerViewModel(Answer answer)
        {
            Answer = answer;
        }

        public AnswerViewModel(int questionId, int userId)
        {
            QuestionId = questionId;
            Answer = new Answer();
        }
        public bool HasUserAlreadyVotedUp(int userId)
        {
            return Answer.Votes.Any(v => v.User.Id == userId && v.PointValue);
        }

        public bool HasUserAlreadyVotedDown(int userId)
        {
            return Answer.Votes.Any(v => v.User.Id == userId && !v.PointValue);
        }

    }
}