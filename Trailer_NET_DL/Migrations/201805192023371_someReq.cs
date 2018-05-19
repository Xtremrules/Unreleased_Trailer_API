namespace Trailer_NET_DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Release_Date", c => c.DateTime());
            AlterColumn("dbo.Movies", "Trailer_Url", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Trailer_Url", c => c.String());
            AlterColumn("dbo.Movies", "Release_Date", c => c.DateTime(nullable: false));
        }
    }
}
