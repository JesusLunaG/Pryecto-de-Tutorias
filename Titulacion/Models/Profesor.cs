﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Titulacion.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Inscripcion = new HashSet<Inscripcion>();
        }

        public int IdProfesor { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Correo { get; set; }
        public string Grupo { get; set; }
        public int HorasTotales { get; set; }
        public int HorasTutoria { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<Inscripcion> Inscripcion { get; set; }
    }
}
