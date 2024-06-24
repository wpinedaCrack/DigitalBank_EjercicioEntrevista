using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace APIRest.Domain.Models
{
    public class LogMonitoreo 
    {
        [Key] // Marca la propiedad como clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que el valor se generará automáticamente
        public int Id { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string NombreMetodo { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Accion { get; set; }
       
        public int UserId { get; set; }
    }
}

