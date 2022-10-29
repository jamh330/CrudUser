using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudUser.Models;
using CrudUser.Repositorios;

namespace CrudUser.Pages
{
    public class UsuariosModel : PageModel
    {
        public List<UsuarioListaModel> ListaUsuario { get; set; }
        public IActionResult OnGet()
        {
            var idSession = HttpContext.Session.GetString("idSession");
            //var session = HttpContext.Session;
            if (string.IsNullOrEmpty(idSession))
            {
                return RedirectToPage("./Index");
            }

            var repo = new UsuarioRepositorio();
            this.ListaUsuario = repo.obtenerUsuarios();

            return Page();
        }
    }
}
