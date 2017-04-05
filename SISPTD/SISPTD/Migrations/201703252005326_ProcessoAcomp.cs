namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcessoAcomp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcompanhanteProcesso",
                c => new
                    {
                        pessoaTipoId = c.Int(nullable: false, identity: true),
                        pessoaId = c.Long(nullable: false),
                        processoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.pessoaTipoId)
                .ForeignKey("dbo.Processo", t => t.processoId, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .Index(t => t.pessoaId)
                .Index(t => t.processoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcompanhanteProcesso", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.AcompanhanteProcesso", "processoId", "dbo.Processo");
            DropIndex("dbo.AcompanhanteProcesso", new[] { "processoId" });
            DropIndex("dbo.AcompanhanteProcesso", new[] { "pessoaId" });
            DropTable("dbo.AcompanhanteProcesso");
        }
    }
}
