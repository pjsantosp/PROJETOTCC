namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteradoorelacionamento : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PessoaRequisicao", name: "requisicaoId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.PessoaRequisicao", name: "pessoaId", newName: "requisicaoId");
            RenameColumn(table: "dbo.PessoaRequisicao", name: "__mig_tmp__0", newName: "pessoaId");
            RenameIndex(table: "dbo.PessoaRequisicao", name: "IX_pessoaId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.PessoaRequisicao", name: "IX_requisicaoId", newName: "IX_pessoaId");
            RenameIndex(table: "dbo.PessoaRequisicao", name: "__mig_tmp__0", newName: "IX_requisicaoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PessoaRequisicao", name: "IX_requisicaoId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.PessoaRequisicao", name: "IX_pessoaId", newName: "IX_requisicaoId");
            RenameIndex(table: "dbo.PessoaRequisicao", name: "__mig_tmp__0", newName: "IX_pessoaId");
            RenameColumn(table: "dbo.PessoaRequisicao", name: "pessoaId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.PessoaRequisicao", name: "requisicaoId", newName: "pessoaId");
            RenameColumn(table: "dbo.PessoaRequisicao", name: "__mig_tmp__0", newName: "requisicaoId");
        }
    }
}
