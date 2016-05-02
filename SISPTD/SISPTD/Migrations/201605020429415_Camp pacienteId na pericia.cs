namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamppacienteIdnapericia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pericia", "pacienteId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pericia", "pacienteId");
        }
    }
}
