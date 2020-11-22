namespace ProjectDAW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblCategory",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserCreatedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.TblUser", t => t.UserCreatedId, cascadeDelete: true)
                .Index(t => t.UserCreatedId);
            
            CreateTable(
                "dbo.TblUser",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblCategory", "UserCreatedId", "dbo.TblUser");
            DropIndex("dbo.TblCategory", new[] { "UserCreatedId" });
            DropTable("dbo.TblUser");
            DropTable("dbo.TblCategory");
        }
    }
}
