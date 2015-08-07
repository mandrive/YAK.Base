using Yak.DTO;

namespace Yak.Web.Models
{
    public class AnswerForm
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }

        public AnswerForm(Answer answer)
        {
            Id = answer.Id;
            QuestionId = answer.QuestionId;
            Content = answer.Content;
        }

        public AnswerForm()
        {
        }

        public Answer ToDto()
        {
            return new Answer
            {
                Id = Id,
                Content = Content,
                QuestionId = QuestionId
            };
        }
    }
}