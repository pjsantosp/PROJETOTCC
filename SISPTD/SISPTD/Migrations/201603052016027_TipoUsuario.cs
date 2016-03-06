namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "tipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "tipo");
        }
    }
}
