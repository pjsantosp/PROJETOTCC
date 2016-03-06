namespace SISPTD.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Validation;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

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
        public virtual DbSet<ItemRequisicao> ItemRequisicao { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Pericia> Pericia { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Requisicao> Requisicao { get; set; }
        public virtual DbSet<Setor> Setor { get; set; }
       
        public virtual DbSet<User> User { get; set; }

    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Medico>().HasRequired(x => x.Pessoa).WithMany().HasForeignKey(x => x.pessoaId).WillCascadeOnDelete(false);



            //modelBuilder.Entity<Cid>()
            //    .Property(e => e.codigoCid)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Cid>()
            //    .Property(e => e.descricao)
            //    .IsUnicode(false);

            modelBuilder.Entity<Cid>()
                .HasMany(e => e.Pericia)
                .WithOptional(e => e.Cid)
                .HasForeignKey(e => e.cidId);

            //modelBuilder.Entity<Cidades>()
            //    .Property(e => e.Cidade)
            //    .IsUnicode(false);

            modelBuilder.Entity<Cidades>()
                .HasMany(e => e.Clinica)
                .WithOptional(e => e.Cidades)
                .HasForeignKey(e => e.IdCidade);

            modelBuilder.Entity<Cidades>()
                .HasMany(e => e.Endereco)
                .WithRequired(e => e.Cidades)
                .HasForeignKey(e => e.IdCidade)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Clinica>()
            //    .Property(e => e.nome_Clinica)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Clinica>()
            //    .Property(e => e.tel_Clinica)
            //    .IsUnicode(false);


            //modelBuilder.Entity<DistribProcesso>()
            //    .Property(e => e.observacoes)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Endereco>()
            //    .Property(e => e.rua)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Endereco>()
            //    .Property(e => e.numero)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Endereco>()
            //    .Property(e => e.cep)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Endereco>()
            //    .Property(e => e.bairro)
            //    .IsFixedLength();

           

            //modelBuilder.Entity<Especialidade>()
            //    .Property(e => e.descricao)
            //    .IsUnicode(false);

            modelBuilder.Entity<Especialidade>()
                .HasMany(e => e.Medico)
                .WithOptional(e => e.Especialidade)
                .HasForeignKey(e => e.especialidadeId);

            //modelBuilder.Entity<Estado>()
            //    .Property(e => e.Estado1)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Estado>()
            //    .Property(e => e.Sigla)
            //    .IsFixedLength()
            //    .IsUnicode(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Cidades)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Medico>()
            //    .Property(e => e.crm_Medico)
            //    .IsUnicode(false);

            modelBuilder.Entity<Medico>()
                .HasMany(e => e.Pericia)
                .WithRequired(e => e.Medico)
                .HasForeignKey(e => e.medicoId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Pericia>()
            //    .Property(e => e.descricao)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pericia>()
            //    .HasMany(e => e.DistribProcesso)
            //    .WithOptional(e => e.Pericia)
            //    .HasForeignKey(e => e.periciaId);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.cpf)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.cns)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.rg)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.orgaoemissor)
            //    .IsUnicode(false);

           



            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.nome)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.email)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.nome_Mae)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.nome_Pai)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.tel)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Pessoa>()
            //    .Property(e => e.cel)
            //    .IsUnicode(false);

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
                .HasMany(e => e.Endereco)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId);
                

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.ItemRequisicao)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Medico)
                .WithRequired
                (e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Pericia)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Pessoa1)
                .WithOptional(e => e.Pessoa2)
                .HasForeignKey(e => e.pessoaPai);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.pessoaId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Pericia>()
            //    .HasMany(e => e.DistribProcesso)
            //    .WithOptional(e => e.Pericia)
            //    .HasForeignKey(e => e.periciaId);

            modelBuilder.Entity<Requisicao>()
                .HasMany(e => e.ItemRequisicao)
                .WithOptional(e => e.Requisicao)
                .HasForeignKey(e => e.requisicaoId);

            //modelBuilder.Entity<Setor>()
            //    .Property(e => e.descricao)
            //    .IsUnicode(false);

            modelBuilder.Entity<Setor>()
                .HasMany(e => e.DistribProcesso)
                .WithRequired(e => e.SetorOrigem)
                .HasForeignKey(e => e.SetorOrigemId);

            modelBuilder.Entity<Setor>()
                .HasMany(e => e.DistribProcesso1)
                .WithRequired(e => e.SetorDestino)
                .HasForeignKey(e => e.SetorDestinoId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .Property(e => e.login)
            //    .IsUnicode(false);

            //modelBuilder.Entity<User>()
            //    .Property(e => e.senha)
            //    .IsUnicode(false);

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
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.usuarioId);
        }
    }
}
