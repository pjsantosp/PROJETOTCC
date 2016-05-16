namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlttamcampteltbClinica : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clinica", "tel_Clinica", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clinica", "tel_Clinica", c => c.String(maxLength: 10));
        }
    }
}
