using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadoCrud_SQL.Models;
using Microsoft.EntityFrameworkCore;
namespace EmpleadoCrud_SQL.Data
{
    public class EmployeContext : DbContext
    {
        //Constructor 
        public EmployeContext(DbContextOptions<EmployeContext> options):base(options)
        {

        }
        //crear nuestro dbSet que llamará al modelo (clase Employe)--> Mapeo de nuestro modelo
        //En EmployeItems tendremos nuestros datos del módelo 
        public DbSet<Employe> Employe { get; set; }
    }
}
