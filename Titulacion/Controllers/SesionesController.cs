using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Titulacion.Clases;
using Titulacion.Models;

namespace Titulacion.Controllers
{    
    [Authorize]
    public class SesionesController : Controller
    {
        UsuarioCLS obj = new UsuarioCLS();
        General generic = new General();
        
        [HttpGet]
        [Authorize(Roles = "Alumno")]        
        public IActionResult InicioAlumno()
        {
            List<Profesor> listaProfesor = obj.listaProfesores();            
            ViewBag.Nombre = obj.Nombre;
            ViewBag.Boleta = generic.Boleta;
            return View(listaProfesor);
        }
        [Authorize(Roles = "Alumno")]
        [HttpPost]
        public async Task<IActionResult> InicioAlumno(string IdUsuario, string nomProfe)
        {
            if (obj.RegistrarTutor(IdUsuario, nomProfe))
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var claimsAlumno = new List<Claim>();                        

                claimsAlumno.Add(new Claim(ClaimTypes.Role, "AlumnoTutoria"));

                var claimsIdentityAlumno = new ClaimsIdentity(claimsAlumno, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentityAlumno));

                return RedirectToAction("AlumnoTutoria");
            }
            List<Profesor> listaProfesor = obj.listaProfesores();
            return View(listaProfesor);
        }
        [Authorize(Roles = "AlumnoTutoria")]
        public IActionResult AlumnoTutoria() {
            ViewBag.Nombre = obj.Nombre;
            return View();
        }        
        [Authorize(Roles = "AlumnoTutoria")]
        public FileResult Comprobante()
        {            
            FileStream documento = new ComprobanteCLS().GenerarComprobante();
            return File(documento, "application/pdf");
        }
        [HttpGet]
        [Authorize(Roles = "Profesor")]
        public IActionResult InicioProfesor() {
            ViewBag.Nombre = obj.Nombre;
            List<AlumnoCLS> alumno = obj.listaAlumnos();            
            return View(alumno);
        }

    }
}
