using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sptd.Web.Models;
using Sptd.Web.Repositories;


namespace Sptd.Web.Controllers
{
    public class EnderecoController : Controller
    {
        private DbSPTD db = new DbSPTD();
        private EstadoRepository repositoryEstado = new EstadoRepository();
        private CidadeRepository repositoryCidade = new CidadeRepository();
        private PessoaRepository repPessoa = new PessoaRepository();
        

        // GET: Endereco
        public ActionResult Index(long pessoaId)
        {
            var lista = repPessoa.ObterPessoa(pessoaId).pessoaId;
            ViewBag.PessoaId = pessoaId;
           
            //var endereco = db.Endereco.Include(e => e.Cidades).Include(e=> e.Cidades.IdEstado);
            //var pessoa = db.Endereco.Include(e => e.Pessoa);
            return View();
        }

        // GET: Endereco/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.Endereco.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // GET: Endereco/Create
        public ActionResult Create(long pessoaId)
        {
            Endereco endPessoa = new Endereco();
            endPessoa.fk_PessoaId = pessoaId;
            
            long ufId = repositoryEstado.EstadoPadrao();
            IList<Estado> estado = repositoryEstado.ObterEstados();
            IList<Cidades> cidades = repositoryCidade.CidadesRO(ufId);
            
            ViewBag.Cidade = new SelectList(cidades, "IdCidade", "Cidade");
            ViewBag.Uf = new SelectList(estado, "IdEstado", "Sigla");
            return View(endPessoa);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Endereco endereco)
        {//[Bind(Include = "enderecoId,fk_CidadeId,fk_Pessoa,rua,numero,cep,bairro")] 
            //PopularComboCidade(endereco.Cidades.IdCidade);
            endereco.Cidades = null;
            if (ModelState.IsValid)
            {
                
                db.Endereco.Add(endereco);
                db.SaveChanges();
                return View();
            }
          
           
            ViewBag.fk_CidadeId = new SelectList(db.Cidades, "IdCidade", "Cidade", endereco.fk_CidadeId);
            ViewBag.Uf = new SelectList(db.Estado, "IdEstado", "Sigla", endereco.Cidades.IdEstado);
            return View(endereco);
        }

        // GET: Endereco/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.Endereco.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_CidadeId = new SelectList(db.Cidades, "IdCidade", "Cidade", endereco.fk_CidadeId);
            return View(endereco);
        }

        // POST: Endereco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "enderecoId,fk_CidadeId,fk_Pessoa,rua,numero,cep,bairro")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endereco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_CidadeId = new SelectList(db.Cidades, "IdCidade", "Cidade", endereco.fk_CidadeId);
            return View(endereco);
        }

        // GET: Endereco/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = db.Endereco.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }
        public void PopularComboCidade(object cidadeSelecionada = null)
        {
            long ufId = repositoryEstado.EstadoPadrao();
            List<Cidades> cidades = repositoryCidade.CidadesRO(ufId);
            ViewBag.Categorias = new SelectList(cidades, "IdCidade", "Cidade", cidadeSelecionada);
        }

        // POST: Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Endereco endereco = db.Endereco.Find(id);
            db.Endereco.Remove(endereco);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    
}
