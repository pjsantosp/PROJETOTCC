namespace SISPTD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeuMigrationInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agendamento",
                c => new
                    {
                        agendamentoId = c.Long(nullable: false),
                        pessoaId = c.Long(),
                        usuarioId = c.Long(),
                        clinicaId = c.Long(),
                        observacoes = c.String(),
                        dt_Agendamento = c.DateTime(nullable: false),
                        dt_Marcacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.agendamentoId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
                .ForeignKey("dbo.Processo", t => t.agendamentoId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.usuarioId)
                .ForeignKey("dbo.Clinica", t => t.clinicaId)
                .Index(t => t.agendamentoId)
                .Index(t => t.pessoaId)
                .Index(t => t.usuarioId)
                .Index(t => t.clinicaId);
            
            CreateTable(
                "dbo.Clinica",
                c => new
                    {
                        clinicaId = c.Long(nullable: false, identity: true),
                        nome_Clinica = c.String(maxLength: 150),
                        IdCidade = c.Int(),
                        tel_Clinica = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.clinicaId)
                .ForeignKey("dbo.Cidades", t => t.IdCidade)
                .Index(t => t.IdCidade);
            
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
                "dbo.Requisicao",
                c => new
                    {
                        requisicaoId = c.Long(nullable: false, identity: true),
                        pacienteId = c.Long(nullable: false),
                        usuarioId = c.Long(),
                        IdCidadesOrigem = c.Int(nullable: false),
                        IdCidadesDestino = c.Int(nullable: false),
                        dtRequisicao = c.DateTime(nullable: false),
                        observacoes = c.String(maxLength: 200),
                        Via = c.Int(nullable: false),
                        Trecho = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.requisicaoId)
                .ForeignKey("dbo.User", t => t.usuarioId)
                .ForeignKey("dbo.Pessoa", t => t.pacienteId, cascadeDelete: true)
                .ForeignKey("dbo.Cidades", t => t.IdCidadesDestino)
                .ForeignKey("dbo.Cidades", t => t.IdCidadesOrigem)
                .Index(t => t.pacienteId)
                .Index(t => t.usuarioId)
                .Index(t => t.IdCidadesOrigem)
                .Index(t => t.IdCidadesDestino);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        pessoaId = c.Long(nullable: false, identity: true),
                        acompanhanteId = c.Long(),
                        tipo = c.Int(),
                        dt_Cadastro = c.DateTime(),
                        cpf = c.String(maxLength: 25),
                        crm = c.String(maxLength: 8),
                        cns = c.String(nullable: false, maxLength: 25),
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
                        Acompanhante_pessoaId = c.Long(),
                        Endereco_enderecoId = c.Int(),
                        Requisicao_requisicaoId = c.Long(),
                    })
                .PrimaryKey(t => t.pessoaId)
                .ForeignKey("dbo.Pessoa", t => t.Acompanhante_pessoaId)
                .ForeignKey("dbo.Endereco", t => t.Endereco_enderecoId)
                .ForeignKey("dbo.Requisicao", t => t.Requisicao_requisicaoId)
                .Index(t => t.Acompanhante_pessoaId)
                .Index(t => t.Endereco_enderecoId)
                .Index(t => t.Requisicao_requisicaoId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        enderecoId = c.Int(nullable: false, identity: true),
                        IdCidade = c.Int(),
                        cep = c.String(maxLength: 15),
                        rua = c.String(nullable: false, maxLength: 100),
                        numero = c.Int(nullable: false),
                        bairro = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.enderecoId)
                .ForeignKey("dbo.Cidades", t => t.IdCidade)
                .Index(t => t.IdCidade);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        EspecialidadeId = c.Long(nullable: false, identity: true),
                        descricao = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.EspecialidadeId);
            
            CreateTable(
                "dbo.Processo",
                c => new
                    {
                        processoId = c.Long(nullable: false, identity: true),
                        dtCadastro = c.DateTime(nullable: false),
                        Procedimento = c.String(),
                        Clinica = c.String(),
                        cidId = c.Long(),
                        CaraterInternacao = c.String(),
                        observacoes = c.String(nullable: false),
                        movimentacaoId = c.Long(nullable: false),
                        agendamentoId = c.Long(),
                        setorId = c.Long(),
                        pacienteId = c.Long(),
                        medicoId = c.Long(),
                    })
                .PrimaryKey(t => t.processoId)
                .ForeignKey("dbo.Cid", t => t.cidId)
                .ForeignKey("dbo.Pessoa", t => t.medicoId)
                .ForeignKey("dbo.Pessoa", t => t.pacienteId)
                .Index(t => t.cidId)
                .Index(t => t.pacienteId)
                .Index(t => t.medicoId);
            
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
                "dbo.Pericia",
                c => new
                    {
                        periciaId = c.Long(nullable: false, identity: true),
                        processoId = c.Long(nullable: false),
                        descricao = c.String(nullable: false),
                        cidId = c.Long(),
                        dt_Pericia = c.DateTime(nullable: false),
                        medicoPessoaId = c.Long(nullable: false),
                        TipoPericia = c.Int(nullable: false),
                        Situacao = c.Int(nullable: false),
                        Pessoa_pessoaId = c.Long(),
                    })
                .PrimaryKey(t => t.periciaId)
                .ForeignKey("dbo.Cid", t => t.cidId)
                .ForeignKey("dbo.Processo", t => t.periciaId)
                .ForeignKey("dbo.Pessoa", t => t.medicoPessoaId, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_pessoaId)
                .Index(t => t.periciaId)
                .Index(t => t.cidId)
                .Index(t => t.medicoPessoaId)
                .Index(t => t.Pessoa_pessoaId);
            
            CreateTable(
                "dbo.Movimentacao",
                c => new
                    {
                        movimentacaoId = c.Long(nullable: false, identity: true),
                        usuarioEnviouId = c.Long(nullable: false),
                        usuarioRecebeuId = c.Long(),
                        setorEnviouId = c.Long(nullable: false),
                        setorRecebeuId = c.Long(),
                        ProcessoId = c.Long(),
                        dtEnvio = c.DateTime(nullable: false),
                        dtRecebimento = c.DateTime(),
                    })
                .PrimaryKey(t => t.movimentacaoId)
                .ForeignKey("dbo.Setor", t => t.setorEnviouId)
                .ForeignKey("dbo.Setor", t => t.setorRecebeuId)
                .ForeignKey("dbo.User", t => t.usuarioEnviouId)
                .ForeignKey("dbo.User", t => t.usuarioRecebeuId)
                .ForeignKey("dbo.Processo", t => t.ProcessoId, cascadeDelete: true)
                .Index(t => t.usuarioEnviouId)
                .Index(t => t.usuarioRecebeuId)
                .Index(t => t.setorEnviouId)
                .Index(t => t.setorRecebeuId)
                .Index(t => t.ProcessoId);
            
            CreateTable(
                "dbo.Setor",
                c => new
                    {
                        setorId = c.Long(nullable: false, identity: true),
                        usuarioId = c.Long(),
                        descricao = c.String(maxLength: 25),
                        abreviacao = c.String(maxLength: 25),
                        Processo_processoId = c.Long(),
                    })
                .PrimaryKey(t => t.setorId)
                .ForeignKey("dbo.Processo", t => t.Processo_processoId)
                .Index(t => t.Processo_processoId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        usuarioId = c.Long(nullable: false, identity: true),
                        setorId = c.Long(nullable: false),
                        login = c.String(nullable: false, maxLength: 50),
                        senha = c.String(maxLength: 50),
                        pessoaId = c.Long(nullable: false),
                        Perfil = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usuarioId)
                .ForeignKey("dbo.Setor", t => t.setorId, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId)
                .Index(t => t.setorId)
                .Index(t => t.pessoaId);
            
            CreateTable(
                "dbo.PessoaRequisicao",
                c => new
                    {
                        pessoaRequisicaoId = c.Int(nullable: false, identity: true),
                        pessoaId = c.Long(nullable: false),
                        TipoPessoa = c.Int(nullable: false),
                        requisicaoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.pessoaRequisicaoId)
                .ForeignKey("dbo.Pessoa", t => t.pessoaId, cascadeDelete: true)
                .ForeignKey("dbo.Requisicao", t => t.requisicaoId)
                .Index(t => t.pessoaId)
                .Index(t => t.requisicaoId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agendamento", "clinicaId", "dbo.Clinica");
            DropForeignKey("dbo.Cidades", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidades");
            DropForeignKey("dbo.Clinica", "IdCidade", "dbo.Cidades");
            DropForeignKey("dbo.Requisicao", "IdCidadesOrigem", "dbo.Cidades");
            DropForeignKey("dbo.Requisicao", "IdCidadesDestino", "dbo.Cidades");
            DropForeignKey("dbo.PessoaRequisicao", "requisicaoId", "dbo.Requisicao");
            DropForeignKey("dbo.Pessoa", "Requisicao_requisicaoId", "dbo.Requisicao");
            DropForeignKey("dbo.User", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Requisicao", "pacienteId", "dbo.Pessoa");
            DropForeignKey("dbo.PessoaRequisicao", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pericia", "Pessoa_pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pericia", "medicoPessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Processo", "pacienteId", "dbo.Pessoa");
            DropForeignKey("dbo.Processo", "medicoId", "dbo.Pessoa");
            DropForeignKey("dbo.Setor", "Processo_processoId", "dbo.Processo");
            DropForeignKey("dbo.Pericia", "periciaId", "dbo.Processo");
            DropForeignKey("dbo.Movimentacao", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.Movimentacao", "usuarioRecebeuId", "dbo.User");
            DropForeignKey("dbo.Movimentacao", "usuarioEnviouId", "dbo.User");
            DropForeignKey("dbo.Movimentacao", "setorRecebeuId", "dbo.Setor");
            DropForeignKey("dbo.User", "setorId", "dbo.Setor");
            DropForeignKey("dbo.Requisicao", "usuarioId", "dbo.User");
            DropForeignKey("dbo.Agendamento", "usuarioId", "dbo.User");
            DropForeignKey("dbo.Movimentacao", "setorEnviouId", "dbo.Setor");
            DropForeignKey("dbo.Processo", "cidId", "dbo.Cid");
            DropForeignKey("dbo.Pericia", "cidId", "dbo.Cid");
            DropForeignKey("dbo.Agendamento", "agendamentoId", "dbo.Processo");
            DropForeignKey("dbo.PessoaEspecialidade", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.PessoaEspecialidade", "EspecialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.Pessoa", "Endereco_enderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Agendamento", "pessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Pessoa", "Acompanhante_pessoaId", "dbo.Pessoa");
            DropIndex("dbo.PessoaEspecialidade", new[] { "pessoaId" });
            DropIndex("dbo.PessoaEspecialidade", new[] { "EspecialidadeId" });
            DropIndex("dbo.PessoaRequisicao", new[] { "requisicaoId" });
            DropIndex("dbo.PessoaRequisicao", new[] { "pessoaId" });
            DropIndex("dbo.User", new[] { "pessoaId" });
            DropIndex("dbo.User", new[] { "setorId" });
            DropIndex("dbo.Setor", new[] { "Processo_processoId" });
            DropIndex("dbo.Movimentacao", new[] { "ProcessoId" });
            DropIndex("dbo.Movimentacao", new[] { "setorRecebeuId" });
            DropIndex("dbo.Movimentacao", new[] { "setorEnviouId" });
            DropIndex("dbo.Movimentacao", new[] { "usuarioRecebeuId" });
            DropIndex("dbo.Movimentacao", new[] { "usuarioEnviouId" });
            DropIndex("dbo.Pericia", new[] { "Pessoa_pessoaId" });
            DropIndex("dbo.Pericia", new[] { "medicoPessoaId" });
            DropIndex("dbo.Pericia", new[] { "cidId" });
            DropIndex("dbo.Pericia", new[] { "periciaId" });
            DropIndex("dbo.Processo", new[] { "medicoId" });
            DropIndex("dbo.Processo", new[] { "pacienteId" });
            DropIndex("dbo.Processo", new[] { "cidId" });
            DropIndex("dbo.Endereco", new[] { "IdCidade" });
            DropIndex("dbo.Pessoa", new[] { "Requisicao_requisicaoId" });
            DropIndex("dbo.Pessoa", new[] { "Endereco_enderecoId" });
            DropIndex("dbo.Pessoa", new[] { "Acompanhante_pessoaId" });
            DropIndex("dbo.Requisicao", new[] { "IdCidadesDestino" });
            DropIndex("dbo.Requisicao", new[] { "IdCidadesOrigem" });
            DropIndex("dbo.Requisicao", new[] { "usuarioId" });
            DropIndex("dbo.Requisicao", new[] { "pacienteId" });
            DropIndex("dbo.Cidades", new[] { "IdEstado" });
            DropIndex("dbo.Clinica", new[] { "IdCidade" });
            DropIndex("dbo.Agendamento", new[] { "clinicaId" });
            DropIndex("dbo.Agendamento", new[] { "usuarioId" });
            DropIndex("dbo.Agendamento", new[] { "pessoaId" });
            DropIndex("dbo.Agendamento", new[] { "agendamentoId" });
            DropTable("dbo.PessoaEspecialidade");
            DropTable("dbo.Estado");
            DropTable("dbo.PessoaRequisicao");
            DropTable("dbo.User");
            DropTable("dbo.Setor");
            DropTable("dbo.Movimentacao");
            DropTable("dbo.Pericia");
            DropTable("dbo.Cid");
            DropTable("dbo.Processo");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Endereco");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Requisicao");
            DropTable("dbo.Cidades");
            DropTable("dbo.Clinica");
            DropTable("dbo.Agendamento");
        }
    }
}
