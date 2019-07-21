﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAROM.Models;

namespace SAROM.Controllers
{
    public class OperationActionsController : Controller
    {
        private readonly OperationContext _context;

        public OperationActionsController(OperationContext context)
        {
            _context = context;
        }

        // GET: OperationActions
        public async Task<IActionResult> Index()
        {
            return View(await _context.OperationAction.ToListAsync());
        }

        // GET: OperationActions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationAction = await _context.OperationAction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operationAction == null)
            {
                return NotFound();
            }

            return View(operationAction);
        }

        // GET: OperationActions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OperationActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message")] OperationAction operationAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operationAction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operationAction);
        }

        // GET: OperationActions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationAction = await _context.OperationAction.FindAsync(id);
            if (operationAction == null)
            {
                return NotFound();
            }
            return View(operationAction);
        }

        // POST: OperationActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Message")] OperationAction operationAction)
        {
            if (id != operationAction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operationAction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationActionExists(operationAction.Id))
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
            return View(operationAction);
        }

        // GET: OperationActions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationAction = await _context.OperationAction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operationAction == null)
            {
                return NotFound();
            }

            return View(operationAction);
        }

        // POST: OperationActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var operationAction = await _context.OperationAction.FindAsync(id);
            _context.OperationAction.Remove(operationAction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationActionExists(string id)
        {
            return _context.OperationAction.Any(e => e.Id == id);
        }
    }
}
