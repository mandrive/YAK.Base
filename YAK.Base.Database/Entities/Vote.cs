using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAK.Base.Database.Entities
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }
        public bool PointValue { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
