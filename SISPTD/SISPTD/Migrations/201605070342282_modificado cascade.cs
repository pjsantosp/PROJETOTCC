namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadocascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DistribProcesso", "usuarioEnviouId", "dbo.User");
            AddForeignKey("dbo.DistribProcesso", "usuarioEnviouId", "dbo.User", "usuarioId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DistribProcesso", "usuarioEnviouId", "dbo.User");
            AddForeignKey("dbo.DistribProcesso", "usuarioEnviouId", "dbo.User", "usuarioId");
        }
    }
}
