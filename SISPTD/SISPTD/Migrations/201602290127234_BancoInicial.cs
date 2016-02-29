namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BancoInicial : DbMigration
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
                    })
                .PrimaryKey(t => t.pessoaId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaPai)
                .Index(t => t.pessoaPai);
            
            CreateTable(
                "dbo.DistribProcesso",
                c => new
                    {
                        distrib_ProcessoId = c.Long(nullable: false, identity: true),
                        SetorOrigemId = c.Long(nullable: false),
                        SetorDestinoId = c.Long(nullable: false),
                        observacoes = c.String(),
                        pessoaId = c.Long(nullable: false),
                        usuarioEnviouId = c.Long(nullable: false),
                        usuarioRecebeuId = c.Long(),
                        periciaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.distrib_ProcessoId)
                .ForeignKey("dbo.Pericia", t => t.periciaId, cascadeDelete: true)
                .ForeignKey("dbo.Setor", t => t.SetorOrigemId, cascadeDelete: true)
                .ForeignKey("dbo.Setor", t => t.SetorDestinoId)
                .ForeignKey("dbo.User", t => t.usuarioEnviouId)
                .ForeignKey("dbo.User", t => t.usuarioRecebeuId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
                .Index(t => t.SetorOrigemId)
                .Index(t => t.SetorDestinoId)
                .Index(t => t.pessoaId)
                .Index(t => t.usuarioEnviouId)
                .Index(t => t.usuarioRecebeuId)
                .Index(t => t.periciaId);
            
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
                .ForeignKey("dbo.Medico", t => t.medicoId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
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
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
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
                        pessoaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.usuarioId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
                .Index(t => t.pessoaId);
            
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
                .ForeignKey("dbo.Requisicao", t => t.requisicaoId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
                .Index(t => t.pessoaId)
                .Index(t => t.requisicaoId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        enderecoId = c.Long(nullable: false, identity: true),
                        IdCidade = c.Int(nullable: false),
                        pessoaId = c.Long(nullable: false),
                        rua = c.String(nullable: false, maxLength: 150),
                        numero = c.String(nullable: false, maxLength: 5),
                        cep = c.String(maxLength: 8),
                        bairro = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.enderecoId)
                .ForeignKey("dbo.Cidades", t => t.IdCidade)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .Index(t => t.IdCidade)
                .Index(t => t.pessoaId);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        IdCidade = c.Int(nullable: false, identity: false),
                        Cidade = c.String(nullable: false, maxLength: 100),
                        IdEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCidade)
                .ForeignKey("dbo.Estado", t => t.IdEstado)
                .Index(t => t.IdEstado);
            
            CreateTable(
                "dbo.Clinica",
                c => new
                    {
                        clinicaId = c.Long(nullable: false, identity: true),
                        nome_Clinica = c.String(maxLength: 150),
                        IdCidade = c.Int(),
                        tel_Clinica = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.clinicaId)
                .ForeignKey("dbo.Cidades", t => t.IdCidade)
                .Index(t => t.IdCidade);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        IdEstado = c.Int(nullable: false, identity: false),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Sigla = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.IdEstado);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pessoa", "pessoaPai", "dbo.Pessoa");
            DropForeignKey("dbo.Pericia", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Medico", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.ItemRequisicao", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Endereco", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Cidades", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidades");
            DropForeignKey("dbo.Clinica", "IdCidade", "dbo.Cidades");
            DropForeignKey("dbo.DistribProcesso", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Requisicao", "usuarioId", "dbo.User");
            DropForeignKey("dbo.ItemRequisicao", "requisicaoId", "dbo.Requisicao");
            DropForeignKey("dbo.DistribProcesso", "usuarioRecebeuId", "dbo.User");
            DropForeignKey("dbo.DistribProcesso", "usuarioEnviouId", "dbo.User");
            DropForeignKey("dbo.Agendamento", "usuarioId", "dbo.User");
            DropForeignKey("dbo.DistribProcesso", "SetorDestinoId", "dbo.Setor");
            DropForeignKey("dbo.DistribProcesso", "SetorOrigemId", "dbo.Setor");
            DropForeignKey("dbo.Pericia", "medicoId", "dbo.Medico");
            DropForeignKey("dbo.Medico", "especialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.DistribProcesso", "periciaId", "dbo.Pericia");
            DropForeignKey("dbo.Pericia", "cidId", "dbo.Cid");
            DropForeignKey("dbo.Agendamento", "pessoaId", "dbo.Pessoa");
            DropIndex("dbo.Clinica", new[] { "IdCidade" });
            DropIndex("dbo.Cidades", new[] { "IdEstado" });
            DropIndex("dbo.Endereco", new[] { "pessoaId" });
            DropIndex("dbo.Endereco", new[] { "IdCidade" });
            DropIndex("dbo.ItemRequisicao", new[] { "requisicaoId" });
            DropIndex("dbo.ItemRequisicao", new[] { "pessoaId" });
            DropIndex("dbo.Requisicao", new[] { "usuarioId" });
            DropIndex("dbo.User", new[] { "pessoaId" });
            DropIndex("dbo.Medico", new[] { "pessoaId" });
            DropIndex("dbo.Medico", new[] { "especialidadeId" });
            DropIndex("dbo.Pericia", new[] { "pessoaId" });
            DropIndex("dbo.Pericia", new[] { "medicoId" });
            DropIndex("dbo.Pericia", new[] { "cidId" });
            DropIndex("dbo.DistribProcesso", new[] { "periciaId" });
            DropIndex("dbo.DistribProcesso", new[] { "usuarioRecebeuId" });
            DropIndex("dbo.DistribProcesso", new[] { "usuarioEnviouId" });
            DropIndex("dbo.DistribProcesso", new[] { "pessoaId" });
            DropIndex("dbo.DistribProcesso", new[] { "SetorDestinoId" });
            DropIndex("dbo.DistribProcesso", new[] { "SetorOrigemId" });
            DropIndex("dbo.Pessoa", new[] { "pessoaPai" });
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
