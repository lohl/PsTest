namespace CodingTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReadingGrav : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReadingGravs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Depth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GravX = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GravY = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GravZ = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReadingGravs");
        }
    }
}
