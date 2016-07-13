USE [master]
GO
/****** Object:  Database [dbSISPTD]    Script Date: 16/05/2016 22:08:57 ******/
CREATE DATABASE [dbSISPTD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbSISPTD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\dbSISPTD.mdf' , SIZE = 6208KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dbSISPTD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\dbSISPTD_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbSISPTD] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbSISPTD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbSISPTD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbSISPTD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbSISPTD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbSISPTD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbSISPTD] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbSISPTD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbSISPTD] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [dbSISPTD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbSISPTD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbSISPTD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbSISPTD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbSISPTD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbSISPTD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbSISPTD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbSISPTD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbSISPTD] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dbSISPTD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbSISPTD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbSISPTD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbSISPTD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbSISPTD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbSISPTD] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [dbSISPTD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbSISPTD] SET RECOVERY FULL 
GO
ALTER DATABASE [dbSISPTD] SET  MULTI_USER 
GO
ALTER DATABASE [dbSISPTD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbSISPTD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbSISPTD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbSISPTD] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbSISPTD', N'ON'
GO
USE [dbSISPTD]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Agendamento]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agendamento](
	[agendamentoId] [bigint] IDENTITY(1,1) NOT NULL,
	[pessoaId] [bigint] NULL,
	[usuarioId] [bigint] NULL,
	[clinicaId] [bigint] NULL,
	[observacoes] [nvarchar](max) NULL,
	[dt_Agendamento] [datetime] NOT NULL,
	[dt_Marcacao] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Agendamento] PRIMARY KEY CLUSTERED 
