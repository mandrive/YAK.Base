using System;
using System.Linq;
using Yak.DTO;

namespace Yak.Web.Models
{
    /// <summary>
    /// For edit and creation purposes.
    /// </summary>
    public class QuestionForm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string Tags { get; set; }

        public QuestionForm()
        {
        }

        public QuestionForm(Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Content = question.Content;
            Tags = string.Join(",", question.Tags.Select(t => t.Name));
        }

        public Question ToDto()
        {
            return new Question
            {
                Id = Id,
                Title = Title,
                Content = Content,
                Tags = Tags.Split(',').Select(part => new Tag(part)).ToList(),
                Author = "Łukasz Wasak", //TODO 
                CreateDate = DateTime.Now,
                LastModificationDate = DateTime.Now
            };
        }
    }
}
