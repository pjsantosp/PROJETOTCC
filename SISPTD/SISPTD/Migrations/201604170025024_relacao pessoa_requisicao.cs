namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relacaopessoa_requisicao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisicao", "pessoaId", c => c.Long(nullable: false));
            CreateIndex("dbo.Requisicao", "pessoaId");
            AddForeignKey("dbo.Requisicao", "pessoaId", "dbo.Pessoa", "pessoaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requisicao", "pessoaId", "dbo.Pessoa");
            DropIndex("dbo.Requisicao", new[] { "pessoaId" });
            DropColumn("dbo.Requisicao", "pessoaId");
        }
    }
}
