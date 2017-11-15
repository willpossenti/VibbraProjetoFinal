using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vibbra.Data
{
    [Table("Tempo")]
    public class Tempo
    {
        public int ID { get; set; }

        public DateTime started_at { get; set; }

        public DateTime ended_at { get; set; }

        public virtual IQueryable<Projeto> Projeto { get; set; }

        public bool Ativo { get; set; }
    }
}
