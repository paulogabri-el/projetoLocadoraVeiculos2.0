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
    public class CategoriaVeiculosController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public CategoriaVeiculosController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context = context;
        }

        // GET: CategoriaVeiculos
        public async Task<IActionResult> Index([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            var categorias = await _context.CategoriaVeiculo.ToListAsync();
            return View(categorias.OrderByDescending(x => x.DataCadastro));

        }

        // GET: CategoriaVeiculos/Details/5
        public async Task<IActionResult> Details([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.CategoriaVeiculo == null)
            {
                return NotFound();
            }

            var categoriaVeiculo = await _context.CategoriaVeiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaVeiculo == null)
            {
                return NotFound();
            }

            return View(categoriaVeiculo);
        }

        // GET: CategoriaVeiculos/Create
        public IActionResult Create([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            return View();
        }

        // POST: CategoriaVeiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] ISessao _sessao, [Bind("Id,Nome,DataCadastro,DataAlteracao")] CategoriaVeiculoViewModel categoriaVeiculo)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var newCategoriaVec = new CategoriaVeiculo()
                {
                    Id = categoriaVeiculo.Id,
                    Nome = categoriaVeiculo.Nome,
                    DataCadastro = DateTime.Now
                };
                _context.Add(newCategoriaVec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaVeiculo);
        }

        // GET: CategoriaVeiculos/Edit/5
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.CategoriaVeiculo == null)
            {
                return NotFound();
            }

            var categoriaVeiculo = await _context.CategoriaVeiculo.FindAsync(id);
            if (categoriaVeiculo == null)
            {
                return NotFound();
            }
            return View(categoriaVeiculo);
        }

        // POST: CategoriaVeiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int id, [Bind("Id,Nome,DataCadastro,DataAlteracao")] CategoriaVeiculoViewModel categoriaVeiculo)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id != categoriaVeiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var editCategoriaVec = await _context.CategoriaVeiculo.FindAsync(id);
                editCategoriaVec.Nome = categoriaVeiculo.Nome;
                editCategoriaVec.DataAlteracao = DateTime.Now;
                _context.Update(editCategoriaVec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaVeiculo);
        }

        // GET: CategoriaVeiculos/Delete/5
        public async Task<IActionResult> Delete([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.CategoriaVeiculo == null)
            {
                return NotFound();
            }

            var categoriaVeiculo = await _context.CategoriaVeiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaVeiculo == null)
            {
                return NotFound();
            }

            return View(categoriaVeiculo);
        }

        // POST: CategoriaVeiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromServices] ISessao _sessao, int id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            try
            {
                if (_context.CategoriaVeiculo == null)
                {
                    return Problem("Entity set 'ProjetoLocadoraDeVeiculosContext.CategoriaVeiculo'  is null.");
                }
                var categoriaVeiculo = await _context.CategoriaVeiculo.FindAsync(id);
                if (categoriaVeiculo != null)
                {
                    _context.CategoriaVeiculo.Remove(categoriaVeiculo);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {

                return RedirectToAction("ErroReferencialCategoria", "Error");
            }

        }

        private bool CategoriaVeiculoExists(int id)
        {
            return _context.CategoriaVeiculo.Any(e => e.Id == id);
        }
    }
}
