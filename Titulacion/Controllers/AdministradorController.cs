﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Titulacion.Clases;
using Titulacion.Models;

namespace Titulacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        AdministradorCLS obj = new AdministradorCLS();
        [HttpGet]
        public IActionResult Alumnos() {            
            return View(obj.MostrarALumnos());
        }
        [HttpPost]
        public IActionResult Alumnos(Alumno alumno)
        {
            obj.ModificarAlumno(alumno);
            return View(obj.MostrarALumnos());
        }
        [HttpPost]
        public IActionResult AgregarAlumno(Alumno alumno, string User) {
            obj.AgregarAlumno(alumno, User);
            return RedirectToAction("Alumno");
        }
        [HttpPost]
        public IActionResult EliminarAlumno(string idAlumno)
        {
            obj.EliminarAlumno(int.Parse(idAlumno));
            return RedirectToAction("Alumnos");
        }
        [HttpGet]
        public IActionResult Profesores() {
            return View(obj.MostrarProfesor());
        }
        [HttpPost]
        public IActionResult Profesores(Profesor profesor)
        {
            obj.ModificarProfesor(profesor);
            return View(obj.MostrarProfesor());
        }
        [HttpPost]
        public IActionResult AgregarProfesor(Profesor prof, string User)
        {
            obj.AgregarProfesor(prof, User);
            return RedirectToAction("Profesores");
        }
        [HttpPost]
        public IActionResult EliminarProfesor(string idProfesor)
        {
            obj.EliminarProfesor(int.Parse(idProfesor));
            return RedirectToAction("Profesores");
        }
        [HttpGet]
        public IActionResult Inscripciones() {
            return View(obj.MostrarInscripciones());
        }
    }
}
