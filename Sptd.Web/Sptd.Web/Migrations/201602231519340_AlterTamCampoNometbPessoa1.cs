namespace Sptd.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTamCampoNometbPessoa1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "nome", c => c.String(nullable: false, maxLength: 160, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "nome", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
    }
}
