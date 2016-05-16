namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alteracaoctabdomeiocomtipo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RelacaoPessoaRequisicao",
                c => new
                    {
                        pessoaRequisicaoId = c.Int(nullable: false, identity: true),
                        requisicaoId = c.Long(nullable: false),
                        pessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.pessoaRequisicaoId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .ForeignKey("dbo.Requisicao", t => t.requisicaoId, cascadeDelete: true)
                .Index(t => t.requisicaoId)
                .Index(t => t.pessoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RelacaoPessoaRequisicao", "requisicaoId", "dbo.Requisicao");
            DropForeignKey("dbo.RelacaoPessoaRequisicao", "pessoaId", "dbo.Pessoa");
            DropIndex("dbo.RelacaoPessoaRequisicao", new[] { "pessoaId" });
            DropIndex("dbo.RelacaoPessoaRequisicao", new[] { "requisicaoId" });
            DropTable("dbo.RelacaoPessoaRequisicao");
        }
    }
}
