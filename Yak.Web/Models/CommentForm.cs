using Yak.DTO;

namespace Yak.Web.Models
{
    public class CommentForm
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int RankPoint { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }

        public CommentForm()
        { }

        public CommentForm(Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            RankPoint = comment.RankPoint;
        }

        public Comment ToDto()
        {
            return new Comment
            {
                Id = Id,
                Content = Content,
                RankPoint = RankPoint,
                QuestionId = QuestionId,
                AnswerId = AnswerId
            };
        }
    }
}