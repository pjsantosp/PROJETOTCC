namespace SISPTD.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class dbSISPTD : DbContext
    {
        public dbSISPTD()
            : base("dbSISPTD")
        {
        }

        public virtual DbSet<Agendamento> Agendamento { get; set; }
        public virtual DbSet<Cid> Cid { get; set; }
        public virtual DbSet<Cidades> Cidades { get; set; }
        public virtual DbSet<Clinica> Clinica { get; set; }
        public virtual DbSet<Processo> Processo { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Especialidade> Especialidade { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Pericia> Pericia { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Requisicao> Requisicao { get; set; }
        public virtual DbSet<Setor> Setor { get; set; }
        public virtual DbSet<User> Usuario { get; set; }
        public virtual DbSet<PessoaRequisicao> PessoaRequisicao { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #region Relacionamento da  Tb Cid
            modelBuilder.Entity<Cid>()
                .HasMany(e => e.ListaDePericia)
                .WithOptional(e => e.Cid)
                .HasForeignKey(e => e.cidId);

            modelBuilder.Entity<Cid>()
               .HasMany(e => e.ListaDeProcessos)
               .WithOptional(e => e.Cid)
               .HasForeignKey(e => e.cidId);
            #endregion

            #region Relacionamento da Tb Clinica
            modelBuilder.Entity<Clinica>()
               .HasMany(e => e.ListadeAgendamento)
               .WithOptional(e => e.Clinica)
               .HasForeignKey(e => e.clinicaId);
            #endregion

            #region Relacionamento da Tb Cidades
            modelBuilder.Entity<Cidades>()
                .HasMany(e => e.Clinica)
                .WithOptional(e => e.Cidades)
                .HasForeignKey(e => e.IdCidade);

            modelBuilder.Entity<Cidades>()
                .HasMany(e => e.Endereco)
                .WithOptional(e => e.Cidade)
                .HasForeignKey(e => e.IdCidade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cidades>()
                .HasMany(e => e.CidadeOrigem)
                .WithRequired(e => e.CidadeOrigem)
                .HasForeignKey(e => e.IdCidadesOrigem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cidades>()
                .HasMany(e => e.CidadeDestino)
                .WithRequired(e => e.CidadeDestino)
                .HasForeignKey(e => e.IdCidadesDestino)
                .WillCascadeOnDelete(false);
            #endregion

            #region Relacionamento da Tb Estado
            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Cidades)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);
            #endregion

            #region Relacionamento da Tb Pessoa

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Agendamento)
                .WithOptional(e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId);

            modelBuilder.Entity<Pessoa>()
               .HasMany(e => e.Usuarios)
               .WithRequired(e => e.Pessoa)
               .HasForeignKey(e => e.pessoaId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pessoa>()
               .HasMany(e => e.PericiaMedico)
               .WithRequired(e => e.Medico)
               .HasForeignKey(e => e.medicoPessoaId);

            modelBuilder.Entity<Pessoa>()
              .HasMany(e => e.ListaDeProcessosMedico)
              .WithOptional(e => e.Medico)
              .HasForeignKey(e => e.medicoId);

            modelBuilder.Entity<Pessoa>()
             .HasMany(e => e.ListaDeProcessosPaciente)
             .WithOptional(e => e.Paciente)
             .HasForeignKey(e => e.pacienteId);

            modelBuilder.Entity<Pessoa>()
              .HasMany(e => e.PessoaRequisicao)
              .WithRequired(e => e.Pessoa)
              .HasForeignKey(e => e.pessoaId);

            modelBuilder.Entity<Pessoa>()
              .HasMany(e => e.RequisicaoComoPaciente)
              .WithRequired(e => e.Paciente)
              .HasForeignKey(e => e.pacienteId);
            #endregion

            #region Relacionamento da Tb Movimentacao



            modelBuilder.Entity<Movimentacao>()
               .HasRequired(e => e.UsuarioEnviou)
               .WithMany(e => e.ListaDeEnvios)
               .HasForeignKey(e => e.usuarioEnviouId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movimentacao>()
               .HasOptional(e => e.UsuarioRecebeu)
               .WithMany(e => e.ListaDeRecebidos)
               .HasForeignKey(e => e.usuarioRecebeuId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Setor>()
               .HasMany(e => e.ListaDeProcessosEnviados)
               .WithRequired(e => e.SetorEnviou)
               .HasForeignKey(e => e.setorEnviouId)
               .WillCascadeOnDelete(false);


            modelBuilder.Entity<Movimentacao>()
              .HasOptional(e => e.SetorRecebeu)
              .WithMany(e => e.ListaDeProcessosRecebidos)
              .HasForeignKey(e => e.setorRecebeuId)
              .WillCascadeOnDelete(false);

            #endregion

            #region Cria relacionamento Pessoa - Requisicao sem Tipo
            //aqui mexi


            //modelBuilder.Entity<Requisicao>()
            //   .HasMany(e => e.PessoaAcompanhante)
            //   .WithMany(e => e.RequisicaoComoAcompanhante)
            //   .Map(e =>
            //   {
            //       e.MapLeftKey("pessoaId");
            //       e.MapRightKey("requisicaoId");
            //       e.ToTable("PessoaRequisicao");
            //   });

            //modelBuilder.Entity<Pessoa>()
            //   .HasMany(e => e.RequisicaoComoAcompanhante)
            //   .WithMany(e => e.PessoaAcompanhante)
            //   .Map(e =>
            //   {
            //       e.MapLeftKey("pessoaId");
            //       e.MapRightKey("requisicaoId");
            //       e.ToTable("PessoaRequisicao");
            //   });

            #endregion

            #region Cria Relacionamento Pessoa - Requisicao com Tipo



            modelBuilder.Entity<Requisicao>()
              .HasMany(e => e.PessoaRequisicao)
              .WithRequired(e => e.Requisicao)
              .HasForeignKey(e => e.requisicaoId)
            .WillCascadeOnDelete(false);





            #endregion
            #region Relacionamento da Tb Especialidade
            modelBuilder.Entity<Especialidade>()
                .HasMany(e => e.Pessoa)
                .WithMany(e => e.Especialidade)
                .Map(e =>
                   {
                       e.MapLeftKey("EspecialidadeId");
                       e.MapRightKey("pessoaId");
                       e.ToTable("PessoaEspecialidade");
                   });

            #endregion
            #region Relacionamentoda Tb Processo

            modelBuilder.Entity<Processo>()
               .HasMany(e => e.ListaDeMovimentacao)
               .WithOptional(e => e.Processo)
               .HasForeignKey(e => e.ProcessoId)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Processo>()
                .HasOptional(e => e.Agendamento)
                .WithRequired(e => e.Processo)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Processo>()
               .HasOptional(e => e.Pericia)
               .WithRequired(e => e.Processo)
               .WillCascadeOnDelete(true);
            #endregion

            #region Relacionamento Tb Setor

            modelBuilder.Entity<Setor>()
               .HasMany(e => e.ListaDeUsuarios)
               .WithRequired(e => e.Setor)
               .HasForeignKey(e => e.setorId);
            #endregion

            #region Relacionamento Tb Usuario

            modelBuilder.Entity<User>()
               .HasMany(e => e.Agendamento)
               .WithOptional(e => e.Usuarios)
               .HasForeignKey(e => e.usuarioId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Requisicao)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.usuarioId);





            #endregion

        }

        public System.Data.Entity.DbSet<SISPTD.Models.Movimentacao> Movimentacaos { get; set; }
    }
}
