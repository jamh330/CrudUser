using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudUser.Pages
{
    public class HomeModel : PageModel
    {
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
    }
}
