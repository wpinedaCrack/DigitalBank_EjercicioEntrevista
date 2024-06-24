using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCFNegocio.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "El campo Nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string FechaFormateada { get; set; }
        [StringLength(1, ErrorMessage = "El campo Sexo no puede tener más de 1 caracter.")]
        public string Sexo { get; set; }
        [StringLength(40, ErrorMessage = "El campo Email no puede tener más de 40 caracter.")]
        public string Email { get; set; }
        public string Passworld { get; set; }

    }
}