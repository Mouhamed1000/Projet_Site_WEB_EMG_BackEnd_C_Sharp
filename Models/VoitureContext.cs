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
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }

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

            // Configuration MARQUE
            modelBuilder.Entity<Marque>(entity =>
            {
                // Clé primaire
                entity.HasKey(m => m.MarqueId);
                entity.Property(m => m.MarqueId).ValueGeneratedOnAdd();

                // Nom unique
                entity.HasIndex(m => m.NomMarque).IsUnique();

                // Relation : une marque a plusieurs modeles
                entity.HasMany(m => m.Modeles)
                      .WithOne(mo => mo.Marque)
                      .HasForeignKey(mo => mo.MarqueId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuration MODELE
            modelBuilder.Entity<Modele>(entity =>
            {
                // Clé primaire
                entity.HasKey(m => m.ModeleId);
                entity.Property(m => m.ModeleId).ValueGeneratedOnAdd();

                // Nom unique
                entity.HasIndex(m => m.NomModele).IsUnique();
            });

            // Configuration VOITURE
            modelBuilder.Entity<Voiture>(entity =>
            {
                // Clé primaire
                entity.HasKey(v => v.VoitureId);
                entity.Property(v => v.VoitureId).ValueGeneratedOnAdd();

                // Champs optionnels
                entity.Property(v => v.Photo).IsRequired(false);
                entity.Property(v => v.Description).IsRequired(false);
            });
        }
    }
}