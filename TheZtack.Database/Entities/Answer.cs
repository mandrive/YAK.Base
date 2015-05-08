using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheZtack.Database.Entities
{
    public class Answer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }

        [ForeignKey("Author")]
        public int UserId { get; set; }
        public int RankPoint { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModificationDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User Author { get; set; }
    }
}
