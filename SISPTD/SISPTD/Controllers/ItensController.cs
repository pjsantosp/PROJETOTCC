using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SISPTD.BO;
using SISPTD.Models;

namespace SISPTD.Controllers
{
    public class ItensController : Controller
    {
       private ItemRequisicaoBO itemRequisicaoBO = new ItemRequisicaoBO(new dbSISPTD());
        public ActionResult ListarItens(int? id)
        {
            var listaItem = itemRequisicaoBO.Selecionar().Where(i => i.requisicaoId==id.Value);
            ViewBag.requisicao = id;
            ItemRequisicao item = new ItemRequisicao();
            item.requisicaoId = id;
            return PartialView(listaItem);
        }
        public ActionResult GravarItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GravarItem(ItemRequisicao item)
        {

            return View();
        }
    }
}