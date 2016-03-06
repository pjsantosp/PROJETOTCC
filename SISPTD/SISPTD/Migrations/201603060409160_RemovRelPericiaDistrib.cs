namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovRelPericiaDistrib : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DistribProcesso", name: "periciaId", newName: "Pericia_periciaId");
            RenameIndex(table: "dbo.DistribProcesso", name: "IX_periciaId", newName: "IX_Pericia_periciaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DistribProcesso", name: "IX_Pericia_periciaId", newName: "IX_periciaId");
            RenameColumn(table: "dbo.DistribProcesso", name: "Pericia_periciaId", newName: "periciaId");
        }
    }
}
