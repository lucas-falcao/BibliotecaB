using LivrosApi.Maps;
using Microsoft.EntityFrameworkCore;

namespace LivrosApi.Models
{
    public class LivroContext : DbContext
    {
        public LivroContext(DbContextOptions<LivroContext> option) :base(option)
        {}
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LivroMap());
        }
    }
}