using System.Diagnostics;
using EI.Web.Models;
using EI.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceEstadisticas _serviceEstadisticas;

        public HomeController(IServiceEstadisticas serviceEstadisticas)
        {
            _serviceEstadisticas = serviceEstadisticas;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _serviceEstadisticas.CalcularAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(double nivelConfianza, double mediaPoblacionalImc, double nivelSignificancia)
        {
            nivelConfianza = Math.Clamp(nivelConfianza, 0.80, 0.999);
            mediaPoblacionalImc = Math.Clamp(mediaPoblacionalImc, 10.0, 60.0);
            nivelSignificancia = Math.Clamp(nivelSignificancia, 0.001, 0.20);

            var model = await _serviceEstadisticas.CalcularAsync(nivelConfianza, mediaPoblacionalImc, nivelSignificancia);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
