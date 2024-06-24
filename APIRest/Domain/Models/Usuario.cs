using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIRest.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Email { get; set; }
        //public DateTime? FechaNacimiento { get; set; }      
        //[Column(TypeName = "varchar(1)")]
        //public string Sexo { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Passworld { get; set; }
    }
}
