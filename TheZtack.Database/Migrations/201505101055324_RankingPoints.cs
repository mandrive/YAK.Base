namespace TheZtack.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RankingPoints : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionTag", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.QuestionTag", "TagId", "dbo.Tag");
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
            
            AddForeignKey("dbo.QuestionTag", "QuestionId", "dbo.Question", "Id");
            AddForeignKey("dbo.QuestionTag", "TagId", "dbo.Tag", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionTag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.QuestionTag", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.AnswerRankingPoint", "RankingPointId", "dbo.RankingPoint");
            DropForeignKey("dbo.AnswerRankingPoint", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.RankingPoint", "UserId", "dbo.User");
            DropForeignKey("dbo.CommentRankingPoint", "RankingPointId", "dbo.RankingPoint");
            DropForeignKey("dbo.CommentRankingPoint", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.QuestionRankingPoint", "RankingPointId", "dbo.RankingPoint");
            DropForeignKey("dbo.QuestionRankingPoint", "QuestionId", "dbo.Question");
            DropIndex("dbo.AnswerRankingPoint", new[] { "RankingPointId" });
            DropIndex("dbo.AnswerRankingPoint", new[] { "AnswerId" });
            DropIndex("dbo.CommentRankingPoint", new[] { "RankingPointId" });
            DropIndex("dbo.CommentRankingPoint", new[] { "CommentId" });
            DropIndex("dbo.QuestionRankingPoint", new[] { "RankingPointId" });
            DropIndex("dbo.QuestionRankingPoint", new[] { "QuestionId" });
            DropIndex("dbo.RankingPoint", new[] { "UserId" });
            DropTable("dbo.AnswerRankingPoint");
            DropTable("dbo.CommentRankingPoint");
            DropTable("dbo.QuestionRankingPoint");
            DropTable("dbo.RankingPoint");
            AddForeignKey("dbo.QuestionTag", "TagId", "dbo.Tag", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuestionTag", "QuestionId", "dbo.Question", "Id", cascadeDelete: true);
        }
    }
}
