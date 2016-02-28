using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sptd.Web.Models;

namespace Sptd.Web.Controllers
{
    public class EndController : Controller
    {
        DbSPTD db = new DbSPTD();
        // GET: End
        public ActionResult ListarItens(long id)
        {
            var lista = db.Pessoa.Where(p => p.pessoaId == id);
            ViewBag.PessoaId = id;
            return PartialView(lista);
        }
    }
}