namespace Sptd.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampoTrechoTbItemReq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemRequisicao", "fk_Trecho1", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemRequisicao", "fk_Trecho1");
        }
    }
}
