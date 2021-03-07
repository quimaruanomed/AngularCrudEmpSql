using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadoCrud_SQL.Models
{
    public class Employe
    {
       //Id será la clave primaria autoincremental 
        [Key]
        public int Id { get; set; }
        
        [Required] // El campo FirstName será obligatorio
        public string FirstName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
       public int Salary { get; set; }
    }
}
