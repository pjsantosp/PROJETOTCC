namespace Sptd.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primeiro : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "dt_Emissao", c => c.DateTime());
            AlterColumn("dbo.Pessoa", "orgaoemissor", c => c.String(maxLength: 7, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "orgaoemissor", c => c.String(maxLength: 7));
            DropColumn("dbo.Pessoa", "dt_Emissao");
        }
    }
}
