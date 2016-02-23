using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sptd.Web.Repositories;
using Sptd.Web.Models;

namespace Sptd.Web.Controllers
{
    public class PessoaEnderecoController : Controller
    {
        PessoaRepository repository = new PessoaRepository();
        CidadeRepository repositoryCidade = new CidadeRepository();
        EstadoRepository repositoryEstado = new EstadoRepository();
        // GET: PessoaEndereco
        public ActionResult Create(long PessoaId )
        {
            ViewBag.Pessoa = repository.ObterPessoa(PessoaId);
           long ufPadrao = repositoryEstado.EstadoPadrao();
           ViewBag.Estado = new SelectList(repositoryEstado.ObterEstados(),"IdEstado","Estado");
           ViewBag.Cidade = new SelectList(repositoryCidade.CidadesRO(ufPadrao), "IdCidade", "Cidade");
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}