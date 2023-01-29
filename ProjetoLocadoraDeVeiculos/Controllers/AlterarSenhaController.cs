using Microsoft.AspNetCore.Mvc;
using ProjetoLocadoraDeVeiculos.Data;
using ProjetoLocadoraDeVeiculos.Helper;
using ProjetoLocadoraDeVeiculos.Models;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public AlterarSenhaController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context= context;
        }

        public IActionResult Index([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Alterar([FromServices] ISessao _sessao, AlterarSenha alterarsenha)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _sessao.BuscarSessaoUsuario();
                    var senhaAtualUser = usuario.Senha;
                    var senhaAtual = alterarsenha.SenhaAtual.GerarHash();
                    var novaSenha = alterarsenha.NovaSenha.GerarHash();
                    if (senhaAtual.ToString() == senhaAtualUser && novaSenha != senhaAtual)
                    {
                        usuario.Senha = novaSenha;
                        _context.Update(usuario);
                        await _context.SaveChangesAsync();

                        TempData["MensagemSucesso"] = $"Senha alterada com sucesso! Para que as alterações sejam aplicadas, por favor, saia e faça o login novamente!";
                        return View("Index", alterarsenha);
                    }
                    else if (senhaAtual.ToString() == senhaAtualUser && novaSenha == senhaAtual)
                    {
                        TempData["MensagemErro"] = $"A nova senha não pode ser igual a senha atual!";
                        return View("Index", alterarsenha);
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"A senha atual do usuário não confere!";
                        return View("Index", alterarsenha);
                    }
                }
                return View("Index");

            }
            catch (Exception)
            {
                TempData["MensagemErro"] = $"Não conseguimos alterar a sua senha. Por favor, tente novamente!";

                return View("Index", alterarsenha);
            }
        }
    }
}
