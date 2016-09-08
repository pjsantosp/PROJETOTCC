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
        public ActionResult PesquisaCidade(string query)
        {



            //var cids = cidBO.Selecionar().Where(x => x.descricao.Contains(query) || x.codigoCid.Contains(query)).Select(x => new
            //{
            //    text = x.codigoCid + " - " + x.descricao,
            //    value = x.cidId,
            //});

            var cidade = cidadeBO.Selecionar().Where(x => x.Cidade.Contains(query)).Select(x => new
            {
                text = x.Cidade,
                value = x.IdCidade,
            });

            return Json(cidade, JsonRequestBehavior.AllowGet);
        }
    }

}