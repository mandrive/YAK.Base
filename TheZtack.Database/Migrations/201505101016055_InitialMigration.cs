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
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(),
                        AnswerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.AnswerId);
            
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
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropForeignKey("dbo.QuestionTag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.QuestionTag", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Comment", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropIndex("dbo.QuestionTag", new[] { "TagId" });
            DropIndex("dbo.QuestionTag", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "AnswerId" });
            DropIndex("dbo.Comment", new[] { "QuestionId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropTable("dbo.QuestionTag");
            DropTable("dbo.Tag");
            DropTable("dbo.Question");
            DropTable("dbo.Comment");
            DropTable("dbo.User");
            DropTable("dbo.Answer");
        }
    }
}
