namespace Yak.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        RankPoint = c.Int(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        RankPoint = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        QuestionId = c.Int(),
                        UserId = c.Int(nullable: false),
                        AnswerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId)
                .Index(t => t.AnswerId);
            
            CreateTable(
                "dbo.Vote",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PointValue = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        RankPoint = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionTag",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.TagId })
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.Tag", t => t.TagId)
                .Index(t => t.QuestionId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.QuestionVote",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        VoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.VoteId })
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.Vote", t => t.VoteId)
                .Index(t => t.QuestionId)
                .Index(t => t.VoteId);
            
            CreateTable(
                "dbo.CommentVote",
                c => new
                    {
                        CommentId = c.Int(nullable: false),
                        VoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommentId, t.VoteId })
                .ForeignKey("dbo.Comment", t => t.CommentId)
                .ForeignKey("dbo.Vote", t => t.VoteId)
                .Index(t => t.CommentId)
                .Index(t => t.VoteId);
            
            CreateTable(
                "dbo.AnswerVote",
                c => new
                    {
                        AnswerId = c.Int(nullable: false),
                        VoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnswerId, t.VoteId })
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .ForeignKey("dbo.Vote", t => t.VoteId)
                .Index(t => t.AnswerId)
                .Index(t => t.VoteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerVote", "VoteId", "dbo.Vote");
            DropForeignKey("dbo.AnswerVote", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.Comment", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.Vote", "UserId", "dbo.User");
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropForeignKey("dbo.CommentVote", "VoteId", "dbo.Vote");
            DropForeignKey("dbo.CommentVote", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.QuestionVote", "VoteId", "dbo.Vote");
            DropForeignKey("dbo.QuestionVote", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.QuestionTag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.QuestionTag", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Comment", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropIndex("dbo.AnswerVote", new[] { "VoteId" });
            DropIndex("dbo.AnswerVote", new[] { "AnswerId" });
            DropIndex("dbo.CommentVote", new[] { "VoteId" });
            DropIndex("dbo.CommentVote", new[] { "CommentId" });
            DropIndex("dbo.QuestionVote", new[] { "VoteId" });
            DropIndex("dbo.QuestionVote", new[] { "QuestionId" });
            DropIndex("dbo.QuestionTag", new[] { "TagId" });
            DropIndex("dbo.QuestionTag", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "UserId" });
            DropIndex("dbo.Vote", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "AnswerId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropTable("dbo.AnswerVote");
            DropTable("dbo.CommentVote");
            DropTable("dbo.QuestionVote");
            DropTable("dbo.QuestionTag");
            DropTable("dbo.Tag");
            DropTable("dbo.Question");
            DropTable("dbo.Vote");
            DropTable("dbo.Comment");
            DropTable("dbo.User");
            DropTable("dbo.Answer");
        }
    }
}
