using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vibbra.Data
{
    [Table("UsuarioProjeto")]
    public class UsuarioProjeto
    {
        public Guid ID { get; set; }

        [ForeignKey("Usuario_Id")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Projeto_Id")]
        public virtual Projeto Projeto { get; set; }

        public bool Ativo { get; set; }
    }
}
