using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SISPTD.Models;
using SISPTD.BO;
using PagedList;

namespace SISPTD.Controllers
{
    public class SetorController : Controller
    {
        private SetorBO setorBO = new SetorBO(new dbSISPTD());

        public ActionResult BuscaSetor(string query)
        {
            query = query.ToUpper();

            var setor = setorBO.Selecionar().Where(x => x.descricao.Contains(query) || x.abreviacao.Contains(query)).Select(x => new
            {
                text = x.abreviacao + " - " + x.descricao,
                value = x.setorId,
            });

            return Json(setor, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(int? pagina)
        {
            int nPorPagina = 5;
            int tamPagina = pagina ?? 1;
            return View(setorBO.Selecionar().OrderBy(s=> s.descricao).ToPagedList(tamPagina, nPorPagina ));
        }

 
        public ActionResult Details(long? id)
        {

            return View(setorBO.SelecionarPorId(id.Value));
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Setor setor)
        {
            try
            {
                setor.descricao = setor.descricao.ToUpper();
                setor.abreviacao = setor.abreviacao.ToUpper();
                if (ModelState.IsValid)
                {
                    setorBO.Inserir(setor);
                    TempData["Sucesso"] = "Setor Gravado Com Sucesso!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Ops! " + ex.Message;
            }

            return View(setor);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setor setor = setorBO.SelecionarPorId(id.Value);
            if (setor == null)
            {
                return HttpNotFound();
            }
            return View(setor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Setor setor)
        {
            try
            {
                setor.abreviacao = setor.abreviacao.ToUpper();
                setor.descricao = setor.descricao.ToUpper();
                if (ModelState.IsValid)
                {
                    setorBO.Alterar(setor);
                    TempData["Sucesso"] = "Setor Alterador com Sucesso !";
                }
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {

                TempData["Erro"] = "Ops ! " + e.Message;
            }
            return View(setor);
        }
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setor setor = setorBO.SelecionarPorId(id.Value);
            if (setor == null)
            {
                return HttpNotFound();
            }
            return View(setor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            setorBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }


    }
}
