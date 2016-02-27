namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agendamento",
                c => new
                    {
                        agendamentoId = c.Long(nullable: false, identity: true),
                        dt_Agendamento = c.DateTime(),
                        dt_Marcacao = c.DateTime(),
                        pessoaId = c.Long(),
                        usuarioId = c.Long(),
                    })
                .PrimaryKey(t => t.agendamentoId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
                .ForeignKey("dbo.User", t => t.usuarioId)
                .Index(t => t.pessoaId)
                .Index(t => t.usuarioId);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        pessoaId = c.Long(nullable: false, identity: true),
                        pessoaPai = c.Long(),
                        dt_Cadastro = c.DateTime(),
                        cpf = c.String(nullable: false, maxLength: 25),
                        cns = c.String(maxLength: 25),
                        rg = c.String(maxLength: 25),
                        orgaoemissor = c.String(maxLength: 10),
                        dt_Emissao = c.DateTime(),
                        nome = c.String(nullable: false, maxLength: 160),
                        dt_Nascimento = c.DateTime(nullable: false),
                        email = c.String(maxLength: 50),
                        nome_Mae = c.String(nullable: false, maxLength: 150),
                        nome_Pai = c.String(nullable: false, maxLength: 150),
                        tel = c.String(nullable: false, maxLength: 20),
                        cel = c.String(maxLength: 20),
                        Pessoa2_pessoaId = c.Long(),
                    })
                .PrimaryKey(t => t.pessoaId)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa2_pessoaId)
                .Index(t => t.Pessoa2_pessoaId);
            
            CreateTable(
                "dbo.DistribProcesso",
                c => new
                    {
                        distrib_ProcessoId = c.Long(nullable: false, identity: true),
                        origem = c.Long(),
                        destino = c.Long(nullable: false),
                        observacoes = c.String(),
                        fk_PessoaId = c.Long(nullable: false),
                        usuarioEnviou = c.Long(nullable: false),
                        fk_Pericia = c.Long(),
                        usuarioRecebeu = c.Long(),
                        Pericia_periciaId = c.Long(),
                        Pessoa_pessoaId = c.Long(),
                        Setor_setorId = c.Long(),
                        Setor_setorId1 = c.Long(),
                        Setor_setorId2 = c.Long(),
                        Setor1_setorId = c.Long(),
                        User_usuarioId = c.Long(),
                        User_usuarioId1 = c.Long(),
                        User_usuarioId2 = c.Long(),
                        User1_usuarioId = c.Long(),
                    })
                .PrimaryKey(t => t.distrib_ProcessoId)
                .ForeignKey("dbo.Pericia", t => t.Pericia_periciaId)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_pessoaId)
                .ForeignKey("dbo.Setor", t => t.Setor_setorId)
                .ForeignKey("dbo.Setor", t => t.Setor_setorId1)
                .ForeignKey("dbo.Setor", t => t.Setor_setorId2)
                .ForeignKey("dbo.Setor", t => t.Setor1_setorId)
                .ForeignKey("dbo.User", t => t.User_usuarioId)
                .ForeignKey("dbo.User", t => t.User_usuarioId1)
                .ForeignKey("dbo.User", t => t.User_usuarioId2)
                .ForeignKey("dbo.User", t => t.User1_usuarioId)
                .Index(t => t.Pericia_periciaId)
                .Index(t => t.Pessoa_pessoaId)
                .Index(t => t.Setor_setorId)
                .Index(t => t.Setor_setorId1)
                .Index(t => t.Setor_setorId2)
                .Index(t => t.Setor1_setorId)
                .Index(t => t.User_usuarioId)
                .Index(t => t.User_usuarioId1)
                .Index(t => t.User_usuarioId2)
                .Index(t => t.User1_usuarioId);
            
            CreateTable(
                "dbo.Pericia",
                c => new
                    {
                        periciaId = c.Long(nullable: false, identity: true),
                        descricao = c.String(nullable: false),
                        cidId = c.Long(),
                        dt_Pericia = c.DateTime(nullable: false),
                        medicoId = c.Long(nullable: false),
                        situacao = c.Int(),
                        pessoaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.periciaId)
                .ForeignKey("dbo.Cid", t => t.cidId)
                .ForeignKey("dbo.Medico", t => t.medicoId, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .Index(t => t.cidId)
                .Index(t => t.medicoId)
                .Index(t => t.pessoaId);
            
            CreateTable(
                "dbo.Cid",
                c => new
                    {
                        cidId = c.Long(nullable: false, identity: true),
                        codigoCid = c.String(maxLength: 5),
                        descricao = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.cidId);
            
            CreateTable(
                "dbo.Medico",
                c => new
                    {
                        medicoId = c.Long(nullable: false, identity: true),
                        especialidadeId = c.Long(),
                        pessoaId = c.Long(nullable: false),
                        crm_Medico = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.medicoId)
                .ForeignKey("dbo.Especialidade", t => t.especialidadeId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: false)
                .Index(t => t.especialidadeId)
                .Index(t => t.pessoaId);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        EspecialidadeId = c.Long(nullable: false, identity: true),
                        descricao = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.EspecialidadeId);
            
            CreateTable(
                "dbo.Setor",
                c => new
                    {
                        setorId = c.Long(nullable: false, identity: true),
                        descricao = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.setorId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        usuarioId = c.Long(nullable: false, identity: true),
                        login = c.String(nullable: false, maxLength: 50),
                        senha = c.String(nullable: false, maxLength: 50),
                        fk_PessoaId = c.Long(nullable: false),
                        Pessoa_pessoaId = c.Long(),
                    })
                .PrimaryKey(t => t.usuarioId)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_pessoaId)
                .Index(t => t.Pessoa_pessoaId);
            
            CreateTable(
                "dbo.Requisicao",
                c => new
                    {
                        requisicaoId = c.Long(nullable: false, identity: true),
                        usuarioId = c.Long(),
                        observacoes = c.Binary(),
                    })
                .PrimaryKey(t => t.requisicaoId)
                .ForeignKey("dbo.User", t => t.usuarioId)
                .Index(t => t.usuarioId);
            
            CreateTable(
                "dbo.ItemRequisicao",
                c => new
                    {
                        itemId = c.Long(nullable: false, identity: true),
                        pessoaId = c.Long(nullable: false),
                        requisicaoId = c.Long(),
                    })
                .PrimaryKey(t => t.itemId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .ForeignKey("dbo.Requisicao", t => t.requisicaoId)
                .Index(t => t.pessoaId)
                .Index(t => t.requisicaoId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        enderecoId = c.Long(nullable: false, identity: true),
                        fk_CidadeId = c.Int(nullable: false),
                        fk_PessoaId = c.Long(),
                        rua = c.String(nullable: false, maxLength: 150),
                        numero = c.String(nullable: false, maxLength: 5),
                        cep = c.String(maxLength: 8),
                        bairro = c.String(maxLength: 10),
                        Cidades_IdCidade = c.Int(),
                        Pessoa_pessoaId = c.Long(),
                    })
                .PrimaryKey(t => t.enderecoId)
                .ForeignKey("dbo.Cidades", t => t.Cidades_IdCidade)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_pessoaId)
                .Index(t => t.Cidades_IdCidade)
                .Index(t => t.Pessoa_pessoaId);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        IdCidade = c.Int(nullable: false, identity: true),
                        Cidade = c.String(nullable: false, maxLength: 100),
                        IdEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCidade)
                .ForeignKey("dbo.Estado", t => t.IdEstado, cascadeDelete: true)
                .Index(t => t.IdEstado);
            
            CreateTable(
                "dbo.Clinica",
                c => new
                    {
                        clinicaId = c.Long(nullable: false, identity: true),
                        nome_Clinica = c.String(maxLength: 150),
                        fk_Cidade = c.Int(),
                        tel_Clinica = c.String(maxLength: 10),
                        Cidades_IdCidade = c.Int(),
                    })
                .PrimaryKey(t => t.clinicaId)
                .ForeignKey("dbo.Cidades", t => t.Cidades_IdCidade)
                .Index(t => t.Cidades_IdCidade);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        IdEstado = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Sigla = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.IdEstado);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoa", "Pessoa2_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Endereco", "Pessoa_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Cidades", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "Cidades_IdCidade", "dbo.Cidades");
            DropForeignKey("dbo.Clinica", "Cidades_IdCidade", "dbo.Cidades");
            DropForeignKey("dbo.DistribProcesso", "User1_usuarioId", "dbo.User");
            DropForeignKey("dbo.DistribProcesso", "User_usuarioId2", "dbo.User");
            DropForeignKey("dbo.Requisicao", "usuarioId", "dbo.User");
            DropForeignKey("dbo.ItemRequisicao", "requisicaoId", "dbo.Requisicao");
            DropForeignKey("dbo.ItemRequisicao", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.User", "Pessoa_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.DistribProcesso", "User_usuarioId1", "dbo.User");
            DropForeignKey("dbo.DistribProcesso", "User_usuarioId", "dbo.User");
            DropForeignKey("dbo.Agendamento", "usuarioId", "dbo.User");
            DropForeignKey("dbo.DistribProcesso", "Setor1_setorId", "dbo.Setor");
            DropForeignKey("dbo.DistribProcesso", "Setor_setorId2", "dbo.Setor");
            DropForeignKey("dbo.DistribProcesso", "Setor_setorId1", "dbo.Setor");
            DropForeignKey("dbo.DistribProcesso", "Setor_setorId", "dbo.Setor");
            DropForeignKey("dbo.DistribProcesso", "Pessoa_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pericia", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Medico", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pericia", "medicoId", "dbo.Medico");
            DropForeignKey("dbo.Medico", "especialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.DistribProcesso", "Pericia_periciaId", "dbo.Pericia");
            DropForeignKey("dbo.Pericia", "cidId", "dbo.Cid");
            DropForeignKey("dbo.Agendamento", "pessoaId", "dbo.Pessoa");
            DropIndex("dbo.Clinica", new[] { "Cidades_IdCidade" });
            DropIndex("dbo.Cidades", new[] { "IdEstado" });
            DropIndex("dbo.Endereco", new[] { "Pessoa_pessoaId" });
            DropIndex("dbo.Endereco", new[] { "Cidades_IdCidade" });
            DropIndex("dbo.ItemRequisicao", new[] { "requisicaoId" });
            DropIndex("dbo.ItemRequisicao", new[] { "pessoaId" });
            DropIndex("dbo.Requisicao", new[] { "usuarioId" });
            DropIndex("dbo.User", new[] { "Pessoa_pessoaId" });
            DropIndex("dbo.Medico", new[] { "pessoaId" });
            DropIndex("dbo.Medico", new[] { "especialidadeId" });
            DropIndex("dbo.Pericia", new[] { "pessoaId" });
            DropIndex("dbo.Pericia", new[] { "medicoId" });
            DropIndex("dbo.Pericia", new[] { "cidId" });
            DropIndex("dbo.DistribProcesso", new[] { "User1_usuarioId" });
            DropIndex("dbo.DistribProcesso", new[] { "User_usuarioId2" });
            DropIndex("dbo.DistribProcesso", new[] { "User_usuarioId1" });
            DropIndex("dbo.DistribProcesso", new[] { "User_usuarioId" });
            DropIndex("dbo.DistribProcesso", new[] { "Setor1_setorId" });
            DropIndex("dbo.DistribProcesso", new[] { "Setor_setorId2" });
            DropIndex("dbo.DistribProcesso", new[] { "Setor_setorId1" });
            DropIndex("dbo.DistribProcesso", new[] { "Setor_setorId" });
            DropIndex("dbo.DistribProcesso", new[] { "Pessoa_pessoaId" });
            DropIndex("dbo.DistribProcesso", new[] { "Pericia_periciaId" });
            DropIndex("dbo.Pessoa", new[] { "Pessoa2_pessoaId" });
            DropIndex("dbo.Agendamento", new[] { "usuarioId" });
            DropIndex("dbo.Agendamento", new[] { "pessoaId" });
            DropTable("dbo.Estado");
            DropTable("dbo.Clinica");
            DropTable("dbo.Cidades");
            DropTable("dbo.Endereco");
            DropTable("dbo.ItemRequisicao");
            DropTable("dbo.Requisicao");
            DropTable("dbo.User");
            DropTable("dbo.Setor");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Medico");
            DropTable("dbo.Cid");
            DropTable("dbo.Pericia");
            DropTable("dbo.DistribProcesso");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Agendamento");
        }
    }
}
