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
                        Author_Id = c.Int(),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Author_Id)
                .ForeignKey("dbo.Question", t => t.Question_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(),
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
                        Answer_Id = c.Int(),
                        Author_Id = c.Int(),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answer", t => t.Answer_Id)
                .ForeignKey("dbo.User", t => t.Author_Id)
                .ForeignKey("dbo.Question", t => t.Question_Id)
                .Index(t => t.Answer_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Question_Id);
            
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
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vote",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PointValue = c.Boolean(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TagQuestion",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Question_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Question_Id })
                .ForeignKey("dbo.Tag", t => t.Tag_Id)
                .ForeignKey("dbo.Question", t => t.Question_Id)
                .Index(t => t.Tag_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.VoteAnswer",
                c => new
                    {
                        Vote_Id = c.Int(nullable: false),
                        Answer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vote_Id, t.Answer_Id })
                .ForeignKey("dbo.Vote", t => t.Vote_Id)
                .ForeignKey("dbo.Answer", t => t.Answer_Id)
                .Index(t => t.Vote_Id)
                .Index(t => t.Answer_Id);
            
            CreateTable(
                "dbo.VoteComment",
                c => new
                    {
                        Vote_Id = c.Int(nullable: false),
                        Comment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vote_Id, t.Comment_Id })
                .ForeignKey("dbo.Vote", t => t.Vote_Id)
                .ForeignKey("dbo.Comment", t => t.Comment_Id)
                .Index(t => t.Vote_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.VoteQuestion",
                c => new
                    {
                        Vote_Id = c.Int(nullable: false),
                        Question_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vote_Id, t.Question_Id })
                .ForeignKey("dbo.Vote", t => t.Vote_Id)
                .ForeignKey("dbo.Question", t => t.Question_Id)
                .Index(t => t.Vote_Id)
                .Index(t => t.Question_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vote", "User_Id", "dbo.User");
            DropForeignKey("dbo.VoteQuestion", "Question_Id", "dbo.Question");
            DropForeignKey("dbo.VoteQuestion", "Vote_Id", "dbo.Vote");
            DropForeignKey("dbo.VoteComment", "Comment_Id", "dbo.Comment");
            DropForeignKey("dbo.VoteComment", "Vote_Id", "dbo.Vote");
            DropForeignKey("dbo.VoteAnswer", "Answer_Id", "dbo.Answer");
            DropForeignKey("dbo.VoteAnswer", "Vote_Id", "dbo.Vote");
            DropForeignKey("dbo.TagQuestion", "Question_Id", "dbo.Question");
            DropForeignKey("dbo.TagQuestion", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.Comment", "Question_Id", "dbo.Question");
            DropForeignKey("dbo.Question", "Author_Id", "dbo.User");
            DropForeignKey("dbo.Answer", "Question_Id", "dbo.Question");
            DropForeignKey("dbo.Comment", "Author_Id", "dbo.User");
            DropForeignKey("dbo.Comment", "Answer_Id", "dbo.Answer");
            DropForeignKey("dbo.Answer", "Author_Id", "dbo.User");
            DropIndex("dbo.VoteQuestion", new[] { "Question_Id" });
            DropIndex("dbo.VoteQuestion", new[] { "Vote_Id" });
            DropIndex("dbo.VoteComment", new[] { "Comment_Id" });
            DropIndex("dbo.VoteComment", new[] { "Vote_Id" });
            DropIndex("dbo.VoteAnswer", new[] { "Answer_Id" });
            DropIndex("dbo.VoteAnswer", new[] { "Vote_Id" });
            DropIndex("dbo.TagQuestion", new[] { "Question_Id" });
            DropIndex("dbo.TagQuestion", new[] { "Tag_Id" });
            DropIndex("dbo.Vote", new[] { "User_Id" });
            DropIndex("dbo.Question", new[] { "Author_Id" });
            DropIndex("dbo.Comment", new[] { "Question_Id" });
            DropIndex("dbo.Comment", new[] { "Author_Id" });
            DropIndex("dbo.Comment", new[] { "Answer_Id" });
            DropIndex("dbo.Answer", new[] { "Question_Id" });
            DropIndex("dbo.Answer", new[] { "Author_Id" });
            DropTable("dbo.VoteQuestion");
            DropTable("dbo.VoteComment");
            DropTable("dbo.VoteAnswer");
            DropTable("dbo.TagQuestion");
            DropTable("dbo.Vote");
            DropTable("dbo.Tag");
            DropTable("dbo.Question");
            DropTable("dbo.Comment");
            DropTable("dbo.User");
            DropTable("dbo.Answer");
        }
    }
}
