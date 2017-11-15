using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vibbra.Data
{
    public class Contexto : DbContext
    {
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioProjeto> UsuarioProjeto { get; set; }
        public DbSet<Tempo> Tempo { get; set; }


        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
