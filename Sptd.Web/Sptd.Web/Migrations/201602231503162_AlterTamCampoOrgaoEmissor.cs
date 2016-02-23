namespace Sptd.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTamCampoOrgaoEmissor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "orgaoemissor", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "orgaoemissor", c => c.String(maxLength: 7, unicode: false));
        }
    }
}
