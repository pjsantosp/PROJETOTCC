using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SISPTD.BO;
using SISPTD.Models;

namespace SISPTD.Controllers
{

    public class CidadeController : Controller
    {

        private CidadeBO cidadeBO = new CidadeBO(new dbSISPTD());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PesquisaCidade(int id)
        {
            var cidades = cidadeBO.Selecionar().Where(m => m.IdEstado == id).Select(c => new
            {
                value = c.IdCidade,
                text = c.Cidade
            }).ToList();
            return Json(cidades, JsonRequestBehavior.AllowGet);
        }
    }

}