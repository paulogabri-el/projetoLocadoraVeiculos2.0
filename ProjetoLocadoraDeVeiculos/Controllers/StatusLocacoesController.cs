using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLocadoraDeVeiculos.Data;
using ProjetoLocadoraDeVeiculos.Helper;
using ProjetoLocadoraDeVeiculos.Models;
using ProjetoLocadoraDeVeiculos.Models.ViewModels;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class StatusLocacoesController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public StatusLocacoesController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context = context;
        }

        // GET: StatusLocacoes
        public async Task<IActionResult> Index([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            var statusLocacoes = await _context.StatusLocacao.ToListAsync();
            return View(statusLocacoes.OrderByDescending(x => x.DataCadastro));
        }

        // GET: StatusLocacoes/Details/5
        public async Task<IActionResult> Details([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.StatusLocacao == null)
            {
                return NotFound();
            }

            var statusLocacao = await _context.StatusLocacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusLocacao == null)
            {
                return NotFound();
            }

            return View(statusLocacao);
        }

        // GET: StatusLocacoes/Create
        public IActionResult Create([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            return View();
        }

        // POST: StatusLocacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] ISessao _sessao, [Bind("Id,Nome")] StatusLocacaoViewModel statusLocacao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var newStatusLoc = new StatusLocacao()
                {
                    Id = statusLocacao.Id,
                    Nome = statusLocacao.Nome,
                    DataCadastro = DateTime.Now
                };
                _context.Add(newStatusLoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusLocacao);
        }

        // GET: StatusLocacoes/Edit/5
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.StatusLocacao == null)
            {
                return NotFound();
            }

            var statusLocacao = await _context.StatusLocacao.FindAsync(id);
            if (statusLocacao == null)
            {
                return NotFound();
            }
            return View(statusLocacao);
        }

        // POST: StatusLocacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int id, [Bind("Id,Nome")] StatusLocacaoViewModel statusLocacao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id != statusLocacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editStatusLoc = await _context.StatusLocacao.FindAsync(id);
                    editStatusLoc.Nome = statusLocacao.Nome;
                    editStatusLoc.DataAlteracao = DateTime.Now;
                    _context.Update(editStatusLoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusLocacaoExists(statusLocacao.Id))
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
            return View(statusLocacao);
        }

        // GET: StatusLocacoes/Delete/5
        public async Task<IActionResult> Delete([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.StatusLocacao == null)
            {
                return NotFound();
            }

            var statusLocacao = await _context.StatusLocacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusLocacao == null)
            {
                return NotFound();
            }

            return View(statusLocacao);
        }

        // POST: StatusLocacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromServices] ISessao _sessao, int id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            try
            {
                if (_context.StatusLocacao == null)
                {
                    return Problem("Entity set 'ProjetoLocadoraDeVeiculosContext.StatusLocacao'  is null.");
                }
                var statusLocacao = await _context.StatusLocacao.FindAsync(id);
                if (statusLocacao != null)
                {
                    _context.StatusLocacao.Remove(statusLocacao);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {

                return RedirectToAction("ErroReferencialStatusLocacao", "Error");
            }
        }

        private bool StatusLocacaoExists(int id)
        {
            return _context.StatusLocacao.Any(e => e.Id == id);
        }
    }
}
