using Microsoft.AspNetCore.Mvc;
using Santi.Game.App.Database;
using Santi.Game.App.Models;
using System.Linq;

namespace Santi.Game.App.Controllers
{
    public class MainController : Controller
    {
        private readonly GameDbContext _context;

        public MainController(GameDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var palavras = _context.Palavras.ToList();
            //ViewBag.Palavras = palavras;
            return View(palavras);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            var palavra = new Palavra();
            //ViewBag.Palavra = new Palavra();
            return View(palavra);
        }

        [HttpPost]
        public IActionResult Criar([FromForm] Palavra palavra)
        {
            if (ModelState.IsValid)
            {
                _context.Palavras.Add(palavra);
                _context.SaveChanges();

                return Redirect("Index");
            }
            else
            {
                ViewBag.Palavra = palavra;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var palavra = _context.Palavras.FirstOrDefault(x => x.Id == id);
            if (palavra != null)
            {
                return View("Criar", palavra);
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Palavra palavra)
        {
            if(ModelState.IsValid)
            {
                _context.Palavras.Update(palavra);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Criar", palavra);
        }

        [HttpGet]
        public IActionResult Remover(int id)
        {
            var palavra = _context.Palavras.FirstOrDefault(x => x.Id.Equals(id));
            if (palavra != null)
            {
                _context.Palavras.Remove(palavra);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MensagemErroRemover = "Não foi possível remover a palavra";
            return RedirectToAction("Index", "Main");
        }
    }
}
