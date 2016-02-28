namespace Sptd.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Endereco", name: "pessoaId", newName: "Pessoa_pessoaId");
            RenameIndex(table: "dbo.Endereco", name: "IX_pessoaId", newName: "IX_Pessoa_pessoaId");
            AddColumn("dbo.Endereco", "fk_PessoaId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Endereco", "fk_PessoaId");
            RenameIndex(table: "dbo.Endereco", name: "IX_Pessoa_pessoaId", newName: "IX_pessoaId");
            RenameColumn(table: "dbo.Endereco", name: "Pessoa_pessoaId", newName: "pessoaId");
        }
    }
}
