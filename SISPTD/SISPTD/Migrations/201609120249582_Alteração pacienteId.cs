namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraçãopacienteId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requisicao", "Paciente_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Requisicao", "Pessoa_pessoaId", "dbo.Pessoa");
            DropIndex("dbo.Requisicao", new[] { "Pessoa_pessoaId" });
            DropIndex("dbo.Requisicao", new[] { "Paciente_pessoaId" });
            //DropColumn("dbo.Requisicao", "pacienteId");
            DropColumn("dbo.Requisicao", "pacienteId");
            //RenameColumn(table: "dbo.Requisicao", name: "Paciente_pessoaId", newName: "pacienteId");
            RenameColumn(table: "dbo.Requisicao", name: "Pessoa_pessoaId", newName: "pacienteId");
            //AlterColumn("dbo.Requisicao", "pacienteId", c => c.Long(nullable: false));
            //AlterColumn("dbo.Requisicao", "pacienteId", c => c.Long(nullable: false));
            //CreateIndex("dbo.Requisicao", "pacienteId");
            AddForeignKey("dbo.Requisicao", "pacienteId", "dbo.Pessoa", "pessoaId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requisicao", "pacienteId", "dbo.Pessoa");
            DropIndex("dbo.Requisicao", new[] { "pacienteId" });
            AlterColumn("dbo.Requisicao", "pacienteId", c => c.Long());
            AlterColumn("dbo.Requisicao", "pacienteId", c => c.Long());
            RenameColumn(table: "dbo.Requisicao", name: "pacienteId", newName: "Pessoa_pessoaId");
            RenameColumn(table: "dbo.Requisicao", name: "pacienteId", newName: "Paciente_pessoaId");
            AddColumn("dbo.Requisicao", "PacienteId", c => c.Long(nullable: false));
            AddColumn("dbo.Requisicao", "PacienteId", c => c.Long(nullable: false));
            CreateIndex("dbo.Requisicao", "Paciente_pessoaId");
            CreateIndex("dbo.Requisicao", "Pessoa_pessoaId");
            AddForeignKey("dbo.Requisicao", "Pessoa_pessoaId", "dbo.Pessoa", "pessoaId");
            AddForeignKey("dbo.Requisicao", "Paciente_pessoaId", "dbo.Pessoa", "pessoaId");
        }
    }
}
