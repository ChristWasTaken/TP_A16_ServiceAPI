using Microsoft.EntityFrameworkCore;

namespace TP_A16_ServiceAPI.models
{
    public class ProduitsContext : DbContext
    {
        public ProduitsContext(DbContextOptions<ProduitsContext> options) : base(options)
        {
            
        }
        public DbSet<Produits> Produits { get; set; } = null;
    }

    
}
