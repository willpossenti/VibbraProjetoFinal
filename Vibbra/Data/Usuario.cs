using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vibbra.Data
{
    [Table("Usuario")]
    public class Usuario: ApplicationUser
    {
      
        public virtual IQueryable<UsuarioProjeto> UsuarioProjeto { get; set; }

        public string Password { get; set; }
    }
}
