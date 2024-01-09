using System;
using ApiCrud.Estudantes;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Estudante> Estudantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString:"Data Source=Banco.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

