namespace Sptd.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelacionamentoPessoaEnd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Endereco", "fk_Pessoa", c => c.Long());
            AddColumn("dbo.Endereco", "Pessoa_pessoaId", c => c.Long());
            CreateIndex("dbo.Endereco", "Pessoa_pessoaId");
            AddForeignKey("dbo.Endereco", "Pessoa_pessoaId", "dbo.Pessoa", "pessoaId");
            //DropColumn("dbo.Pessoa", "fk_Endereco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoa", "fk_Endereco", c => c.Long());
            DropForeignKey("dbo.Endereco", "Pessoa_pessoaId", "dbo.Pessoa");
            DropIndex("dbo.Endereco", new[] { "Pessoa_pessoaId" });
            DropColumn("dbo.Endereco", "Pessoa_pessoaId");
            DropColumn("dbo.Endereco", "fk_Pessoa");
        }
    }
}
