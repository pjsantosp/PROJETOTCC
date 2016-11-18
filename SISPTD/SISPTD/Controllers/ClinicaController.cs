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
    public class ClinicaController : Controller
    {
        private ClinicaBO clinicaBO = new ClinicaBO(new dbSISPTD());
        private CidadeBO cidadeBO = new CidadeBO(new dbSISPTD());

        public ActionResult Index(int? pagina)
        {
            int nPorPagina = 10;
            int numPagina = pagina ?? 1; 
            //ViewBag.IdCidade = new SelectList(cidadeBO.ObterCidades(), "IdCidade", "Cidade");
            return View(clinicaBO.Selecionar().OrderBy(x=> x.nome_Clinica).ToPagedList(numPagina, nPorPagina));
        }
        public ActionResult BuscaClinica(string query = "")
        {
            query = query.ToUpper();
            var clinica = clinicaBO.Selecionar().Where(c => c.nome_Clinica.Contains(query)).Select(c => new
            {
                text = c.nome_Clinica + "-" + c.tel_Clinica,
                value = c.clinicaId,
            });

            return Json(clinica, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = clinicaBO.SelecionarPorId(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        public ActionResult Create()
        {
           
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clinica clinica)
        {
            try
            {
                clinica.nome_Clinica = clinica.nome_Clinica.ToUpper();
                if (ModelState.IsValid)
                {
                    clinicaBO.Inserir(clinica);
                    TempData["Sucesso"] = "Clinica Cadastrada com Sucesso!";
                    return RedirectToAction("Index");
                }

                return View(clinica);
            }
            catch (Exception ex)
            {
                return View(clinica);
                throw new Exception("Ops ! Algo está errado no Cadastro de Clinica", ex);
            }

           
           
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = clinicaBO.SelecionarPorId(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCidade = new SelectList(cidadeBO.ObterCidades(), "IdCidade", "Cidade", clinica.IdCidade);
            return View(clinica);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Clinica clinica)
        {
            try
            {
                clinica.nome_Clinica = clinica.nome_Clinica.ToUpper();
                if (ModelState.IsValid)
                {
                    clinicaBO.Alterar(clinica);
                    TempData["Sucesso"] = "Clinica Editada com sucesso!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
           
            ViewBag.IdCidade = new SelectList(cidadeBO.ObterCidades(), "IdCidade", "Cidade", clinica.IdCidade);
            return View(clinica);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = clinicaBO.SelecionarPorId(id.Value);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            clinicaBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }

       
    }
}
