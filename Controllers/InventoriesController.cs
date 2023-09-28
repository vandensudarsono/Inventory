using Inventory.Models;
using Inventory.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Inventory.Controllers
{
    public class AssetsController: Controller
    {
        private readonly AssetsService _assetService;

        public AssetsController(AssetsService assetsService)
        {
            _assetService = assetsService;
        }

        public async Task<IActionResult>Index()
        {
            if (_assetService == null) {
                return Problem("Entitiy set 'AssetService' is null. ");
            }

            var result = await _assetService.GetAsync();

            return View(result.ToList()); 
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _assetService == null)
            {
                return NotFound();
            }

            var result = await _assetService.GetOneAsync(id);
            if (result == null) {
                return NotFound();
            }

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Picture,Quantity,Price,Editor")]Assets assets)
        {
            if (ModelState.IsValid)
            {
                await _assetService.CreateOneAsync(assets);
                return RedirectToAction(nameof(Index));
            }

            return View(assets);
        }

        //GET: Assets/Edit/1
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _assetService == null)
            {
                return NotFound();
            }

            var result = await _assetService.GetOneAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Assets/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Picture,Quantity,Price,Editor")]Assets assets) 
        {
            if (id != assets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _assetService.UpdateOneAsync(id, assets);
                }
                catch (MongoWriteException ex)
                {
                    Console.WriteLine($"Errot at update data: {ex.Message}");
                }

                return RedirectToAction(nameof(Index));
            }

            return View(assets);
        }

        //Delete
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _assetService == null)
            {
                return NotFound();
            }

            var result = await _assetService.GetOneAsync(id);
            if (result == null )
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_assetService == null )
            {
                return Problem("Entity set 'AssetService' is null.");
            }

            var result = await _assetService.GetOneAsync(id);
            if (result != null) 
            {
               await _assetService.RemoveOneAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
       
    }
}