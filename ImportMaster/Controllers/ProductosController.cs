using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImportMaster.Data;
using InventoryManagement.Models;

namespace ImportMaster.Controllers
{
    public class ProductosController : Controller
    {
          private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(ApplicationDbContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Existencias,Precio,PorcentajeImpuesto,OrdenCompraId")] Producto producto)
        {
            try
            {
                _logger.LogInformation("Intentando añadir producto...");
                _logger.LogInformation($"Datos del producto: {producto.Nombre}, {producto.Descripcion}, {producto.Existencias}, {producto.Precio}");

                _context.Add(producto);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Producto añadido exitosamente.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al añadir producto: {ex.Message}");
                throw;
            }
        }


        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Existencias,Precio,PorcentajeImpuesto,OrdenCompraId")] Producto producto)
        {


            if (id != producto.Id)
            {
                _logger.LogWarning("IDs no coinciden. Redirigiendo a NotFound.");
                return NotFound();
            }

        
                try
                {
                    _logger.LogInformation("Actualizando producto...");
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Producto actualizado exitosamente.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError($"Error de concurrencia: {ex.Message}");
                    if (!ProductoExists(producto.Id))
                    {
                        _logger.LogWarning("Producto no existe. Redirigiendo a NotFound.");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
     

            return View(producto);
        }


        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
