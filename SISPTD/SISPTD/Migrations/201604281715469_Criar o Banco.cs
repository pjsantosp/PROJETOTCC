namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriaroBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agendamento",
                c => new
                    {
                        agendamentoId = c.Long(nullable: false, identity: true),
                        pessoaId = c.Long(),
                        usuarioId = c.Long(),
                        observacoes = c.String(),
                        dt_Agendamento = c.DateTime(),
                        dt_Marcacao = c.DateTime(),
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
                        tipo = c.Int(),
                        dt_Cadastro = c.DateTime(),
                        cpf = c.String(maxLength: 25),
                        crm = c.String(maxLength: 8),
                        cns = c.String(maxLength: 25),
                        rg = c.String(maxLength: 25),
                        orgaoemissor = c.String(maxLength: 10),
                        dt_Emissao = c.DateTime(),
                        nome = c.String(nullable: false, maxLength: 160),
                        dt_Nascimento = c.DateTime(nullable: false),
                        idade = c.Int(),
                        email = c.String(maxLength: 50),
                        nome_Mae = c.String(maxLength: 150),
                        nome_Pai = c.String(maxLength: 150),
                        tel = c.String(nullable: false, maxLength: 20),
                        cel = c.String(maxLength: 20),
                        Endereco_enderecoId = c.Int(),
                        Pessoa_Pai_pessoaId = c.Long(),
                    })
                .PrimaryKey(t => t.pessoaId)
                .ForeignKey("dbo.Endereco", t => t.Endereco_enderecoId)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_Pai_pessoaId)
                .Index(t => t.Endereco_enderecoId)
                .Index(t => t.Pessoa_Pai_pessoaId);
            
            CreateTable(
                "dbo.DistribProcesso",
                c => new
                    {
                        distrib_ProcessoId = c.Long(nullable: false, identity: true),
                        SetorOrigemId = c.Long(nullable: false),
                        SetorDestinoId = c.Long(nullable: false),
                        observacoes = c.String(nullable: false),
                        pessoaId = c.Long(nullable: false),
                        usuarioEnviouId = c.Long(nullable: false),
                        usuarioRecebeuId = c.Long(),
                    })
                .PrimaryKey(t => t.distrib_ProcessoId)
                .ForeignKey("dbo.Setor", t => t.SetorOrigemId, cascadeDelete: true)
                .ForeignKey("dbo.Setor", t => t.SetorDestinoId)
                .ForeignKey("dbo.User", t => t.usuarioEnviouId)
                .ForeignKey("dbo.User", t => t.usuarioRecebeuId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
                .Index(t => t.SetorOrigemId)
                .Index(t => t.SetorDestinoId)
                .Index(t => t.pessoaId)
                .Index(t => t.usuarioEnviouId)
                .Index(t => t.usuarioRecebeuId);
            
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
                        Perfil = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usuarioId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
                .Index(t => t.pessoaId);
            
            CreateTable(
                "dbo.Requisicao",
                c => new
                    {
                        requisicaoId = c.Long(nullable: false, identity: true),
                        pacienteId = c.Long(),
                        usuarioId = c.Long(),
                        IdCidadesOrigem = c.Int(nullable: false),
                        IdCidadesDestino = c.Int(nullable: false),
                        dtRequisicao = c.DateTime(nullable: false),
                        observacoes = c.String(maxLength: 200),
                        via = c.Int(nullable: false),
                        trecho = c.Int(nullable: false),
                        Paciente_pessoaId = c.Long(),
                    })
                .PrimaryKey(t => t.requisicaoId)
                .ForeignKey("dbo.Cidades", t => t.IdCidadesDestino)
                .ForeignKey("dbo.Cidades", t => t.IdCidadesOrigem)
                .ForeignKey("dbo.Pessoa", t => t.Paciente_pessoaId)
                .ForeignKey("dbo.User", t => t.usuarioId)
                .Index(t => t.usuarioId)
                .Index(t => t.IdCidadesOrigem)
                .Index(t => t.IdCidadesDestino)
                .Index(t => t.Paciente_pessoaId);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        IdCidade = c.Int(nullable: false, identity: true),
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
                "dbo.Endereco",
                c => new
                    {
                        enderecoId = c.Int(nullable: false, identity: true),
                        IdCidade = c.Int(),
                        cep = c.String(maxLength: 15),
                        rua = c.String(maxLength: 100),
                        numero = c.Int(nullable: false),
                        bairro = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.enderecoId)
                .ForeignKey("dbo.Cidades", t => t.IdCidade)
                .Index(t => t.IdCidade);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        IdEstado = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Sigla = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.IdEstado);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        EspecialidadeId = c.Long(nullable: false, identity: true),
                        descricao = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.EspecialidadeId);
            
            CreateTable(
                "dbo.Pericia",
                c => new
                    {
                        periciaId = c.Long(nullable: false, identity: true),
                        descricao = c.String(nullable: false),
                        cidId = c.Long(),
                        dt_Pericia = c.DateTime(nullable: false),
                        situacao = c.String(),
                        pacientePessoaId = c.Long(),
                        TipoPericia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.periciaId)
                .ForeignKey("dbo.Cid", t => t.cidId)
                .ForeignKey("dbo.Pessoa", t => t.pacientePessoaId)
                .Index(t => t.cidId)
                .Index(t => t.pacientePessoaId);
            
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
                "dbo.PessoaRequisicao",
                c => new
                    {
                        requisicaoId = c.Long(nullable: false),
                        pessoaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.requisicaoId, t.pessoaId })
                .ForeignKey("dbo.Requisicao", t => t.requisicaoId, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .Index(t => t.requisicaoId)
                .Index(t => t.pessoaId);
            
            CreateTable(
                "dbo.PessoaEspecialidade",
                c => new
                    {
                        EspecialidadeId = c.Long(nullable: false),
                        pessoaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.EspecialidadeId, t.pessoaId })
                .ForeignKey("dbo.Especialidade", t => t.EspecialidadeId, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .Index(t => t.EspecialidadeId)
                .Index(t => t.pessoaId);
            
            CreateTable(
                "dbo.medicoPericia",
                c => new
                    {
                        pessoaId = c.Long(nullable: false),
                        periciaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.pessoaId, t.periciaId })
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .ForeignKey("dbo.Pericia", t => t.periciaId, cascadeDelete: true)
                .Index(t => t.pessoaId)
                .Index(t => t.periciaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pessoa", "Pessoa_Pai_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pericia", "pacientePessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.medicoPericia", "periciaId", "dbo.Pericia");
            DropForeignKey("dbo.medicoPericia", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pericia", "cidId", "dbo.Cid");
            DropForeignKey("dbo.PessoaEspecialidade", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.PessoaEspecialidade", "EspecialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.DistribProcesso", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Requisicao", "usuarioId", "dbo.User");
            DropForeignKey("dbo.PessoaRequisicao", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.PessoaRequisicao", "requisicaoId", "dbo.Requisicao");
            DropForeignKey("dbo.Requisicao", "Paciente_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Cidades", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidades");
            DropForeignKey("dbo.Pessoa", "Endereco_enderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Clinica", "IdCidade", "dbo.Cidades");
            DropForeignKey("dbo.Requisicao", "IdCidadesOrigem", "dbo.Cidades");
            DropForeignKey("dbo.Requisicao", "IdCidadesDestino", "dbo.Cidades");
            DropForeignKey("dbo.DistribProcesso", "usuarioRecebeuId", "dbo.User");
            DropForeignKey("dbo.DistribProcesso", "usuarioEnviouId", "dbo.User");
            DropForeignKey("dbo.Agendamento", "usuarioId", "dbo.User");
            DropForeignKey("dbo.DistribProcesso", "SetorDestinoId", "dbo.Setor");
            DropForeignKey("dbo.DistribProcesso", "SetorOrigemId", "dbo.Setor");
            DropForeignKey("dbo.Agendamento", "pessoaId", "dbo.Pessoa");
            DropIndex("dbo.medicoPericia", new[] { "periciaId" });
            DropIndex("dbo.medicoPericia", new[] { "pessoaId" });
            DropIndex("dbo.PessoaEspecialidade", new[] { "pessoaId" });
            DropIndex("dbo.PessoaEspecialidade", new[] { "EspecialidadeId" });
            DropIndex("dbo.PessoaRequisicao", new[] { "pessoaId" });
            DropIndex("dbo.PessoaRequisicao", new[] { "requisicaoId" });
            DropIndex("dbo.Pericia", new[] { "pacientePessoaId" });
            DropIndex("dbo.Pericia", new[] { "cidId" });
            DropIndex("dbo.Endereco", new[] { "IdCidade" });
            DropIndex("dbo.Clinica", new[] { "IdCidade" });
            DropIndex("dbo.Cidades", new[] { "IdEstado" });
            DropIndex("dbo.Requisicao", new[] { "Paciente_pessoaId" });
            DropIndex("dbo.Requisicao", new[] { "IdCidadesDestino" });
            DropIndex("dbo.Requisicao", new[] { "IdCidadesOrigem" });
            DropIndex("dbo.Requisicao", new[] { "usuarioId" });
            DropIndex("dbo.User", new[] { "pessoaId" });
            DropIndex("dbo.DistribProcesso", new[] { "usuarioRecebeuId" });
            DropIndex("dbo.DistribProcesso", new[] { "usuarioEnviouId" });
            DropIndex("dbo.DistribProcesso", new[] { "pessoaId" });
            DropIndex("dbo.DistribProcesso", new[] { "SetorDestinoId" });
            DropIndex("dbo.DistribProcesso", new[] { "SetorOrigemId" });
            DropIndex("dbo.Pessoa", new[] { "Pessoa_Pai_pessoaId" });
            DropIndex("dbo.Pessoa", new[] { "Endereco_enderecoId" });
            DropIndex("dbo.Agendamento", new[] { "usuarioId" });
            DropIndex("dbo.Agendamento", new[] { "pessoaId" });
            DropTable("dbo.medicoPericia");
            DropTable("dbo.PessoaEspecialidade");
            DropTable("dbo.PessoaRequisicao");
            DropTable("dbo.Cid");
            DropTable("dbo.Pericia");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Estado");
            DropTable("dbo.Endereco");
            DropTable("dbo.Clinica");
            DropTable("dbo.Cidades");
            DropTable("dbo.Requisicao");
            DropTable("dbo.User");
            DropTable("dbo.Setor");
            DropTable("dbo.DistribProcesso");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Agendamento");
        }
    }
}
