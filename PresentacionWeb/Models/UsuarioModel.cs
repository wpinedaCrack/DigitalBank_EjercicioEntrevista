using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentacionWeb.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        //[Required(ErrorMessage = "El campo FechaNacimiento es obligatorio")]
        public DateTime? FechaNacimiento { get; set; }
        public string FechaFormateada { get; set; }
        [Required(ErrorMessage = "El campo Sexo es obligatorio")]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string Email { get; set; }
        public string Passworld { get; set; }
    }
}