using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Data.SqlClient;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using CrudUser.Repositorios;


namespace CrudUser.Pages
{
    public class EditarPaisModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [Display(Name = "Nombre del pais")]
        public string Nombre { get; set; }
        public IActionResult OnGet(int id)
        {
            var idSession = HttpContext.Session.GetString("idSession");
            //var session = HttpContext.Session;
            if (string.IsNullOrEmpty(idSession))
            {
                return RedirectToPage("./Index");
            }
            this.Id = id;

            //buscar el rol por id en la bd

          
            var repo = new PaisRepositorio();
            var pais = repo.ObtenerPaisPorId(this.Id);

            this.Nombre = pais.Nombre;

            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                var repo = new PaisRepositorio();
                repo.ActualizarPais(this.Id, this.Nombre);
                //actualizar en la bd
                return RedirectToPage("./Paises");

            }

            return Page();
        }



    }
}
