using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SISPTD.Models;
using SISPTD.BO;

namespace SISPTD.Controllers
{
    public class MovimentacaoController : Controller
    {
        private dbSISPTD db = new dbSISPTD();
        private MovimentacaoBO movimentacaoBO = new MovimentacaoBO(new dbSISPTD());
        private UserBO usuarioBO = new UserBO(new dbSISPTD());
        private ProcessoBO processoBO = new ProcessoBO(new dbSISPTD());

        // GET: Movimentacao
        public ActionResult Index(int? pagina, string buscar="")
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            //var movimentacaos = db.Movimentacaos.Include(m => m.Processo).Include(m => m.SetorEnviou).Include(m => m.SetorRecebeu).Include(m => m.UsuarioEnviou).Include(m => m.UsuarioRecebeu);
            return View(movimentacaoBO.ObterMovimentacao(buscar ,numeroPagina, tamanhoPagina));
        }

        public ActionResult ProcessoEmPericia()
        {
            return PartialView(movimentacaoBO.ObterPericias());
        }

        // GET: Movimentacao/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacaos.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // GET: Movimentacao/Create
        public ActionResult Create()
        {
            ViewBag.processoId = 0;
            ViewBag.setorEnviouId = new SelectList(db.Setor, "setorId", "descricao");
            ViewBag.setorRecebeuId = new SelectList(db.Setor, "setorId", "descricao");
            ViewBag.usuarioEnviouId = new SelectList(db.Usuario, "usuarioId", "login");
            ViewBag.usuarioRecebeuId = new SelectList(db.Usuario, "usuarioId", "login");
            return View( );
        }

        // POST: Movimentacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Movimentacao movimentacao)
        {
            //[Bind(Include = "movimentacaoId,usuarioEnviouId,usuarioRecebeuId,setorEnviouId,setorRecebeuId,ProcessoId,dtEnvio,dtRecebimento")]
            try
            {
                var user = usuarioBO.userLogado(User.Identity.Name);
                movimentacao.usuarioEnviouId = user.usuarioId;
                movimentacao.dtEnvio = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Movimentacaos.Add(movimentacao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
           

            ViewBag.ProcessoId = new SelectList(db.Processo, "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioRecebeuId);
            return View(movimentacao);
        }

        // GET: Movimentacao/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacaos.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProcessoId = new SelectList(db.Processo, "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioRecebeuId);
            return View(movimentacao);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "movimentacaoId,usuarioEnviouId,usuarioRecebeuId,setorEnviouId,setorRecebeuId,ProcessoId,dtEnvio,dtRecebimento")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimentacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProcessoId = new SelectList(db.Processo, "processoId", "Procedimento", movimentacao.ProcessoId);
            ViewBag.setorEnviouId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorEnviouId);
            ViewBag.setorRecebeuId = new SelectList(db.Setor, "setorId", "descricao", movimentacao.setorRecebeuId);
            ViewBag.usuarioEnviouId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioEnviouId);
            ViewBag.usuarioRecebeuId = new SelectList(db.Usuario, "usuarioId", "login", movimentacao.usuarioRecebeuId);
            return View(movimentacao);
        }

        // GET: Movimentacao/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacaos.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Movimentacao movimentacao = db.Movimentacaos.Find(id);
            db.Movimentacaos.Remove(movimentacao);
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
