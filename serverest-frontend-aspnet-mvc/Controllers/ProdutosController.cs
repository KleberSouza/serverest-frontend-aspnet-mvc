using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using serverest_frontend_aspnet_mvc.Models;
using serverest_frontend_aspnet_mvc.Services;
using System.Reflection;

namespace serverest_frontend_aspnet_mvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ServeRestService _serveRestService;

        public ProdutosController(ServeRestService serveRestService)
        {
            _serveRestService = serveRestService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _serveRestService.GetAllProdutos();
            return View(produtos);
        }

        public async Task<IActionResult> Details(string id)
        {
            var produtos = await _serveRestService.GetProdutoById(id);
            return View(produtos);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            if (!_serveRestService.AuthorizationToken()) return RedirectToAction("Logout", "Usuarios");
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoCreate model)
        {
            var produtos = await _serveRestService.CreateProduto(model);

            ViewBag.Message = produtos;
            return View(model);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id)
        {
            if (!_serveRestService.AuthorizationToken()) return RedirectToAction("Logout", "Usuarios");

            var produtos = await _serveRestService.GetProdutoById(id);
            return View(produtos);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProdutoCreate model)
        {
            var produtos = await _serveRestService.UpdateProdutoById(id, model);

            ViewBag.Message = produtos;

            var data = await _serveRestService.GetProdutoById(id);
            return View(data);
        }
    

        public async Task<IActionResult> Delete(string id)
        {
            if (!_serveRestService.AuthorizationToken()) return RedirectToAction("Logout", "Usuarios");
            var produtos = await _serveRestService.GetProdutoById(id);
            return View(produtos);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var produtos = await _serveRestService.DeleteProdutoById(id);
            return RedirectToAction("Index", "Produtos");
        }

    }
}
