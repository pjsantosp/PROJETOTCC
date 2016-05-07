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
        public virtual DbSet<DistribProcesso> DistribProcesso { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Especialidade> Especialidade { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Pericia> Pericia { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Requisicao> Requisicao { get; set; }
        public virtual DbSet<Setor> Setor { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

           
            modelBuilder.Entity<Cid>()
                .HasMany(e => e.Pericia)
                .WithOptional(e => e.Cid)
                .HasForeignKey(e => e.cidId);

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

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Cidades)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Agendamento)
                .WithOptional(e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.DistribProcesso)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId)
                .WillCascadeOnDelete(false);
           
            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.PericiaPaciente)
                .WithOptional(e => e.Paciente)
                .HasForeignKey(e => e.pacientePessoaId)
                .WillCascadeOnDelete(false);

         

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requisicao>()
               .HasMany(e => e.PessoaAcompanhante)
               .WithMany(e => e.RequisicaoComoAcompanhante)
               .Map(e =>
               {
                   e.MapLeftKey("requisicaoId");
                   e.MapRightKey("pessoaId");
                   e.ToTable("PessoaRequisicao");
               });


            modelBuilder.Entity<Especialidade>()
                .HasMany(e => e.Pessoa)
                .WithMany(e => e.Especialidade)
                .Map(e =>
                   {
                       e.MapLeftKey("EspecialidadeId");
                       e.MapRightKey("pessoaId");
                       e.ToTable("PessoaEspecialidade");
                   });

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.PericiaMedico)
                .WithMany(e => e.Medico)
                .Map(e =>
                  {
                      e.MapLeftKey("pessoaId");
                      e.MapRightKey("periciaId");
                      e.ToTable("medicoPericia");
                  });

            modelBuilder.Entity<Setor>()
                .HasMany(e => e.DistribProcesso)
                .WithRequired(e => e.SetorOrigem)
                .HasForeignKey(e => e.SetorOrigemId);

            modelBuilder.Entity<Setor>()
                .HasMany(e => e.DistribProcesso1)
                .WithRequired(e => e.SetorDestino)
                .HasForeignKey(e => e.SetorDestinoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Agendamento)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.usuarioId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.DistribProcesso)
                .WithRequired(e => e.UserEnviou)
                .HasForeignKey(e => e.usuarioEnviouId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.DistribProcesso1)
                .WithOptional(e => e.UserRecebeu)
                .HasForeignKey(e => e.usuarioRecebeuId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Requisicao)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.usuarioId);
        }
    }
}
