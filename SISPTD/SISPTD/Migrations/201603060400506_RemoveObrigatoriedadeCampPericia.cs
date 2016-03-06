namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveObrigatoriedadeCampPericia : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DistribProcesso", "periciaId", "dbo.Pericia");
            DropIndex("dbo.DistribProcesso", new[] { "periciaId" });
            AlterColumn("dbo.DistribProcesso", "periciaId", c => c.Long());
            CreateIndex("dbo.DistribProcesso", "periciaId");
            AddForeignKey("dbo.DistribProcesso", "periciaId", "dbo.Pericia", "periciaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DistribProcesso", "periciaId", "dbo.Pericia");
            DropIndex("dbo.DistribProcesso", new[] { "periciaId" });
            AlterColumn("dbo.DistribProcesso", "periciaId", c => c.Long(nullable: false));
            CreateIndex("dbo.DistribProcesso", "periciaId");
            AddForeignKey("dbo.DistribProcesso", "periciaId", "dbo.Pericia", "periciaId", cascadeDelete: true);
        }
    }
}
