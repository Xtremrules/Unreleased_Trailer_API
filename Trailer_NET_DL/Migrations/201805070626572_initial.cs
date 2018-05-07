namespace Trailer_NET_DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        File_Name = c.String(),
                        URI = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Director = c.String(),
                        Writer = c.String(),
                        Producer = c.String(),
                        ImageID = c.Int(),
                        GenreID = c.Int(),
                        Release_Date = c.DateTime(nullable: false),
                        Trailer_Url = c.String(),
                        Created_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genres", t => t.GenreID)
                .ForeignKey("dbo.Images", t => t.ImageID)
                .Index(t => t.ImageID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.User_Roles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.User_Claims",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User_Logins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Movie_Image",
                c => new
                    {
                        MovieID = c.Int(nullable: false),
                        ImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieID, t.ImageID })
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Liked_Table",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieID })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieID);
            
            CreateTable(
                "dbo.Watch_Later_Table",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieID })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Watch_Later_Table", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.Watch_Later_Table", "UserId", "dbo.Users");
            DropForeignKey("dbo.User_Roles", "UserId", "dbo.Users");
            DropForeignKey("dbo.User_Logins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Liked_Table", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.Liked_Table", "UserId", "dbo.Users");
            DropForeignKey("dbo.User_Claims", "UserId", "dbo.Users");
            DropForeignKey("dbo.User_Roles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Movie_Image", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Movie_Image", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.Movies", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Movies", "GenreID", "dbo.Genres");
            DropIndex("dbo.Watch_Later_Table", new[] { "MovieID" });
            DropIndex("dbo.Watch_Later_Table", new[] { "UserId" });
            DropIndex("dbo.Liked_Table", new[] { "MovieID" });
            DropIndex("dbo.Liked_Table", new[] { "UserId" });
            DropIndex("dbo.Movie_Image", new[] { "ImageID" });
            DropIndex("dbo.Movie_Image", new[] { "MovieID" });
            DropIndex("dbo.User_Logins", new[] { "UserId" });
            DropIndex("dbo.User_Claims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.User_Roles", new[] { "RoleId" });
            DropIndex("dbo.User_Roles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Movies", new[] { "GenreID" });
            DropIndex("dbo.Movies", new[] { "ImageID" });
            DropTable("dbo.Watch_Later_Table");
            DropTable("dbo.Liked_Table");
            DropTable("dbo.Movie_Image");
            DropTable("dbo.User_Logins");
            DropTable("dbo.User_Claims");
            DropTable("dbo.Users");
            DropTable("dbo.User_Roles");
            DropTable("dbo.Roles");
            DropTable("dbo.Movies");
            DropTable("dbo.Images");
            DropTable("dbo.Genres");
        }
    }
}
