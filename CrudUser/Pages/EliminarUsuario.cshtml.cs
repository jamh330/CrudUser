using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudUser.Repositorios;

namespace CrudUser.Pages
{
    public class EliminarUsuarioModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string NombreUsuario { get; set; }
        public IActionResult OnGet(int id)
        {
            var idSession = HttpContext.Session.GetString("idSession");
            //var session = HttpContext.Session;
            if (string.IsNullOrEmpty(idSession))
            {
                return RedirectToPage("./Index");
            }

            var repo = new UsuarioRepositorio();
            var usuario = repo.obtenerUsuarioPorId(id);
            this.NombreUsuario = usuario.nombreUsuario;

            return Page();
        }

        public IActionResult OnPost()
        {
            //  eliminar de la bd
            var repo = new UsuarioRepositorio();
            repo.EliminarUsuario(this.Id);
            return RedirectToPage("./Usuarios");
        }
    }
}
