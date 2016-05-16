namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Retorpqera : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PessoaRequisicao", "Pessoa_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.PessoaRequisicao", "Requisicao_requisicaoId", "dbo.Requisicao");
            DropForeignKey("dbo.RelacaoPessoaRequisicao", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.RelacaoPessoaRequisicao", "requisicaoId", "dbo.Requisicao");
            DropIndex("dbo.RelacaoPessoaRequisicao", new[] { "requisicaoId" });
            DropIndex("dbo.RelacaoPessoaRequisicao", new[] { "pessoaId" });
            DropIndex("dbo.PessoaRequisicao", new[] { "Pessoa_pessoaId" });
            DropIndex("dbo.PessoaRequisicao", new[] { "Requisicao_requisicaoId" });
            AddColumn("dbo.Requisicao", "pacienteId", c => c.Long(nullable: false));
            AddColumn("dbo.Requisicao", "Pessoa_pessoaId", c => c.Long());
            AddColumn("dbo.Requisicao", "Pessoa_pessoaId1", c => c.Long());
            AddColumn("dbo.Requisicao", "Paciente_pessoaId", c => c.Long());
            AddColumn("dbo.Pessoa", "Requisicao_requisicaoId", c => c.Long());
            CreateIndex("dbo.Requisicao", "Pessoa_pessoaId");
            CreateIndex("dbo.Requisicao", "Pessoa_pessoaId1");
            CreateIndex("dbo.Requisicao", "Paciente_pessoaId");
            CreateIndex("dbo.Pessoa", "Requisicao_requisicaoId");
            AddForeignKey("dbo.Requisicao", "Pessoa_pessoaId", "dbo.Pessoa", "pessoaId");
            AddForeignKey("dbo.Requisicao", "Pessoa_pessoaId1", "dbo.Pessoa", "pessoaId");
            AddForeignKey("dbo.Pessoa", "Requisicao_requisicaoId", "dbo.Requisicao", "requisicaoId");
            AddForeignKey("dbo.Requisicao", "Paciente_pessoaId", "dbo.Pessoa", "pessoaId");
            //DropTable("dbo.RelacaoPessoaRequisicao");
            //DropTable("dbo.PessoaRequisicao");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PessoaRequisicao",
                c => new
                    {
                        Pessoa_pessoaId = c.Long(nullable: false),
                        Requisicao_requisicaoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pessoa_pessoaId, t.Requisicao_requisicaoId });
            
            CreateTable(
                "dbo.RelacaoPessoaRequisicao",
                c => new
                    {
                        pessoaRequisicaoId = c.Int(nullable: false, identity: true),
                        requisicaoId = c.Long(nullable: false),
                        pessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.pessoaRequisicaoId);
            
            DropForeignKey("dbo.Requisicao", "Paciente_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pessoa", "Requisicao_requisicaoId", "dbo.Requisicao");
            DropForeignKey("dbo.Requisicao", "Pessoa_pessoaId1", "dbo.Pessoa");
            DropForeignKey("dbo.Requisicao", "Pessoa_pessoaId", "dbo.Pessoa");
            DropIndex("dbo.Pessoa", new[] { "Requisicao_requisicaoId" });
            DropIndex("dbo.Requisicao", new[] { "Paciente_pessoaId" });
            DropIndex("dbo.Requisicao", new[] { "Pessoa_pessoaId1" });
            DropIndex("dbo.Requisicao", new[] { "Pessoa_pessoaId" });
            DropColumn("dbo.Pessoa", "Requisicao_requisicaoId");
            DropColumn("dbo.Requisicao", "Paciente_pessoaId");
            DropColumn("dbo.Requisicao", "Pessoa_pessoaId1");
            DropColumn("dbo.Requisicao", "Pessoa_pessoaId");
            DropColumn("dbo.Requisicao", "pacienteId");
            CreateIndex("dbo.PessoaRequisicao", "Requisicao_requisicaoId");
            CreateIndex("dbo.PessoaRequisicao", "Pessoa_pessoaId");
            CreateIndex("dbo.RelacaoPessoaRequisicao", "pessoaId");
            CreateIndex("dbo.RelacaoPessoaRequisicao", "requisicaoId");
            AddForeignKey("dbo.RelacaoPessoaRequisicao", "requisicaoId", "dbo.Requisicao", "requisicaoId", cascadeDelete: true);
            AddForeignKey("dbo.RelacaoPessoaRequisicao", "pessoaId", "dbo.Pessoa", "pessoaId", cascadeDelete: true);
            AddForeignKey("dbo.PessoaRequisicao", "Requisicao_requisicaoId", "dbo.Requisicao", "requisicaoId", cascadeDelete: true);
            AddForeignKey("dbo.PessoaRequisicao", "Pessoa_pessoaId", "dbo.Pessoa", "pessoaId", cascadeDelete: true);
        }
    }
}
