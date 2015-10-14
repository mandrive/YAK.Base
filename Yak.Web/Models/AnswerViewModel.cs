using System.Collections.Generic;
using System.Linq;
using Yak.DTO;

namespace Yak.Web.Models
{
    public class AnswerViewModel
    {
        public Answer Answer { get; set; }
        public IList<CommentViewModel> CommentViewModels { get; set; }
        public int QuestionId { get; set; }

        public AnswerViewModel()
        {
            Answer = new Answer();
        }

        public AnswerViewModel(Answer answer)
        {
            Answer = answer;
            CommentViewModels = new List<CommentViewModel>();

            foreach (var comment in answer.Comments)
            {
                CommentViewModels.Add(new CommentViewModel(comment));
            }
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