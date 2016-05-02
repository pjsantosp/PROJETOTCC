namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamppacienteIdnapericiaremovido : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pericia", "pacienteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pericia", "pacienteId", c => c.Long(nullable: false));
        }
    }
}
