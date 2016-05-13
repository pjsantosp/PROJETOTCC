namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alterpericiapsolicitacao : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Pericia", newName: "SolicitacaoPericia");
            DropForeignKey("dbo.medicoPericia", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.medicoPericia", "periciaId", "dbo.Pericia");
            DropIndex("dbo.medicoPericia", new[] { "pessoaId" });
            DropIndex("dbo.medicoPericia", new[] { "periciaId" });
            AddColumn("dbo.SolicitacaoPericia", "medicoPessoaId", c => c.Long());
            AlterColumn("dbo.SolicitacaoPericia", "Situacao", c => c.Int(nullable: false));
            CreateIndex("dbo.SolicitacaoPericia", "medicoPessoaId");
            AddForeignKey("dbo.SolicitacaoPericia", "medicoPessoaId", "dbo.Pessoa", "pessoaId");
            DropTable("dbo.medicoPericia");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.medicoPericia",
                c => new
                    {
                        pessoaId = c.Long(nullable: false),
                        periciaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.pessoaId, t.periciaId });
            
            DropForeignKey("dbo.SolicitacaoPericia", "medicoPessoaId", "dbo.Pessoa");
            DropIndex("dbo.SolicitacaoPericia", new[] { "medicoPessoaId" });
            AlterColumn("dbo.SolicitacaoPericia", "Situacao", c => c.String());
            DropColumn("dbo.SolicitacaoPericia", "medicoPessoaId");
            CreateIndex("dbo.medicoPericia", "periciaId");
            CreateIndex("dbo.medicoPericia", "pessoaId");
            AddForeignKey("dbo.medicoPericia", "periciaId", "dbo.Pericia", "periciaId", cascadeDelete: true);
            AddForeignKey("dbo.medicoPericia", "pessoaId", "dbo.Pessoa", "pessoaId", cascadeDelete: true);
            RenameTable(name: "dbo.SolicitacaoPericia", newName: "Pericia");
        }
    }
}
