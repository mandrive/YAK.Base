using System.Collections.Generic;
using System.Linq;
using Yak.DTO;

namespace Yak.Web.Models
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public IList<AnswerViewModel> AnswersViewModels { get; set; }
        public IList<CommentViewModel> CommentViewModels { get; set; }

        public QuestionViewModel()
        {
        }

        public QuestionViewModel(Question question)
        {
            Question = question;
            AnswersViewModels = new List<AnswerViewModel>();
            CommentViewModels = new List<CommentViewModel>();

            foreach (var answer in question.Answers)
            {
                AnswersViewModels.Add(new AnswerViewModel(answer));
            }

            foreach(var comment in question.Comments)
            {
                CommentViewModels.Add(new CommentViewModel(comment));
            }
        }

        public bool HasUserAlreadyVotedUp(int userId)
        {
            return Question.Votes.Any(v => v.User.Id == userId && v.PointValue);
        }

        public bool HasUserAlreadyVotedDown(int userId)
        {
            return Question.Votes.Any(v => v.User.Id == userId && !v.PointValue);
        }

        public bool HasUserAlreadyAnsweredQuestion(int userId)
        {
            return Question.Answers.Any(a => a.Author.Id == userId);
        }
    }
}