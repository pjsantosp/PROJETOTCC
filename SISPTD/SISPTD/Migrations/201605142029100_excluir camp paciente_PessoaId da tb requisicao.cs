namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class excluircamppaciente_PessoaIddatbrequisicao : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Requisicao", new[] { "Paciente_pessoaId" });
            DropForeignKey("dbo.Requisicao", "Paciente_pessoaId", "dbo.Pessoa");
            DropColumn("dbo.Requisicao", "Paciente_pessoaId");
        }
        
        public override void Down()
        {
        }
    }
}
