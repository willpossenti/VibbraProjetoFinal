using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vibbra.Data
{
    [Table("Projeto")]
    public class Projeto
    {
        public int Project_id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        [ForeignKey("Tempo_Id")]
        public virtual List<Tempo> Tempo { get; set; }

        public virtual IQueryable<UsuarioProjeto> UsuarioProjeto { get; set; }

        public bool Ativo { get; set; }
    }
}
