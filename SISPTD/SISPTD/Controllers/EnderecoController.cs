using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SISPTD.Models;

namespace SISPTD.Controllers
{
    public class EnderecoController : Controller
    {
        private dbSISPTD db = new dbSISPTD();

        // GET: Endereco
        public ActionResult Index()
        {
            var endereco = db.Endereco.Include(e => e.Cidades).Include(e => e.Pessoa);
            return View(endereco.ToList());
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
        public ActionResult Create(long? pessoaId)
        {
            Endereco endPessoa = new Endereco();
            endPessoa.pessoaId = pessoaId;
            var listaUf = db.Estado.Include(c => c.Cidades).ToList();
            long ufPadrao = db.Estado.Where(e => e.Sigla == "RO").First().IdEstado;

            IList<Estado> estado = db.Estado.ToList();

            ViewBag.Uf = new SelectList(estado, "IdEstado", "Sigla");

            ViewBag.Cidade = new SelectList(db.Cidades.Where(c=> c.IdEstado==ufPadrao), "IdCidade", "Cidade", ufPadrao);
            ViewBag.pessoaId = db.Pessoa.Where(p => p.pessoaId == pessoaId).First().nome;
            return View(endPessoa);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Endereco endereco)
        {
            
            if (ModelState.IsValid)
            {
                db.Endereco.Add(endereco);
                db.SaveChanges();

                //var anoNascimento = endereco.Pessoa.dt_Nascimento.Year;
                //DateTime dataAtual = DateTime.Now;
                //int anoAtual = dataAtual.Year;
                //int idade = anoAtual - anoNascimento;

               

                return RedirectToAction("CreateAcompanhate", "Pessoa", new { acompanhate = endereco.pessoaId });
            }

            ViewBag.IdCidade = new SelectList(db.Cidades, "IdCidade", "Cidade", endereco.IdCidade);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", endereco.pessoaId);
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
            ViewBag.IdCidade = new SelectList(db.Cidades, "IdCidade", "Cidade", endereco.IdCidade);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", endereco.pessoaId);
            return View(endereco);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "enderecoId,IdCidade,pessoaId,rua,numero,cep,bairro")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endereco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCidade = new SelectList(db.Cidades, "IdCidade", "Cidade", endereco.IdCidade);
            ViewBag.pessoaId = new SelectList(db.Pessoa, "pessoaId", "cpf", endereco.pessoaId);
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
        public void PovoarComboCidade(int idUF)
        {
            var listaCidade = db.Cidades.Where(c => c.IdEstado == idUF).FirstOrDefault().Cidade;
            ViewBag.Cidade = new SelectList(listaCidade, "IdCidade", "Cidade", idUF);
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
