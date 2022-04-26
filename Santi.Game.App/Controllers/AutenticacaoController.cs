using Microsoft.AspNetCore.Mvc;
using Santi.Game.App.Database;
using Santi.Game.App.Models;
using System.Linq;

namespace Santi.Game.App.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly GameDbContext _context;

        public AutenticacaoController(GameDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Logar()
        {
            ViewBag.Usuario = new Usuario();
            return View();
        }

        [HttpPost]
        public IActionResult Logar([FromForm] Usuario usuario)
        {
            var usuarioResult = _context.Usuarios.FirstOrDefault(x => x.Email.Equals(usuario.Email) && x.Senha.Equals(usuario.Senha));
            
            if(usuarioResult == null)
            {
                ViewBag.Usuario = usuario;
                ViewBag.MensagemErroLogin = "Usuário ou senha inválidos.";
                return View();
            }

            return RedirectToAction("Index","Main");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            ViewBag.Usuario = new Usuario();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar([FromForm] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Logar", "Autenticacao");
        }
    }
}
