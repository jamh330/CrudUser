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
    public class EliminarPaisModel : PageModel
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


             var repo = new PaisRepositorio();
            var pais = repo.ObtenerPaisPorId(id);


            this.Id = pais.Id;
            this.Nombre = pais.Nombre;

            return Page();
        }

        public IActionResult OnPost()
        {

           var repo = new PaisRepositorio();
            repo.EliminarPais(this.Id);
            return RedirectToPage("./Paises");

        }
    }
}
