namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        User_GTAccount = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Users", t => t.User_GTAccount)
                .Index(t => t.User_GTAccount);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        GTAccount = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.GTAccount);
            
            AddColumn("dbo.Permissions", "Role_Name", c => c.String(maxLength: 128));
            CreateIndex("dbo.Permissions", "Role_Name");
            AddForeignKey("dbo.Permissions", "Role_Name", "dbo.Roles", "Name");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "User_GTAccount", "dbo.Users");
            DropForeignKey("dbo.Permissions", "Role_Name", "dbo.Roles");
            DropIndex("dbo.Roles", new[] { "User_GTAccount" });
            DropIndex("dbo.Permissions", new[] { "Role_Name" });
            DropColumn("dbo.Permissions", "Role_Name");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
