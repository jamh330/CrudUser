using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudUser.Repositorios;

namespace CrudUser.Pages
{
    public class NuevoRolModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage ="El campo nombre es requerido")]
        [Display(Name ="Nombre del rol")]
        public string Nombre { get; set; }
        public IActionResult OnGet()
        {
            
            var idSession = HttpContext.Session.GetString("idSession");
            //var session = HttpContext.Session;
            if (string.IsNullOrEmpty(idSession))
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Guardar en la bd
                var repo = new RolRepositorio();
                repo.InsertRol(this.Nombre);
               return RedirectToPage("./Roles");
            }

            return Page();
        }
    }
}
