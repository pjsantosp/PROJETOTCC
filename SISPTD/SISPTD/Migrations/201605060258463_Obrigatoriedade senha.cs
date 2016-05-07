namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Obrigatoriedadesenha : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "senha", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "senha", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
