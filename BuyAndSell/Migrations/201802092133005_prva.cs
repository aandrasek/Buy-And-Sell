namespace BuyAndSell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prva : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ListModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ListModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Area = c.String(),
                        PhoneNum = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(),
                        DateofRel = c.DateTime(nullable: false),
                        MainImage = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
