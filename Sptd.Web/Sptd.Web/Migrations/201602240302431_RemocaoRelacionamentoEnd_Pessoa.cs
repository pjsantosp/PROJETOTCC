namespace Sptd.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemocaoRelacionamentoEnd_Pessoa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pessoa", "fk_Endereco", "dbo.Endereco");
            DropIndex("dbo.Pessoa", new[] { "fk_Endereco" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Pessoa", "fk_Endereco");
            AddForeignKey("dbo.Pessoa", "fk_Endereco", "dbo.Endereco", "enderecoId");
        }
    }
}
