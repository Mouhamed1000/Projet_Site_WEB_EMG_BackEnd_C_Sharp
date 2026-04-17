using EMG_MED1000_BACKEND.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMG_MED1000_BACKEND.Models
{
    //Création d'une classe VoitureContext héritant de DbContext
    //Cette classe servira à l'interaction avec la base de données en utilisant EF Core
    public class VoitureContext : DbContext
    {
        private readonly IConfiguration _configuration;

        //Constructeur
        public VoitureContext(DbContextOptions<VoitureContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        //entitées
        public DbSet<Voiture> Voitures {get; set; }
        public DbSet<Marque> Marques {get; set; }
        public DbSet<Modele> Modeles {get; set; }

        //Implémentation de la méthode OnConfiguring (...) permettant l'import des configs de notre base de données
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }

            base.OnConfiguring(optionsBuilder);
        }

        //Implementation des contraintes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Marque>()
            .HasKey(m => m.MarqueId);

            // On s'assure que Nom de la Marque est unique dans la table Marques
            modelBuilder.Entity<Marque>()
                .HasIndex(m => m.NomMarque)
                .IsUnique();

            modelBuilder.Entity<Modele>()
                .HasKey(v => v.ModeleId);

            modelBuilder.Entity<Voiture>()
                .HasKey(l => l.VoitureId);

            //On s'assure que NomModele est unique dans la table Modeles
            modelBuilder.Entity<Modele>()
                .HasIndex(m => m.NomModele)
                .IsUnique();

            // Configuration de la relation entre Marque et Modele : Une marque peut avoir plusieurs modèles
            modelBuilder.Entity<Marque>()
                .HasMany(m => m.Modeles)
                .WithOne(m => m.Marque)
                .HasForeignKey(m => m.MarqueId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    } 
}