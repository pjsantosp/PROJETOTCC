namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agendamentocclinica : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agendamento", "clinicaId", c => c.Long());
            CreateIndex("dbo.Agendamento", "clinicaId");
            AddForeignKey("dbo.Agendamento", "clinicaId", "dbo.Clinica", "clinicaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agendamento", "clinicaId", "dbo.Clinica");
            DropIndex("dbo.Agendamento", new[] { "clinicaId" });
            DropColumn("dbo.Agendamento", "clinicaId");
        }
    }
}
