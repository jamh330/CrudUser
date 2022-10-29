using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrudUser.Repositorios;

namespace CrudUser.Pages
{
    
    public class NuevoUsuarioModel : PageModel
    {
        [Display(Name = "Nombre de usuario")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Nombre de usuario es requerido")]
        public string NombreUsuario { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo Nombres es requerido")]
        public string Nombres { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo Apellidos es requerido")]
        public string Apellidos { get; set; }
        [Display(Name = "Rol")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Rol es requerido")]
        public int? RolId { get; set; }
        [Display(Name = "Pais")]
        [BindProperty]
        [Required(ErrorMessage = "El campo Pais es requerido")]
        public int? PaisId { get; set; }
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

        public SelectList Roles { get; set; }
        public SelectList Paises { get; set; }

        public IActionResult OnGet()
        {
            var idSession = HttpContext.Session.GetString("idSession");
            //var session = HttpContext.Session;
            if (string.IsNullOrEmpty(idSession))
            {
                return RedirectToPage("./Index");
            }

            var rolRepo = new RolRepositorio();
            var listaRoles = rolRepo.obtenerRoles();
            this.Roles = new SelectList(listaRoles, "Id", "Nombre");

            var paisRepo = new PaisRepositorio();
            var listaPaises = paisRepo.ObtenerPaises();
            this.Paises = new SelectList(listaPaises, "Id", "Nombre");

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
                var repo = new UsuarioRepositorio();
                if (repo.nombreUsuarioExiste(this.NombreUsuario))
                {
                    ModelState.AddModelError(string.Empty, "El nombre de usuario ya existe en la base de datos");
                    return Page();
                }


                //Guardar el usuario en la bd

                repo.InsertUsuario(this.NombreUsuario, this.Nombres, this.Apellidos, (int)this.RolId, (int)this.PaisId, this.Password);

                return RedirectToPage("./Usuarios");
            }

            return Page();

        }
    }
}
