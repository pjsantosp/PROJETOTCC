using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SISPTD.BO;
using SISPTD.Models;

namespace SISPTD.Controllers
{
    public class CidController : Controller
    {
        private CidBO cidBO = new CidBO(new dbSISPTD());
        public ActionResult PesquisaCid(string query)
        {

            var cids = cidBO.Selecionar().Where(x => x.descricao.Contains(query) || x.codigoCid.Contains(query) ).Select(x => new 
            {
                text = x.codigoCid + " - " + x.descricao,
                value = x.cidId,
            });

            return Json(cids, JsonRequestBehavior.AllowGet);
        }
    }
}