using SISPTD.BO;
using SISPTD.Models;
using SISPTD.Ultis;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SISPTD.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaBO pessoaBO = new PessoaBO(new dbSISPTD());
        private CidadeBO cidadeBO = new CidadeBO(new dbSISPTD());

        public ActionResult Index(int? pagina, string busca = "")
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            busca = Util.RemoverMascara(busca);
            return View(pessoaBO.ObterPessoa(busca, numeroPagina, tamanhoPagina));
        }
        /// <summary>
        /// Busca um Paciente quando passado um CPF
        /// </summary>
        /// <param name="cpf">string cpf</param>
        /// <returns>Json</returns>
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
                return Json(new { Nome = pessoa.nome, Id = pessoa.pessoaId, Cpf = pessoa.cpf }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PesquisarMedico(string cpf)
        {
            cpf = Util.RemoverMascara(cpf);
            var pessoa = pessoaBO.Selecionar().Where(p => p.cpf == cpf && p.tipo == 2).FirstOrDefault();
            if (pessoa == null)
            {
                return Json(new { Nome = "", Id = 0, Cpf = "", Cns = "", Tel = "", Cel = "", Crm = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Nome = pessoa.nome, Id = pessoa.pessoaId, Cpf = pessoa.cpf, Tel = pessoa.tel, Cel = pessoa.cel, Crm = pessoa.crm, Cns = pessoa.cns }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(long? id)
        {
            ViewBag.acompanhante = pessoaBO.Selecionar().Where(a => a.acompanhanteId == id.Value).Take(5);

            return View(pessoaBO.SelecionarPorId(id.Value));
        }
        [Authorize(Roles = "Funcionario, Gerente")]
        public ActionResult Create()
        {
            ViewBag.cidade = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");
            return View();
        }
        /// <summary>
        /// Action que cria uma pessoa do tipo Paciente e retorna para Action Index 
        /// </summary>
        /// <param name="pessoa">pessoa</param>
        /// <returns>Action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Gerente")]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pessoa.tipo = (int)TipoPessoa.Paciente;
                    pessoa.cpf = Util.RemoverMascara(pessoa.cpf);
                    pessoa.dt_Cadastro = DateTime.Now;
                    if (pessoaBO.CalculoIdade(pessoa))
                    {
                        pessoaBO.Inserir(pessoa);
                        TempData["Erro"] = "O Cadastro do Paciente requer um Acompanhante!";
                        return RedirectToAction("CreateAcompanhante", new { acompanhanteId = pessoa.pessoaId });

                    }
                    pessoaBO.Inserir(pessoa);
                    TempData["Sucesso"] = "Cadastrado Realizado com Sucesso!";
                    pessoaBO.SelecionarPorId(pessoa.pessoaId);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["Erro"] = "Ops! " + e.Message;
            }

            ViewBag.cidade = new SelectList(cidadeBO.Selecionar(), "IdCidade", "Cidade");
            ViewBag.pessoaPai = new SelectList(pessoaBO.Selecionar(), "pessoaId", "cpf", pessoa.acompanhanteId);
            return View(pessoa);
        }
        public ActionResult CreateAcompanhante(long? acompanhanteId)
        {
            try
            {
                if (acompanhanteId != null)
                {
                    if (ModelState.IsValid)
                    {
                        Pessoa acompanhante = new Pessoa();
                        acompanhante.acompanhanteId = acompanhanteId;
                        ViewBag.pessoaId = acompanhanteId;
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
        /// <summary>
        /// Action Responsavel por criar Uma Pessoa do Tipo Acompanhante Retornando
        /// </summary>
        /// <param name="pessoa">pessoa</param>
        /// <param name="acompanhanteId">id do acompanhante</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateAcompanhante(Pessoa pessoa, long? acompanhanteId)
        {
            try
            {
                pessoa.cpf = Ultis.Util.RemoverMascara(pessoa.cpf);
                pessoa.acompanhanteId = acompanhanteId;

                if (ModelState.IsValid)
                {
                    pessoa.tipo = (int)TipoPessoa.Acompanhante;
                    pessoa.dt_Cadastro = DateTime.Now;
                    if (pessoaBO.CalculoIdade(pessoa))
                    {
                        TempData["Erro"] = "Ops! O acompanhate não deve ser menor de 18 ou maior de 60 anos !";
                        return View(pessoa);
                    }
                    pessoaBO.Inserir(pessoa);
                    TempData["Sucesso"] = "Acompanhante Cadastrado com Sucesso !";
                    return RedirectToAction("Edit", new { id = pessoa.acompanhanteId });
                }
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Erro durante o Cadastro de Acompanhante " + ex.Message;
            }

            return View();
        }
        #region Crud Funcionario
        [Authorize(Roles = "Gerente, Administrador")]
        public ActionResult ListaDeFuncionario(int? pagina, string buscaFuncionario = "")
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            buscaFuncionario = Util.RemoverMascara(buscaFuncionario);

            return View(pessoaBO.ObterFuncionarios(buscaFuncionario, numeroPagina, tamanhoPagina));
        }
        [Authorize(Roles = "Gerente, Administrador")]
        public ActionResult CreateFuncionario()
        {
            return View();
        }
        /// <summary>
        /// Action recebe um objPessoa para criar Pessoa do tipo Funcionario
        /// </summary>
        /// <param name="funcionario"> pessoa</param>
        /// <returns>Action p controle Usuario</returns>
        [Authorize(Roles = "Gerente, Administrador")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateFuncionario(Pessoa funcionario)
        {
            try
            {
                funcionario.cpf = Ultis.Util.RemoverMascara(funcionario.cpf);
                if (ModelState.IsValid)
                {
                    funcionario.tipo = (int)TipoPessoa.Funcionario;
                    funcionario.dt_Cadastro = DateTime.Now;
                    pessoaBO.Inserir(funcionario);
                    TempData["Sucesso"] = "Cadastro Realizado com Sucesso !";
                }
                return RedirectToAction("Index", "User");
            }
            catch (Exception e)
            {

                TempData["Erro"] = "Ops !" + e.Message;
            }
            return View(funcionario);
        }
        /// <summary>
        /// Action de Edição de um Funcionario
        /// </summary>
        /// <param name="id"> id </param>
        /// <returns>Action</returns>
        [Authorize(Roles = "Gerente, Administrador")]
        public ActionResult EditFuncionario(long? id)
        {
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
        [Authorize(Roles = "Gerente, Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult EditFuncionario(Pessoa pessoa)
        {
            try
            {
                pessoa.cns = Ultis.Util.RemoverMascara(pessoa.cns);
                pessoa.cpf = Ultis.Util.RemoverMascara(pessoa.cpf);
                if (ModelState.IsValid)
                {
                    pessoa.tipo = (int)TipoPessoa.Funcionario;
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Alterar(pessoa);
                    TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                }
                return RedirectToAction("ListaDeMedico");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ops! Ocorreu um erro!" + ex.Message;
            }
            return View(pessoa);
        }
        #endregion
        #region Crud Medico
        /// <summary>
        /// Action que retorna a lista de Medicos cadastro
        /// </summary>
        /// <param name="pagina">int</param>
        /// <param name="buscaMedico">CPF</param>
        /// <returns>Lista de Medicos</returns>
        public ActionResult ListaDeMedico(int? pagina, string buscaMedico = "")
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            buscaMedico = Util.RemoverMascara(buscaMedico);

            return View(pessoaBO.ObterMedico(buscaMedico, numeroPagina, tamanhoPagina));
        }

        public ActionResult CreateMedico()
        {

            return View();
        }
        /// <summary>
        /// Action que cria uma pessoa do tipo Medico
        /// </summary>
        /// <param name="pessoa">pessoa</param>
        /// <returns>View tipo Medico</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateMedico(Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pessoa.tipo = (int)TipoPessoa.Medico;
                    pessoa.cpf = Util.RemoverMascara(pessoa.cpf);
                    pessoa.cns = Util.RemoverMascara(pessoa.cns);
                    pessoa.dt_Cadastro = DateTime.Now;
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Inserir(pessoa);
                    TempData["Sucesso"] = "Cadastrado Realizado com Sucesso!";
                    return RedirectToAction("ListaDeMedico");
                }
            }
            catch (Exception e)
            {

                TempData["Erro"] = "Ops! " + e.Message;
            }

            return View(pessoa);
        }
        [Authorize(Roles = "Funcionario, Gerente, Administrador")]
        public ActionResult EditMedico(long? id)
        {
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
        /// <summary>
        /// Action responsável pela edição de um Médico
        /// </summary>
        /// <param name="pessoa">pessoa</param>
        /// <returns>View tipo Medico</returns>
        [HttpPost]
        [Authorize(Roles = "Funcionario, Gerente, Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult EditMedico(Pessoa pessoa)
        {
            try
            {
                pessoa.cns = Ultis.Util.RemoverMascara(pessoa.cns);
                pessoa.cpf = Ultis.Util.RemoverMascara(pessoa.cpf);
                if (ModelState.IsValid)
                {
                    pessoa.tipo = (int)TipoPessoa.Medico;
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Alterar(pessoa);
                    TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                }
                return RedirectToAction("ListaDeMedico");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ops! Ocorreu um erro!" + ex.Message;
            }
            return View(pessoa);
        }
        #endregion
        [Authorize(Roles = "Funcionario, Gerente, Administrador")]
        public ActionResult Edit(long? id)
        {
            ViewBag.acompanhante = pessoaBO.Selecionar().Where(a => a.acompanhanteId == id.Value).Count();
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
        [Authorize(Roles = "Funcionario, Gerente, Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pessoa pessoa)
        {
            try
            {
                pessoa.cns = Ultis.Util.RemoverMascara(pessoa.cns);
                pessoa.cpf = Ultis.Util.RemoverMascara(pessoa.cpf);
                if (ModelState.IsValid)
                {
                    if (pessoa.tipo == 0)
                    {
                        pessoa.tipo = (int)TipoPessoa.Paciente;
                    }
                    else if (pessoa.tipo == 1)
                    {
                        pessoa.tipo = (int)TipoPessoa.Acompanhante;
                    }
                    else if (pessoa.tipo == 2)
                    {
                        pessoa.tipo = (int)TipoPessoa.Medico;
                    }
                    else
                    {
                        pessoa.tipo = (int)TipoPessoa.Funcionario;
                    }
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Alterar(pessoa);
                    TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                }
                return View(pessoa);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ops! Ocorreu um erro!" + ex.Message;
            }
            return View(pessoa);
        }
        public ActionResult Delete(long? id)
        {
            Pessoa pessoa = pessoaBO.SelecionarPorId(id.Value);
            return View(pessoa);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            pessoaBO.ExcluirPorId(id);
            return RedirectToAction("Index");
        }
    }
}
