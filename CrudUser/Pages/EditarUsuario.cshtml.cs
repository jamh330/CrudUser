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
    public class EditarUsuarioModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
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
        public SelectList Roles { get; set; }
        public SelectList Paises { get; set; }
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
            this.Nombres = usuario.nombres;
            this.Apellidos = usuario.apellidos;
            this.RolId = usuario.RolId;
            this.PaisId = usuario.PaisId;
            this.Id = usuario.Id;

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
            var repo = new UsuarioRepositorio();
            repo.ActualizarUsuario(this.Id, this.Nombres, this.Apellidos, (int)this.RolId, (int)this.PaisId);
            return RedirectToPage("./Usuarios");
        }
    }
}
