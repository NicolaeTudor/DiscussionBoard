namespace ProjectDAW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostsToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TblPost", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.TblCategory", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TblCategory", "Name", c => c.String());
            DropColumn("dbo.TblPost", "Title");
        }
    }
}
