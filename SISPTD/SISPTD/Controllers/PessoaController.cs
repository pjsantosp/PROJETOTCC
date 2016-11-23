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
            var pessoa = pessoaBO.Selecionar().Where(p => p.cpf == cpf && p.TipoPessoa == TipoPessoa.Medico).FirstOrDefault();
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
                    pessoa.TipoPessoa = TipoPessoa.Paciente;
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
                    pessoa.TipoPessoa = TipoPessoa.Acompanhante;
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
        public ActionResult EditarAcompanhante(long? id)
        {
            //ViewBag.acompanhante = pessoaBO.Selecionar().Where(a => a.acompanhanteId == id.Value).Count();
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
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditarAcompanhante(Pessoa objAcompanhante)
        {
            try
            {
                objAcompanhante.cns = Ultis.Util.RemoverMascara(objAcompanhante.cns);
                objAcompanhante.cpf = Ultis.Util.RemoverMascara(objAcompanhante.cpf);
                if (ModelState.IsValid)
                {

                    pessoaBO.CalculoIdade(objAcompanhante);
                    pessoaBO.Alterar(objAcompanhante);
                    TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                }
                return View(objAcompanhante);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ops! Ocorreu um erro!" + ex.Message;
            }
            return View(objAcompanhante);
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
                funcionario.cpf = Ultis.Util.RemoverMascara(funcionario.cns);
                if (ModelState.IsValid)
                {
                    funcionario.TipoPessoa = TipoPessoa.Funcionario;
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
                    pessoa.TipoPessoa = TipoPessoa.Funcionario;
                    pessoaBO.CalculoIdade(pessoa);
                    pessoaBO.Alterar(pessoa);
                    TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                }
                return RedirectToAction("ListaDeFuncionario");
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
                    pessoa.TipoPessoa = TipoPessoa.Medico;
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
                    pessoa.TipoPessoa = TipoPessoa.Medico;
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
        [Authorize(Roles ="Administrador, Gerente")]
        public ActionResult ManutencaoDeCadastroBusca(string cpf )
        {
            cpf = Util.RemoverMascara(cpf);
            var pessoa = pessoaBO.Selecionar().Where(p => p.cpf == cpf).FirstOrDefault();
            if (pessoa == null || pessoa.idade < 18)
            {
                return Json(new { Nome = "", Id = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Id = pessoa.pessoaId,
                    Cpf = pessoa.cpf,
                    Nome = pessoa.nome,
                    DtNascimento = pessoa.dt_Nascimento.ToShortDateString(),
                    Rg = pessoa.rg,
                    OrgaoEmissor = pessoa.orgaoemissor,
                    Cns = pessoa.cns,
                    Crm = pessoa.crm,
                    Mae = pessoa.nome_Mae,
                    Pai = pessoa.nome_Pai,
                    Tel = pessoa.tel,
                    Cel = pessoa.cel,
                    Email = pessoa.email,
                    Cep = pessoa.Endereco.cep,
                    Rua = pessoa.Endereco.rua,
                    Numero = pessoa.Endereco.numero,
                    Bairro = pessoa.Endereco.bairro
                },
                JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ManutencaoDeCadastro()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ManutencaoDeCadastro(int idPessoaManutencao, Pessoa pessoa)
        {
            try
            {
                pessoa.cns = Ultis.Util.RemoverMascara(pessoa.cns);
                pessoa.cpf = Ultis.Util.RemoverMascara(pessoa.cpf);
                if (ModelState.IsValid)
                {
                    Pessoa objPessoa = pessoaBO.SelecionarPorId(idPessoaManutencao);
                    pessoaBO.CalculoIdade(objPessoa);
                    objPessoa.TipoPessoa = pessoa.TipoPessoa;
                    pessoaBO.Alterar(objPessoa);
                    if (objPessoa.TipoPessoa== TipoPessoa.Paciente)
                    {
                        TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                        return RedirectToAction("Index");

                    }
                    else if(objPessoa.TipoPessoa == TipoPessoa.Funcionario)
                    {
                        TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                        return RedirectToAction("ListaDeFuncionario");

                    }
                    else if (objPessoa.TipoPessoa == TipoPessoa.Medico)
                    {
                        TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                        return RedirectToAction("ListaMedico");
                    }
                    

                    TempData["Sucesso"] = "Alteração Realizada com Sucesso!";
                    return View(objPessoa);
                }
                else
                {
                    TempData["Errro"] = "Não possível Alterar o Cadastro !";
                }
               
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
