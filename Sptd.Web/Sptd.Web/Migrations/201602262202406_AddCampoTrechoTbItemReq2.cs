namespace Sptd.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampoTrechoTbItemReq2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemRequisicao", "Trecho1_trechoId", c => c.Long());
            CreateIndex("dbo.ItemRequisicao", "Trecho1_trechoId");
            AddForeignKey("dbo.ItemRequisicao", "Trecho1_trechoId", "dbo.Trecho", "trechoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemRequisicao", "Trecho1_trechoId", "dbo.Trecho");
            DropIndex("dbo.ItemRequisicao", new[] { "Trecho1_trechoId" });
            DropColumn("dbo.ItemRequisicao", "Trecho1_trechoId");
        }
    }
}