(
	[agendamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cid]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cid](
	[cidId] [bigint] NOT NULL,
	[codigoCid] [nvarchar](5) NULL,
	[descricao] [nvarchar](150) NULL,
 CONSTRAINT [PK_dbo.Cid] PRIMARY KEY CLUSTERED 
(
	[cidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cidades]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cidades](
	[IdCidade] [int] NOT NULL,
	[Cidade] [nvarchar](100) NOT NULL,
	[IdEstado] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Cidades] PRIMARY KEY CLUSTERED 
(
	[IdCidade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clinica]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clinica](
	[clinicaId] [bigint] IDENTITY(1,1) NOT NULL,
	[nome_Clinica] [nvarchar](150) NULL,
	[IdCidade] [int] NULL,
	[tel_Clinica] [nvarchar](20) NULL,
 CONSTRAINT [PK_dbo.Clinica] PRIMARY KEY CLUSTERED 
(
	[clinicaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DistribProcesso]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistribProcesso](
	[distrib_ProcessoId] [bigint] IDENTITY(1,1) NOT NULL,
	[SetorOrigemId] [bigint] NOT NULL,
	[SetorDestinoId] [bigint] NOT NULL,
	[observacoes] [nvarchar](max) NOT NULL,
	[pessoaId] [bigint] NOT NULL,
	[usuarioEnviouId] [bigint] NOT NULL,
	[usuarioRecebeuId] [bigint] NULL,
 CONSTRAINT [PK_dbo.DistribProcesso] PRIMARY KEY CLUSTERED 
(
	[distrib_ProcessoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Endereco]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Endereco](
	[enderecoId] [int] IDENTITY(1,1) NOT NULL,
	[IdCidade] [int] NULL,
	[cep] [nvarchar](15) NULL,
	[rua] [nvarchar](100) NOT NULL,
	[numero] [int] NOT NULL,
	[bairro] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Endereco] PRIMARY KEY CLUSTERED 
(
	[enderecoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Especialidade]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidade](
	[EspecialidadeId] [bigint] IDENTITY(1,1) NOT NULL,
	[descricao] [nvarchar](150) NULL,
 CONSTRAINT [PK_dbo.Especialidade] PRIMARY KEY CLUSTERED 
(
	[EspecialidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Estado]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[IdEstado] [int] NOT NULL,
	[Estado] [nvarchar](20) NOT NULL,
	[Sigla] [nvarchar](2) NOT NULL,
 CONSTRAINT [PK_dbo.Estado] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoa](
	[pessoaId] [bigint] IDENTITY(1,1) NOT NULL,
	[pessoaPai] [bigint] NULL,
	[tipo] [int] NULL,
	[dt_Cadastro] [datetime] NULL,
	[cpf] [nvarchar](25) NULL,
	[crm] [nvarchar](8) NULL,
	[cns] [nvarchar](25) NOT NULL,
	[rg] [nvarchar](25) NULL,
	[orgaoemissor] [nvarchar](10) NULL,
	[dt_Emissao] [datetime] NULL,
	[nome] [nvarchar](160) NOT NULL,
	[dt_Nascimento] [datetime] NOT NULL,
	[idade] [int] NULL,
	[email] [nvarchar](50) NULL,
	[nome_Mae] [nvarchar](150) NULL,
	[nome_Pai] [nvarchar](150) NULL,
	[tel] [nvarchar](20) NOT NULL,
	[cel] [nvarchar](20) NULL,
	[Endereco_enderecoId] [int] NULL,
	[Pessoa_Pai_pessoaId] [bigint] NULL,
 CONSTRAINT [PK_dbo.Pessoa] PRIMARY KEY CLUSTERED 
(
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PessoaEspecialidade]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PessoaEspecialidade](
	[EspecialidadeId] [bigint] NOT NULL,
	[pessoaId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.PessoaEspecialidade] PRIMARY KEY CLUSTERED 
(
	[EspecialidadeId] ASC,
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PessoaRequisicao]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PessoaRequisicao](
	[pessoaId] [bigint] NOT NULL,
	[requisicaoId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.PessoaRequisicao] PRIMARY KEY CLUSTERED 
(
	[pessoaId] ASC,
	[requisicaoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Requisicao]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requisicao](
	[requisicaoId] [bigint] IDENTITY(1,1) NOT NULL,
	[usuarioId] [bigint] NULL,
	[IdCidadesOrigem] [int] NOT NULL,
	[IdCidadesDestino] [int] NOT NULL,
	[dtRequisicao] [datetime] NOT NULL,
	[observacoes] [nvarchar](200) NULL,
	[via] [int] NOT NULL,
	[trecho] [int] NOT NULL,
	[pacienteId] [bigint] NULL,
 CONSTRAINT [PK_dbo.Requisicao] PRIMARY KEY CLUSTERED 
(
	[requisicaoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Setor]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setor](
	[setorId] [bigint] IDENTITY(1,1) NOT NULL,
	[descricao] [nvarchar](25) NULL,
 CONSTRAINT [PK_dbo.Setor] PRIMARY KEY CLUSTERED 
(
	[setorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SolicitacaoPericia]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolicitacaoPericia](
	[periciaId] [bigint] IDENTITY(1,1) NOT NULL,
	[descricao] [nvarchar](max) NOT NULL,
	[cidId] [bigint] NULL,
	[dt_Pericia] [datetime] NOT NULL,
	[pacientePessoaId] [bigint] NULL,
	[medicoPessoaId] [bigint] NULL,
	[TipoPericia] [int] NOT NULL,
	[Situacao] [int] NOT NULL,
 CONSTRAINT [PK_dbo.SolicitacaoPericia] PRIMARY KEY CLUSTERED 
(
	[periciaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 16/05/2016 22:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[usuarioId] [bigint] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[senha] [nvarchar](50) NULL,
	[pessoaId] [bigint] NOT NULL,
	[Perfil] [int] NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[usuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_clinicaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_clinicaId] ON [dbo].[Agendamento]
(
	[clinicaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_pessoaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_pessoaId] ON [dbo].[Agendamento]
(
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_usuarioId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_usuarioId] ON [dbo].[Agendamento]
(
	[usuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdEstado]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_IdEstado] ON [dbo].[Cidades]
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdCidade]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_IdCidade] ON [dbo].[Clinica]
(
	[IdCidade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_pessoaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_pessoaId] ON [dbo].[DistribProcesso]
(
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SetorDestinoId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_SetorDestinoId] ON [dbo].[DistribProcesso]
(
	[SetorDestinoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SetorOrigemId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_SetorOrigemId] ON [dbo].[DistribProcesso]
(
	[SetorOrigemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_usuarioEnviouId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_usuarioEnviouId] ON [dbo].[DistribProcesso]
(
	[usuarioEnviouId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_usuarioRecebeuId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_usuarioRecebeuId] ON [dbo].[DistribProcesso]
(
	[usuarioRecebeuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdCidade]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_IdCidade] ON [dbo].[Endereco]
(
	[IdCidade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Endereco_enderecoId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_Endereco_enderecoId] ON [dbo].[Pessoa]
(
	[Endereco_enderecoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pessoa_Pai_pessoaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_Pessoa_Pai_pessoaId] ON [dbo].[Pessoa]
(
	[Pessoa_Pai_pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EspecialidadeId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_EspecialidadeId] ON [dbo].[PessoaEspecialidade]
(
	[EspecialidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_pessoaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_pessoaId] ON [dbo].[PessoaEspecialidade]
(
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_pessoaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_pessoaId] ON [dbo].[PessoaRequisicao]
(
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_requisicaoId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_requisicaoId] ON [dbo].[PessoaRequisicao]
(
	[requisicaoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdCidadesDestino]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_IdCidadesDestino] ON [dbo].[Requisicao]
(
	[IdCidadesDestino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdCidadesOrigem]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_IdCidadesOrigem] ON [dbo].[Requisicao]
(
	[IdCidadesOrigem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_pacienteId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_pacienteId] ON [dbo].[Requisicao]
(
	[pacienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_usuarioId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_usuarioId] ON [dbo].[Requisicao]
(
	[usuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_cidId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_cidId] ON [dbo].[SolicitacaoPericia]
(
	[cidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_medicoPessoaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_medicoPessoaId] ON [dbo].[SolicitacaoPericia]
(
	[medicoPessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_pacientePessoaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_pacientePessoaId] ON [dbo].[SolicitacaoPericia]
(
	[pacientePessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_pessoaId]    Script Date: 16/05/2016 22:08:57 ******/
CREATE NONCLUSTERED INDEX [IX_pessoaId] ON [dbo].[User]
(
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Agendamento]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Agendamento_dbo.Clinica_clinicaId] FOREIGN KEY([clinicaId])
REFERENCES [dbo].[Clinica] ([clinicaId])
GO
ALTER TABLE [dbo].[Agendamento] CHECK CONSTRAINT [FK_dbo.Agendamento_dbo.Clinica_clinicaId]
GO
ALTER TABLE [dbo].[Agendamento]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Agendamento_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[Agendamento] CHECK CONSTRAINT [FK_dbo.Agendamento_dbo.Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[Agendamento]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Agendamento_dbo.User_usuarioId] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[User] ([usuarioId])
GO
ALTER TABLE [dbo].[Agendamento] CHECK CONSTRAINT [FK_dbo.Agendamento_dbo.User_usuarioId]
GO
ALTER TABLE [dbo].[Cidades]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Cidades_dbo.Estado_IdEstado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Cidades] CHECK CONSTRAINT [FK_dbo.Cidades_dbo.Estado_IdEstado]
GO
ALTER TABLE [dbo].[Clinica]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Clinica_dbo.Cidades_IdCidade] FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidades] ([IdCidade])
GO
ALTER TABLE [dbo].[Clinica] CHECK CONSTRAINT [FK_dbo.Clinica_dbo.Cidades_IdCidade]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_SetorDestinoId] FOREIGN KEY([SetorDestinoId])
REFERENCES [dbo].[Setor] ([setorId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_SetorDestinoId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_SetorOrigemId] FOREIGN KEY([SetorOrigemId])
REFERENCES [dbo].[Setor] ([setorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_SetorOrigemId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_usuarioEnviouId] FOREIGN KEY([usuarioEnviouId])
REFERENCES [dbo].[User] ([usuarioId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_usuarioEnviouId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_usuarioRecebeuId] FOREIGN KEY([usuarioRecebeuId])
REFERENCES [dbo].[User] ([usuarioId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_usuarioRecebeuId]
GO
ALTER TABLE [dbo].[Endereco]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Endereco_dbo.Cidades_IdCidade] FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidades] ([IdCidade])
GO
ALTER TABLE [dbo].[Endereco] CHECK CONSTRAINT [FK_dbo.Endereco_dbo.Cidades_IdCidade]
GO
ALTER TABLE [dbo].[Pessoa]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pessoa_dbo.Endereco_Endereco_enderecoId] FOREIGN KEY([Endereco_enderecoId])
REFERENCES [dbo].[Endereco] ([enderecoId])
GO
ALTER TABLE [dbo].[Pessoa] CHECK CONSTRAINT [FK_dbo.Pessoa_dbo.Endereco_Endereco_enderecoId]
GO
ALTER TABLE [dbo].[Pessoa]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pessoa_dbo.Pessoa_Pessoa_Pai_pessoaId] FOREIGN KEY([Pessoa_Pai_pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[Pessoa] CHECK CONSTRAINT [FK_dbo.Pessoa_dbo.Pessoa_Pessoa_Pai_pessoaId]
GO
ALTER TABLE [dbo].[PessoaEspecialidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PessoaEspecialidade_dbo.Especialidade_EspecialidadeId] FOREIGN KEY([EspecialidadeId])
REFERENCES [dbo].[Especialidade] ([EspecialidadeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PessoaEspecialidade] CHECK CONSTRAINT [FK_dbo.PessoaEspecialidade_dbo.Especialidade_EspecialidadeId]
GO
ALTER TABLE [dbo].[PessoaEspecialidade]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PessoaEspecialidade_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PessoaEspecialidade] CHECK CONSTRAINT [FK_dbo.PessoaEspecialidade_dbo.Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[PessoaRequisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PessoaRequisicao_dbo.Pessoa_pessoaId] FOREIGN KEY([requisicaoId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PessoaRequisicao] CHECK CONSTRAINT [FK_dbo.PessoaRequisicao_dbo.Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[PessoaRequisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PessoaRequisicao_dbo.Requisicao_requisicaoId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Requisicao] ([requisicaoId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PessoaRequisicao] CHECK CONSTRAINT [FK_dbo.PessoaRequisicao_dbo.Requisicao_requisicaoId]
GO
ALTER TABLE [dbo].[Requisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Requisicao_dbo.Cidades_IdCidadesDestino] FOREIGN KEY([IdCidadesDestino])
REFERENCES [dbo].[Cidades] ([IdCidade])
GO
ALTER TABLE [dbo].[Requisicao] CHECK CONSTRAINT [FK_dbo.Requisicao_dbo.Cidades_IdCidadesDestino]
GO
ALTER TABLE [dbo].[Requisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Requisicao_dbo.Cidades_IdCidadesOrigem] FOREIGN KEY([IdCidadesOrigem])
REFERENCES [dbo].[Cidades] ([IdCidade])
GO
ALTER TABLE [dbo].[Requisicao] CHECK CONSTRAINT [FK_dbo.Requisicao_dbo.Cidades_IdCidadesOrigem]
GO
ALTER TABLE [dbo].[Requisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Requisicao_dbo.Pessoa_Paciente_pessoaId] FOREIGN KEY([pacienteId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[Requisicao] CHECK CONSTRAINT [FK_dbo.Requisicao_dbo.Pessoa_Paciente_pessoaId]
GO
ALTER TABLE [dbo].[Requisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Requisicao_dbo.User_usuarioId] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[User] ([usuarioId])
GO
ALTER TABLE [dbo].[Requisicao] CHECK CONSTRAINT [FK_dbo.Requisicao_dbo.User_usuarioId]
GO
ALTER TABLE [dbo].[SolicitacaoPericia]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SolicitacaoPericia_dbo.Cid_cidId] FOREIGN KEY([cidId])
REFERENCES [dbo].[Cid] ([cidId])
GO
ALTER TABLE [dbo].[SolicitacaoPericia] CHECK CONSTRAINT [FK_dbo.SolicitacaoPericia_dbo.Cid_cidId]
GO
ALTER TABLE [dbo].[SolicitacaoPericia]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SolicitacaoPericia_dbo.Pessoa_medicoPessoaId] FOREIGN KEY([medicoPessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[SolicitacaoPericia] CHECK CONSTRAINT [FK_dbo.SolicitacaoPericia_dbo.Pessoa_medicoPessoaId]
GO
ALTER TABLE [dbo].[SolicitacaoPericia]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SolicitacaoPericia_dbo.Pessoa_pacientePessoaId] FOREIGN KEY([pacientePessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[SolicitacaoPericia] CHECK CONSTRAINT [FK_dbo.SolicitacaoPericia_dbo.Pessoa_pacientePessoaId]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_dbo.User_dbo.Pessoa_pessoaId]
GO
USE [master]
GO
ALTER DATABASE [dbSISPTD] SET  READ_WRITE 
GO
