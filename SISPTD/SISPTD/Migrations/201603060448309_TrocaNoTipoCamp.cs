namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrocaNoTipoCamp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pericia", "situacao", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pericia", "situacao", c => c.Int());
        }
    }
}
