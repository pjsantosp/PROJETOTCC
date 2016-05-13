using SISPTD.BO;
using SISPTD.Models;
using SISPTD.Ultis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SISPTD.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
        private CidadeBO cidadeBO = new CidadeBO(new dbSISPTD());


        public ActionResult Index(string busca = "")
        {
            return View(pessoaBO.ObterPessoa(busca));
        }
        public ActionResult Pesquisar(string cpf)
        {
            cpf = Util.RemoverMascara(cpf);
            var pessoa = pessoaBO.Selecionar().Where(p => p.cpf == cpf).FirstOrDefault();
            if (pessoa == null)
            {
                return Json(new { Nome = "", Id = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Nome = pessoa.nome, Id = pessoa.pessoaId }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult PesquisarMedico(string cpf)
        {
            cpf = Util.RemoverMascara(cpf);
            var pessoa = pessoaBO.Selecionar().Where(p => p.cpf == cpf && p.tipo==2).FirstOrDefault();
            if (pessoa == null)
            {
                return Json(new { Nome = "", Id = 0, Cpf = "", Cns = "", Tel = "", Crm = ""}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Nome = pessoa.nome, Id = pessoa.pessoaId, Cpf = pessoa.cpf, Tel= pessoa.tel, Crm = pessoa.crm, Cns = pessoa.cns }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Details(long? id)
        {
            ViewBag.acompanhante = pessoaBO.Selecionar().Where(a => a.pessoaPai == id.Value).Take(5);

            return View(pessoaBO.SelecionarPorId(id.Value));
        }

        //[Authorize(Roles = "Funcionario, Gerente")]
        public ActionResult Create()
        {
            ViewBag.cidade = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Funcionario, Gerente")]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pessoa.cpf = Util.RemoverMascara(pessoa.cpf);
                    pessoa.dt_Cadastro = DateTime.Now;
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Inserir(pessoa);
                    TempData["Sucesso"] = "Cadastrado Realizado com Sucesso!";

                    pessoaBO.SelecionarPorId(pessoa.pessoaId);
                    return RedirectToAction("Edit", new { id = pessoa.pessoaId });
                }
            }
            catch (Exception e)
            {
                TempData["Erro"] = "Ops! " + e.Message;
            }

            ViewBag.cidade = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");
            ViewBag.pessoaPai = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", pessoa.pessoaPai);
            return View(pessoa);
        }
        public ActionResult CreateAcompanhante(long? pessoaPai)
        {
            try
            {
                if (pessoaPai != null)
                {
                    if (ModelState.IsValid)
                    {
                        Pessoa acompanhante = new Pessoa();
                        acompanhante.pessoaPai = pessoaPai;
                        ViewBag.pessoaId = pessoaPai;
                        return View(acompanhante);
                    }
                }
            }
            catch (Exception)
            {
                TempData["Erro"] = "Ops! erro durante o Cadastro do Acompanhante!";
            }


            ViewBag.pessoaId = 0;

            return View();


        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateAcompanhante(Pessoa pessoa, long? pessoaPai)
        {
            try
            {
                pessoa.cpf = Ultis.Util.RemoverMascara(pessoa.cpf);
                pessoa.pessoaPai = pessoaPai;

                if (ModelState.IsValid)
                {
                    pessoa.dt_Cadastro = DateTime.Now;
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Inserir(pessoa);
                    TempData["Sucesso"] = "Acompanhante Cadastrado com Sucesso";
                    return RedirectToAction("Edit", new { id = pessoa.pessoaPai });
                }
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Erro durante o Cadastro de Acompanhante " + ex.Message;
            }


            return View();
        }
        public ActionResult CreateMedico()
        {

            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateMedico(Pessoa pessoa)
        {

            return View();
        }

        // GET: Pessoa/Edit/5
        //[Authorize(Roles = "Funcionario, Gerente")]
        public ActionResult Edit(long? id)
        {
            ViewBag.acompanhante = pessoaBO.Selecionar().Where(a => a.pessoaPai == id.Value).Count();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = pessoaBO.SelecionarPorId(id.Value);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        [HttpPost]
        //[Authorize(Roles = "Funcionario, Gerente")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pessoa pessoa)
        {
            try
            {
                pessoa.cpf =  Ultis.Util.RemoverMascara(pessoa.cpf);
                if (ModelState.IsValid)
                {
                    pessoaBO.Alterar(pessoa);
                    TempData["Sucesso"] = "Alteração feita com Sucesso!";
                }
                return View(pessoa);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ops! Ocorreu um erro!" + ex.Message;
            }
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(long? id)
        {
            Pessoa pessoa = pessoaBO.SelecionarPorId(id.Value);
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            pessoaBO.ExcluirPorId(id);

            return RedirectToAction("Index");
        }


    }
}
