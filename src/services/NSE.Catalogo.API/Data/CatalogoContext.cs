using Microsoft.EntityFrameworkCore;
using System.Linq;
using NSE.Catalogo.API.Models;
using NSE.Core.Data;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Data
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }

        //Aplicar configuração do CatalagoContext para qualquer entidade que está no seu contexto
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //caso aconteça de mapear alguma coluna para banco de dados, o contexto mapea para varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
