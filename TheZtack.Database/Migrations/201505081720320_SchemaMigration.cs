namespace TheZtack.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchemaMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        UserId = c.Int(nullable: false),
                        RankPoint = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        QuestionId = c.Int(),
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
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        UserId = c.Int(nullable: false),
                        RankPoint = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        AnswerId = c.Int(),
                        QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .Index(t => t.UserId)
                .Index(t => t.AnswerId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            AddColumn("dbo.Question", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Question", "RankPoint", c => c.Int(nullable: false));
            AddColumn("dbo.Question", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Question", "LastModificationDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Question", "UserId");
            AddForeignKey("dbo.Question", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tag", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Comment", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Comment", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropIndex("dbo.Tag", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "QuestionId" });
            DropIndex("dbo.Comment", new[] { "AnswerId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropColumn("dbo.Question", "LastModificationDate");
            DropColumn("dbo.Question", "CreateDate");
            DropColumn("dbo.Question", "RankPoint");
            DropColumn("dbo.Question", "UserId");
            DropTable("dbo.Tag");
            DropTable("dbo.Comment");
            DropTable("dbo.User");
            DropTable("dbo.Answer");
        }
    }
}
