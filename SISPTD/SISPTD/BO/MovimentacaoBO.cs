﻿using SISPTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PagedList;

namespace SISPTD.BO
{
    public class MovimentacaoBO : CrudComumEntity<Movimentacao, long>
    {
        public MovimentacaoBO(DbContext contexto) : base(contexto)
        {
            
        }
        private SetorBO setorBO = new SetorBO(new dbSISPTD());

        public override void Inserir(Movimentacao entidade)
        {
            base.Inserir(entidade); 
        }
        public override void Alterar(Movimentacao entitade)
        {
            base.Alterar(entitade);
        }
       /// <summary>
       /// Metodo que busca as movimentações dos processos
       /// </summary>
       /// <param name="busca"></param>
       /// <param name="pagina"></param>
       /// <param name="tamanhoPagina"></param>
       /// <returns>lista de Movimentação</returns>
        public IEnumerable<Movimentacao>ObterMovimentacao(string busca, int? pagina, int tamanhoPagina)
        {
            try
            {
                IEnumerable<Movimentacao> listaDeMovimentacao = _contexto.Set<Movimentacao>()
               .Include(m => m.Processo)
               .Include(m => m.SetorEnviou)
               .Include(m => m.SetorRecebeu)
               .Include(m => m.UsuarioEnviou)
               .Include(m => m.UsuarioRecebeu)
               .Where(b=> b.Processo.Paciente.cpf.Contains(busca) || b.Processo.Paciente.cns.Contains(busca) && b.Processo.Paciente.tipo == 0);

                return listaDeMovimentacao.OrderByDescending(m=> m.movimentacaoId).ToPagedList(pagina.Value, tamanhoPagina);

            }
            catch (Exception e)
            {

                throw new Exception("Ops! Não foi possível listar a Movimentação", e);
            }

           


        }
        public IEnumerable<Movimentacao> ObterPericias(int? pagina, int tamanhoPagina)
        {
            //var ListaDePericias = from s in _contexto.Set<Setor>()
            //                      join m in _contexto.Set<Movimentacao>() on s.setorId equals m.setorRecebeuId
            //                      join p in _contexto.Set<Processo>() on m.ProcessoId equals p.processoId
            //                      select new
            //                      {
            //                          _processo = p.processoId


            //                      };
            IEnumerable<Movimentacao> listaDePericia = _contexto.Set<Movimentacao>().Where(m => m.SetorRecebeu.descricao == "Pericia")
                .Include(p => p.Processo);
            return listaDePericia.OrderByDescending(m=> m.dtEnvio).ToPagedList(pagina.Value, tamanhoPagina);


        }
        public IEnumerable<Movimentacao> ObterAgendamento(int? pagina, int tamanhoPagina)
        {
            //var ListaDePericias = from s in _contexto.Set<Setor>()
            //                      join m in _contexto.Set<Movimentacao>() on s.setorId equals m.setorRecebeuId
            //                      join p in _contexto.Set<Processo>() on m.ProcessoId equals p.processoId
            //                      select new
            //                      {
            //                          _processo = p.processoId


            //                      };
            IEnumerable<Movimentacao> listaDePericia = _contexto.Set<Movimentacao>().Where(m => m.setorAtual == "Agendamento" && m.SetorEnviou.descricao != "Agendamento")
                .Include(p => p.Processo);
            return listaDePericia.OrderByDescending(m => m.dtEnvio).ToPagedList(pagina.Value, tamanhoPagina);


        }
        public IEnumerable<Movimentacao> ObterDetalheDoProcesso(int processoId)
        {
            try
            {
                IEnumerable<Movimentacao> listaDeMovimentacao = _contexto.Set<Movimentacao>()
                    .Include(m => m.SetorEnviou)
                    .Include(m => m.SetorRecebeu)
                    .Include(m => m.UsuarioRecebeu)
                    .Include(m => m.UsuarioEnviou)
                    .Include(m => m.Processo).Where(p => p.ProcessoId.Value == processoId).ToList();
                return listaDeMovimentacao;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro na Busca do Processo", ex);
            }

        }
        public void AlteraSetorAtual(int movId, long setorRecebeuId)
        {
            try
            {

                Movimentacao objMovimentacao = _contexto.Set<Movimentacao>()
                   .Include(m => m.Processo)
               .Include(m => m.SetorEnviou)
               .Include(m => m.SetorRecebeu)
               .Include(m => m.UsuarioEnviou)
               .Include(m => m.UsuarioRecebeu).FirstOrDefault(m => m.movimentacaoId == movId);
                objMovimentacao.setorAtual = setorBO.SelecionarPorId(setorRecebeuId).descricao;


                if (objMovimentacao != null)
                {
                    base.Alterar(objMovimentacao);

                }




            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}