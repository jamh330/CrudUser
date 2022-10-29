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
    public class EliminarRolModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Nombre { get; set; }
        public IActionResult OnGet(int id)
        {
            var idSession = HttpContext.Session.GetString("idSession");
            //var session = HttpContext.Session;
            if (string.IsNullOrEmpty(idSession))
            {
                return RedirectToPage("./Index");
            }

            var repo = new RolRepositorio();
            var rol = repo.obtenerRolPorId(id);

            this.Id = rol.Id;
            this.Nombre = rol.Nombre;

            return Page();
        }

        public IActionResult OnPost()
        {
            var repo = new RolRepositorio();
            repo.EliminarRol(this.Id);
            return RedirectToPage("./Roles");
        }
    }
}
