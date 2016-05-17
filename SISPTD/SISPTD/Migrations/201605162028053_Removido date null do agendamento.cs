namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removidodatenulldoagendamento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Agendamento", "dt_Agendamento", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Agendamento", "dt_Marcacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Agendamento", "dt_Marcacao", c => c.DateTime());
            AlterColumn("dbo.Agendamento", "dt_Agendamento", c => c.DateTime());
        }
    }
}
