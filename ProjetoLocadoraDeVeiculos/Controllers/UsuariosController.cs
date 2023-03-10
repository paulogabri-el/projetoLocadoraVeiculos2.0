using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentValidator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLocadoraDeVeiculos.Data;
using ProjetoLocadoraDeVeiculos.Helper;
using ProjetoLocadoraDeVeiculos.Models;
using ProjetoLocadoraDeVeiculos.Models.ViewModels;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public UsuariosController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            var usuarios = await _context.Usuario.ToListAsync();
            return View(usuarios.OrderByDescending(x => x.DataCadastro));
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create([FromServices] ISessao _sessao)
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] ISessao _sessao, [Bind("Id,Nome,Cpf,Email,Senha,DataCadastro,DataAlteracao")] UsuarioViewModel usuario)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            var usuarioExiste1 = _context.Usuario.Where(x => x.Cpf == usuario.Cpf);

            if (usuarioExiste1 != null)
            {
                var usuarioExistente = usuarioExiste1.FirstOrDefault();
                if (usuarioExistente != null)
                {
                    TempData["MensagemUsuarioExistente"] = $"Já existe um usuário cadastrado com esse CPF! Nome: {usuarioExistente.Nome}";

                    return RedirectToAction(nameof(Create));
                }

            }

            var usuarioExiste = _context.Usuario.Where(x => x.Email == usuario.Email);

            if (usuarioExiste != null)
            {
                var usuarioExistente = usuarioExiste.FirstOrDefault();
                if (usuarioExistente != null)
                {
                    TempData["MensagemUsuarioExistente1"] = $"Já existe um usuário cadastrado com esse email! Nome: {usuarioExistente.Nome}";

                    return RedirectToAction(nameof(Create));
                }

            }
            

            var cpfValido = CpfValidation.Validate(usuario.Cpf);

            if (ModelState.IsValid && cpfValido)
            {
                var newUser = new Usuario()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Cpf = usuario.Cpf,
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    DataCadastro = DateTime.Now
                };
                newUser.SetSenhaHash();
                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!cpfValido)
                    TempData["MensagemErroCpf"] = $"O CPF informado não é valido!";

                return RedirectToAction(nameof(Create));
            }
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int id, [Bind("Id,Nome,Cpf,Email,DataCadastro,DataAlteracao")] UsuarioViewModel usuario)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id != usuario.Id)
            {
                return NotFound();
            }

            var cpfValido = CpfValidation.Validate(usuario.Cpf);

            try
            {
                if (ModelState.IsValid && cpfValido)
                {
                    try
                    {
                        var editUser = await _context.Usuario.FindAsync(id);
                        editUser.Id = usuario.Id;
                        editUser.Nome = usuario.Nome;
                        editUser.Cpf = usuario.Cpf;
                        editUser.Email = usuario.Email;
                        editUser.DataAlteracao = DateTime.Now;
                        editUser.SetSenhaHash();
                        _context.Update(editUser);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsuarioExists(usuario.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (!cpfValido)
                        TempData["MensagemErroCpf"] = $"O CPF informado não é valido!";

                    return RedirectToAction(nameof(Edit));
                }
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = $"O CPF ou o EMAIL que está tentando utilizar já está sendo utilizado por outro usuário!";

                return RedirectToAction("Edit");
            }
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromServices] ISessao _sessao, int id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (_context.Usuario == null)
            {
                return Problem("Entity set 'ProjetoLocadoraDeVeiculosContext.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            var usuarioLogado = _sessao.BuscarSessaoUsuario();
            var novoTeste = usuarioLogado.Id;

            if (usuario.Id == novoTeste)
            {
                TempData["MensagemErro"] = $"Não é possível excluir o usuário logado!";

                return RedirectToAction("Delete");
            }
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
