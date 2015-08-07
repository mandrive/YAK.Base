using System.Linq;
using Yak.DTO;

namespace Yak.Web.Models
{
    public class CommentViewModel
    {
        public Comment Comment { get; set; }

        public int AnswerId { get; set; }
        public int QuestionId { get; set; }

        public CommentViewModel()
        {
        }

        public CommentViewModel(Comment comment)
        {
            Comment = comment;
        }

        public bool HasUserAlreadyVotedUp(int userId)
        {
            return Comment.Votes.Any(v => v.User.Id == userId && v.PointValue);
        }

        public bool HasUserAlreadyVotedDown(int userId)
        {
            return Comment.Votes.Any(v => v.User.Id == userId && !v.PointValue);
        }
    }
}