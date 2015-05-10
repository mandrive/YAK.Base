namespace TheZtack.Database.Migrations
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
                "dbo.RankingPoint",
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
                "dbo.QuestionRankingPoint",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        RankingPointId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.RankingPointId })
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.RankingPoint", t => t.RankingPointId)
                .Index(t => t.QuestionId)
                .Index(t => t.RankingPointId);
            
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
                "dbo.CommentRankingPoint",
                c => new
                    {
                        CommentId = c.Int(nullable: false),
                        RankingPointId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommentId, t.RankingPointId })
                .ForeignKey("dbo.Comment", t => t.CommentId)
                .ForeignKey("dbo.RankingPoint", t => t.RankingPointId)
                .Index(t => t.CommentId)
                .Index(t => t.RankingPointId);
            
            CreateTable(
                "dbo.AnswerRankingPoint",
                c => new
                    {
                        AnswerId = c.Int(nullable: false),
                        RankingPointId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnswerId, t.RankingPointId })
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .ForeignKey("dbo.RankingPoint", t => t.RankingPointId)
                .Index(t => t.AnswerId)
                .Index(t => t.RankingPointId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerRankingPoint", "RankingPointId", "dbo.RankingPoint");
            DropForeignKey("dbo.AnswerRankingPoint", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.Comment", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.RankingPoint", "UserId", "dbo.User");
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropForeignKey("dbo.CommentRankingPoint", "RankingPointId", "dbo.RankingPoint");
            DropForeignKey("dbo.CommentRankingPoint", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.QuestionTag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.QuestionTag", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.QuestionRankingPoint", "RankingPointId", "dbo.RankingPoint");
            DropForeignKey("dbo.QuestionRankingPoint", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Comment", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropIndex("dbo.AnswerRankingPoint", new[] { "RankingPointId" });
            DropIndex("dbo.AnswerRankingPoint", new[] { "AnswerId" });
            DropIndex("dbo.CommentRankingPoint", new[] { "RankingPointId" });
            DropIndex("dbo.CommentRankingPoint", new[] { "CommentId" });
            DropIndex("dbo.QuestionTag", new[] { "TagId" });
            DropIndex("dbo.QuestionTag", new[] { "QuestionId" });
            DropIndex("dbo.QuestionRankingPoint", new[] { "RankingPointId" });
            DropIndex("dbo.QuestionRankingPoint", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "UserId" });
            DropIndex("dbo.RankingPoint", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "AnswerId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropTable("dbo.AnswerRankingPoint");
            DropTable("dbo.CommentRankingPoint");
            DropTable("dbo.QuestionTag");
            DropTable("dbo.QuestionRankingPoint");
            DropTable("dbo.Tag");
            DropTable("dbo.Question");
            DropTable("dbo.RankingPoint");
            DropTable("dbo.Comment");
            DropTable("dbo.User");
            DropTable("dbo.Answer");
        }
    }
}
