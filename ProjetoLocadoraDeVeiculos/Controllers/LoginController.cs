using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using ProjetoLocadoraDeVeiculos.Data;
using ProjetoLocadoraDeVeiculos.Helper;
using ProjetoLocadoraDeVeiculos.Models;
using ProjetoLocadoraDeVeiculos.Repositorios;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;
        private readonly IUsuarioRepositorio _usuariorepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(ProjetoLocadoraDeVeiculosContext context, IUsuarioRepositorio usuariorepositorio, ISessao sessao, IEmail email)
        {
            _context = context;
            _usuariorepositorio = usuariorepositorio;
            _sessao = sessao;
            _email = email;
        }

        //
        public IActionResult Index()
        {
            // Se o usuário estiver logado, redireciona direto para a home.
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = _usuariorepositorio.BuscarPorEmail(login.Email);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(login.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha do usuário é inválida. Por favor, tente novamente.";

                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar o seu login, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RedefirnirSenha(RedefinirSenha redefinirSenha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = _usuariorepositorio.BuscarPorEmailECpf(redefinirSenha.Email, redefinirSenha.Cpf);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Olá, {usuario.Nome}! Você solicitou uma redefinição de senha, a sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Nova Senha - Loc Vec", mensagem);
                        
                        if (emailEnviado)
                        {
                            _context.Update(usuario);
                            await _context.SaveChangesAsync();
                            TempData["MensagemSucesso"] = $"Enviamos uma nova senha para o seu email cadastrado.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos redefinir a sua senha! Por favor, tente novamente!";
                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $"Email e/ou CPF inválido(s). Por favor, tente novamente.";
                }

                return View("RedefinirSenha");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos redefinir a sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
