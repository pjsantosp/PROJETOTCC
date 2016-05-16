namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class excluirtabpessoarequisicao : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Requisicao", "Paciente_pessoaId", "dbo.Pessoa");
            //DropIndex("dbo.Requisicao", new[] { "Paciente_pessoaId" });
            //RenameColumn(table: "dbo.PessoaRequisicao", name: "requisicaoId", newName: "Requisicao_requisicaoId");
            //RenameColumn(table: "dbo.PessoaRequisicao", name: "pessoaId", newName: "Pessoa_pessoaId");
            //RenameIndex(table: "dbo.PessoaRequisicao", name: "IX_pessoaId", newName: "IX_Pessoa_pessoaId");
            //RenameIndex(table: "dbo.PessoaRequisicao", name: "IX_requisicaoId", newName: "IX_Requisicao_requisicaoId");
            //DropPrimaryKey("dbo.PessoaRequisicao");
            //AddPrimaryKey("dbo.PessoaRequisicao", new[] { "Pessoa_pessoaId", "Requisicao_requisicaoId" });
            //DropColumn("dbo.Requisicao", "pacienteId");
            //DropColumn("dbo.Requisicao", "Paciente_pessoaId");
            DropTable("dbo.PessoaRequisicao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requisicao", "Paciente_pessoaId", c => c.Long());
            AddColumn("dbo.Requisicao", "pacienteId", c => c.Long());
            DropPrimaryKey("dbo.PessoaRequisicao");
            AddPrimaryKey("dbo.PessoaRequisicao", new[] { "requisicaoId", "pessoaId" });
            RenameIndex(table: "dbo.PessoaRequisicao", name: "IX_Requisicao_requisicaoId", newName: "IX_requisicaoId");
            RenameIndex(table: "dbo.PessoaRequisicao", name: "IX_Pessoa_pessoaId", newName: "IX_pessoaId");
            RenameColumn(table: "dbo.PessoaRequisicao", name: "Pessoa_pessoaId", newName: "pessoaId");
            RenameColumn(table: "dbo.PessoaRequisicao", name: "Requisicao_requisicaoId", newName: "requisicaoId");
            CreateIndex("dbo.Requisicao", "Paciente_pessoaId");
            AddForeignKey("dbo.Requisicao", "Paciente_pessoaId", "dbo.Pessoa", "pessoaId");
        }
    }
}
