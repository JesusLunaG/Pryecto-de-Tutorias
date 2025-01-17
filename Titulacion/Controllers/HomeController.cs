﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Titulacion.Clases;
using Titulacion.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Titulacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Bool = false;
            if (TempData["mensaje"] != null)
            {
                ViewBag.Bool = true;
                ViewBag.Error = TempData["mensaje"].ToString(); 
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuarios userReci)
        {
            UsuarioCLS user = new UsuarioCLS();

            switch (user.Validar(userReci))
            {
                case 0:
                    var claimsAdmin = new List<Claim>
                    {
                        new Claim("Usuario", userReci.User),
                        new Claim("Contraseña", userReci.Pass),
                        
                    };

                    claimsAdmin.Add(new Claim(ClaimTypes.Role, "Administrador"));

                    var claimsIdentityAdmin = new ClaimsIdentity(claimsAdmin, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentityAdmin));

                    return RedirectToAction("Alumnos", "Administrador");                    
                case 1:
                    var claimsProfe = new List<Claim>
                    {
                        new Claim("Usuario", userReci.User),
                        new Claim("Contraseña", userReci.Pass),
                        new Claim(ClaimTypes.Role, "Profesor")
                    };
                    var claimsIdentityProfesor = new ClaimsIdentity(claimsProfe, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentityProfesor));
                    return RedirectToAction("InicioProfesor", "Sesiones");
                case 2:
                    var claimsAlumno = new List<Claim>
                        {
                            new Claim("Usuario", userReci.User),
                            new Claim("Contraseña", userReci.Pass),
                            
                        };
                    if (!new UsuarioCLS().Tutoria)
                    {
                        claimsAlumno.Add(new Claim(ClaimTypes.Role,"Alumno"));

                        var claimsIdentityAlumno = new ClaimsIdentity(claimsAlumno, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentityAlumno));

                        return RedirectToAction("InicioAlumno", "Sesiones");
                    }
                    else
                    {
                        claimsAlumno.Add(new Claim(ClaimTypes.Role, "AlumnoTutoria"));

                        var claimsIdentityAlumno = new ClaimsIdentity(claimsAlumno, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentityAlumno));

                        return RedirectToAction("AlumnoTutoria", "Sesiones");
                    }
                default:
                    ViewBag.Bool = true;
                    ViewBag.Error = "Tu usuario y/o contraseña son incorrectos";
                    return View();
            }
        }
        [HttpPost]
        public IActionResult CambiarContraseña(string User, string Pass) {
            TempData["mensaje"] = new UsuarioCLS().UpdatePassAlumno(User, Pass);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register() {
            ViewBag.Bool = false;
            return View();
        }
        [HttpPost]
        public IActionResult Register(Usuarios usuario,Alumno alumno)
        {            
            ViewBag.Bool = true;
            ViewBag.TodoFine = new UsuarioCLS().registro(usuario, alumno);
            return View();
        }

        public async Task<IActionResult> Logout() {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login","Home");
        }

        public IActionResult Denegado() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
