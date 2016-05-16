namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class excluircampdatbrequisicao : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requisicao", "pacienteId");
            

        }
        
        public override void Down()
        {
        }
    }
}
