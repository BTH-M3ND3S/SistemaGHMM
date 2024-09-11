﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGHMM.Models;

namespace SistemaGHMM.Controllers
{
    public class PecaController : Controller
    {
        private readonly Contexto _context;

        public PecaController(Contexto context)
        {
            _context = context;
        }

        // GET: Peca
        public async Task<IActionResult> Index()
        {
              return _context.Peca != null ? 
                          View(await _context.Peca.ToListAsync()) :
                          Problem("Entity set 'Contexto.Peca'  is null.");
        }

        // GET: Peca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peca == null)
            {
                return NotFound();
            }

            var pecaModel = await _context.Peca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pecaModel == null)
            {
                return NotFound();
            }

            return View(pecaModel);
        }

        // GET: Peca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Peca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PecaNome,QuantidadeEstoque")] PecaModel pecaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pecaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pecaModel);
        }

        // GET: Peca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peca == null)
            {
                return NotFound();
            }

            var pecaModel = await _context.Peca.FindAsync(id);
            if (pecaModel == null)
            {
                return NotFound();
            }
            return View(pecaModel);
        }

        // POST: Peca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PecaNome,QuantidadeEstoque")] PecaModel pecaModel)
        {
            if (id != pecaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pecaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PecaModelExists(pecaModel.Id))
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
            return View(pecaModel);
        }

        // GET: Peca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peca == null)
            {
                return NotFound();
            }

            var pecaModel = await _context.Peca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pecaModel == null)
            {
                return NotFound();
            }

            return View(pecaModel);
        }

        // POST: Peca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peca == null)
            {
                return Problem("Entity set 'Contexto.Peca'  is null.");
            }
            var pecaModel = await _context.Peca.FindAsync(id);
            if (pecaModel != null)
            {
                _context.Peca.Remove(pecaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PecaModelExists(int id)
        {
          return (_context.Peca?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}