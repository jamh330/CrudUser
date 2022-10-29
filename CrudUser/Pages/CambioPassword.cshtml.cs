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
    public class CambioPasswordModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [Display(Name = "Contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        [MinLength(8, ErrorMessage = "El campo Contraseña debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseña debe tener al menos una mayuscula,minusculas y digitos")]
        public string Password { get; set; }
        [Display(Name = "Repetir contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Repetir contraseña es requerido")]
        [MinLength(8, ErrorMessage = "El campo repetir contraseña debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseña debe tener al menos una mayuscula,minusculas y digitos")]
        public string RePassword { get; set; }
        public IActionResult OnGet(int id)
        {
            this.Id = id;
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
                //Comprobar si las contraseñas son iguales
                if (this.Password != this.RePassword)
                {
                    ModelState.AddModelError(string.Empty, "Las contraseñas no son iguales");
                    return Page();
                }

                //Actualizar la contraseña en la BD
                var repo = new UsuarioRepositorio();
                repo.ActualizarPassword(this.Id, this.Password);

                return RedirectToPage("./Usuarios");
            }

            return Page();
        }
    }
}
