namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Altrelacaopessoarequisica : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requisicao", "pacienteId");
            RenameColumn(table: "dbo.Requisicao", name: "Paciente_pessoaId", newName: "pacienteId");
            RenameIndex(table: "dbo.Requisicao", name: "IX_Paciente_pessoaId", newName: "IX_pacienteId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Requisicao", name: "IX_pacienteId", newName: "IX_Paciente_pessoaId");
            RenameColumn(table: "dbo.Requisicao", name: "pacienteId", newName: "Paciente_pessoaId");
            AddColumn("dbo.Requisicao", "pacienteId", c => c.Long());
        }
    }
}
