namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovRelPericiaDistribProcesso : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DistribProcesso", "periciaId", "dbo.Pericia");
            DropIndex("dbo.DistribProcesso", new[] { "Pericia_periciaId" });
            DropColumn("dbo.DistribProcesso", "Pericia_periciaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DistribProcesso", "Pericia_periciaId", c => c.Long());
            CreateIndex("dbo.DistribProcesso", "Pericia_periciaId");
            AddForeignKey("dbo.DistribProcesso", "Pericia_periciaId", "dbo.Pericia", "periciaId");
        }
    }
}
