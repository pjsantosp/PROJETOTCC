USE [dbSISPTD]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 27/02/2016 20:22:26 ******/
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
/****** Object:  Table [dbo].[Agendamento]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agendamento](
	[agendamentoId] [bigint] IDENTITY(1,1) NOT NULL,
	[dt_Agendamento] [datetime] NULL,
	[dt_Marcacao] [datetime] NULL,
	[pessoaId] [bigint] NULL,
	[usuarioId] [bigint] NULL,
 CONSTRAINT [PK_dbo.Agendamento] PRIMARY KEY CLUSTERED 
(
	[agendamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cid]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cid](
	[cidId] [bigint] IDENTITY(1,1) NOT NULL,
	[codigoCid] [nvarchar](5) NULL,
	[descricao] [nvarchar](150) NULL,
 CONSTRAINT [PK_dbo.Cid] PRIMARY KEY CLUSTERED 
(
	[cidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cidades]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cidades](
	[IdCidade] [int] IDENTITY(1,1) NOT NULL,
	[Cidade] [nvarchar](100) NOT NULL,
	[IdEstado] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Cidades] PRIMARY KEY CLUSTERED 
(
	[IdCidade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clinica]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clinica](
	[clinicaId] [bigint] IDENTITY(1,1) NOT NULL,
	[nome_Clinica] [nvarchar](150) NULL,
	[fk_Cidade] [int] NULL,
	[tel_Clinica] [nvarchar](10) NULL,
	[Cidades_IdCidade] [int] NULL,
 CONSTRAINT [PK_dbo.Clinica] PRIMARY KEY CLUSTERED 
(
	[clinicaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DistribProcesso]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistribProcesso](
	[distrib_ProcessoId] [bigint] IDENTITY(1,1) NOT NULL,
	[origem] [bigint] NULL,
	[destino] [bigint] NOT NULL,
	[observacoes] [nvarchar](max) NULL,
	[usuarioEnviou] [bigint] NOT NULL,
	[usuarioRecebeu] [bigint] NULL,
	[periciaId] [bigint] NULL,
	[pessoaId] [bigint] NOT NULL,
	[Setor_setorId] [bigint] NULL,
	[Setor_setorId1] [bigint] NULL,
	[Setor_setorId2] [bigint] NULL,
	[Setor1_setorId] [bigint] NULL,
	[User_usuarioId] [bigint] NULL,
	[User_usuarioId1] [bigint] NULL,
	[User_usuarioId2] [bigint] NULL,
	[User1_usuarioId] [bigint] NULL,
 CONSTRAINT [PK_dbo.DistribProcesso] PRIMARY KEY CLUSTERED 
(
	[distrib_ProcessoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Endereco]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Endereco](
	[enderecoId] [bigint] IDENTITY(1,1) NOT NULL,
	[rua] [nvarchar](150) NOT NULL,
	[numero] [nvarchar](5) NOT NULL,
	[cep] [nvarchar](8) NULL,
	[bairro] [nvarchar](10) NULL,
	[IdCidade] [int] NOT NULL,
	[pessoaId] [bigint] NULL,
 CONSTRAINT [PK_dbo.Endereco] PRIMARY KEY CLUSTERED 
(
	[enderecoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Especialidade]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidade](
	[EspecialidadeId] [bigint] IDENTITY(1,1) NOT NULL,
	[descricao] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.Especialidade] PRIMARY KEY CLUSTERED 
(
	[EspecialidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Estado]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [nvarchar](20) NOT NULL,
	[Sigla] [nvarchar](2) NOT NULL,
 CONSTRAINT [PK_dbo.Estado] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemRequisicao]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemRequisicao](
	[itemId] [bigint] IDENTITY(1,1) NOT NULL,
	[pessoaId] [bigint] NOT NULL,
	[requisicaoId] [bigint] NULL,
 CONSTRAINT [PK_dbo.ItemRequisicao] PRIMARY KEY CLUSTERED 
(
	[itemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medico]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medico](
	[medicoId] [bigint] IDENTITY(1,1) NOT NULL,
	[especialidadeId] [bigint] NULL,
	[pessoaId] [bigint] NOT NULL,
	[crm_Medico] [nvarchar](20) NOT NULL,
	[Pessoa_pessoaId] [bigint] NULL,
 CONSTRAINT [PK_dbo.Medico] PRIMARY KEY CLUSTERED 
(
	[medicoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pericia]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pericia](
	[periciaId] [bigint] IDENTITY(1,1) NOT NULL,
	[descricao] [nvarchar](max) NOT NULL,
	[cidId] [bigint] NULL,
	[dt_Pericia] [datetime] NOT NULL,
	[medicoId] [bigint] NOT NULL,
	[situacao] [int] NULL,
	[pessoaId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.Pericia] PRIMARY KEY CLUSTERED 
(
	[periciaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoa](
	[pessoaId] [bigint] IDENTITY(1,1) NOT NULL,
	[pessoaPai] [bigint] NULL,
	[dt_Cadastro] [datetime] NULL,
	[cpf] [nvarchar](25) NOT NULL,
	[cns] [nvarchar](25) NULL,
	[rg] [nvarchar](25) NULL,
	[orgaoemissor] [nvarchar](10) NULL,
	[dt_Emissao] [datetime] NULL,
	[nome] [nvarchar](160) NOT NULL,
	[dt_Nascimento] [datetime] NOT NULL,
	[email] [nvarchar](50) NULL,
	[nome_Mae] [nvarchar](150) NOT NULL,
	[nome_Pai] [nvarchar](150) NOT NULL,
	[tel] [nvarchar](20) NOT NULL,
	[cel] [nvarchar](20) NULL,
	[Pessoa2_pessoaId] [bigint] NULL,
 CONSTRAINT [PK_dbo.Pessoa] PRIMARY KEY CLUSTERED 
(
	[pessoaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Requisicao]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Requisicao](
	[requisicaoId] [bigint] IDENTITY(1,1) NOT NULL,
	[usuarioId] [bigint] NULL,
	[observacoes] [varbinary](max) NULL,
 CONSTRAINT [PK_dbo.Requisicao] PRIMARY KEY CLUSTERED 
(
	[requisicaoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Setor]    Script Date: 27/02/2016 20:22:27 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 27/02/2016 20:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[usuarioId] [bigint] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[senha] [nvarchar](50) NOT NULL,
	[pessoaId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[usuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cidades] CHECK CONSTRAINT [FK_dbo.Cidades_dbo.Estado_IdEstado]
GO
ALTER TABLE [dbo].[Clinica]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Clinica_dbo.Cidades_Cidades_IdCidade] FOREIGN KEY([Cidades_IdCidade])
REFERENCES [dbo].[Cidades] ([IdCidade])
GO
ALTER TABLE [dbo].[Clinica] CHECK CONSTRAINT [FK_dbo.Clinica_dbo.Cidades_Cidades_IdCidade]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Pericia_Pericia_periciaId] FOREIGN KEY([periciaId])
REFERENCES [dbo].[Pericia] ([periciaId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Pericia_Pericia_periciaId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_Setor_setorId] FOREIGN KEY([Setor_setorId])
REFERENCES [dbo].[Setor] ([setorId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_Setor_setorId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_Setor_setorId1] FOREIGN KEY([Setor_setorId1])
REFERENCES [dbo].[Setor] ([setorId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_Setor_setorId1]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_Setor_setorId2] FOREIGN KEY([Setor_setorId2])
REFERENCES [dbo].[Setor] ([setorId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_Setor_setorId2]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_Setor1_setorId] FOREIGN KEY([Setor1_setorId])
REFERENCES [dbo].[Setor] ([setorId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.Setor_Setor1_setorId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_User_usuarioId] FOREIGN KEY([User_usuarioId])
REFERENCES [dbo].[User] ([usuarioId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_User_usuarioId]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_User_usuarioId1] FOREIGN KEY([User_usuarioId1])
REFERENCES [dbo].[User] ([usuarioId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_User_usuarioId1]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_User_usuarioId2] FOREIGN KEY([User_usuarioId2])
REFERENCES [dbo].[User] ([usuarioId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_User_usuarioId2]
GO
ALTER TABLE [dbo].[DistribProcesso]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_User1_usuarioId] FOREIGN KEY([User1_usuarioId])
REFERENCES [dbo].[User] ([usuarioId])
GO
ALTER TABLE [dbo].[DistribProcesso] CHECK CONSTRAINT [FK_dbo.DistribProcesso_dbo.User_User1_usuarioId]
GO
ALTER TABLE [dbo].[Endereco]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Endereco_dbo.Cidades_IdCidade] FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidades] ([IdCidade])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Endereco] CHECK CONSTRAINT [FK_dbo.Endereco_dbo.Cidades_IdCidade]
GO
ALTER TABLE [dbo].[Endereco]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Endereco_dbo.Pessoa_Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[Endereco] CHECK CONSTRAINT [FK_dbo.Endereco_dbo.Pessoa_Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[ItemRequisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ItemRequisicao_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ItemRequisicao] CHECK CONSTRAINT [FK_dbo.ItemRequisicao_dbo.Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[ItemRequisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ItemRequisicao_dbo.Requisicao_requisicaoId] FOREIGN KEY([requisicaoId])
REFERENCES [dbo].[Requisicao] ([requisicaoId])
GO
ALTER TABLE [dbo].[ItemRequisicao] CHECK CONSTRAINT [FK_dbo.ItemRequisicao_dbo.Requisicao_requisicaoId]
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Medico_dbo.Especialidade_especialidadeId] FOREIGN KEY([especialidadeId])
REFERENCES [dbo].[Especialidade] ([EspecialidadeId])
GO
ALTER TABLE [dbo].[Medico] CHECK CONSTRAINT [FK_dbo.Medico_dbo.Especialidade_especialidadeId]
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Medico_dbo.Pessoa_Pessoa_pessoaId] FOREIGN KEY([Pessoa_pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[Medico] CHECK CONSTRAINT [FK_dbo.Medico_dbo.Pessoa_Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Medico_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[Medico] CHECK CONSTRAINT [FK_dbo.Medico_dbo.Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[Pericia]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pericia_dbo.Cid_cidId] FOREIGN KEY([cidId])
REFERENCES [dbo].[Cid] ([cidId])
GO
ALTER TABLE [dbo].[Pericia] CHECK CONSTRAINT [FK_dbo.Pericia_dbo.Cid_cidId]
GO
ALTER TABLE [dbo].[Pericia]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pericia_dbo.Medico_medicoId] FOREIGN KEY([medicoId])
REFERENCES [dbo].[Medico] ([medicoId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pericia] CHECK CONSTRAINT [FK_dbo.Pericia_dbo.Medico_medicoId]
GO
ALTER TABLE [dbo].[Pericia]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pericia_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pericia] CHECK CONSTRAINT [FK_dbo.Pericia_dbo.Pessoa_pessoaId]
GO
ALTER TABLE [dbo].[Pessoa]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pessoa_dbo.Pessoa_Pessoa2_pessoaId] FOREIGN KEY([Pessoa2_pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
GO
ALTER TABLE [dbo].[Pessoa] CHECK CONSTRAINT [FK_dbo.Pessoa_dbo.Pessoa_Pessoa2_pessoaId]
GO
ALTER TABLE [dbo].[Requisicao]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Requisicao_dbo.User_usuarioId] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[User] ([usuarioId])
GO
ALTER TABLE [dbo].[Requisicao] CHECK CONSTRAINT [FK_dbo.Requisicao_dbo.User_usuarioId]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_dbo.Pessoa_pessoaId] FOREIGN KEY([pessoaId])
REFERENCES [dbo].[Pessoa] ([pessoaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_dbo.User_dbo.Pessoa_pessoaId]
GO
