using EMG_MED1000_BACKEND.Entities;
using EMG_MED1000_BACKEND.Models;
using Microsoft.EntityFrameworkCore;

public class MarqueService
{
    private readonly VoitureContext _context;

    //Constructeur pour initialiser notre objet _context d'accès à la base de données
    public MarqueService(VoitureContext context)
    {
        _context = context;
    }

    //Méthode pour créer une marque
    public async Task<Marque> CreateMarque(String nomMarque, List<Modele> modeles)
    {
        // On verifie si une marque avec ce nom existe déjà
        var existingMarque = await _context.Marques
                                .FirstOrDefaultAsync(m => m.NomMarque == nomMarque);

        if (existingMarque != null)
        {
            throw new Exception("Une marque avec ce nom existe déjà.");
        }

        //On Vérifie l'unicité des modèles associés
        foreach (var modele in modeles)
        {
            var existingModele = await _context.Modeles
                .FirstOrDefaultAsync(m => m.NomModele == modele.NomModele);

            if (existingModele != null)
            {
                throw new Exception($"Un modèle avec le nom {modele.NomModele} existe déjà.");
            }
        }

        //Création d'un objet Marque sans utiliser le constructeur
        var marque = new Marque()
        {
            NomMarque = nomMarque,
            Modeles = modeles ?? new List<Modele>()
        };

        //Ajout de l'entité marque au _context
        _context.Marques.Add(marque);

        //Sauvegarde de l'entité dans la base de données
        await _context.SaveChangesAsync();

        return marque;
    }

    //Méthode pour modifier une marque
    public async Task<bool> UpdateMarque(int id, String nomMarque, List<Modele> modeles)
    {
        //Une fois que l'id est renseigné, on recherche l'id dans la table de la base de données
        var marque = await _context.Marques.FindAsync(id);
        if (marque == null)
        {
            return false;
        }

        //Si la marque existe, on met à jour les propriétés
        marque.NomMarque = nomMarque;
        marque.Modeles = modeles;

        //Ici, on met à jour la table à travers le _context
        _context.Marques.Update(marque);

        //Ensuite, on sauvegarde les changements
        await _context.SaveChangesAsync();

        return true;
    }


    //Méthode pour supprimer une marque
    public async Task<bool> DeleteMarque(int id)
    {
        //Pour supprimer une marque, on aura besoin de son id
        var marque = await _context.Marques.FindAsync(id);
        if (marque == null)
            throw new KeyNotFoundException("Marque introuvable");

        //Suppression de l'entité marque au _context
        _context.Marques.Remove(marque);

        //Sauvegarde des changements dans la base de données
        await _context.SaveChangesAsync();

        return true;
    }

    //Méthode pour obtenir toutes les marques
    public async Task<List<Marque>> GetAllMarquesAsync()
    {
        return await _context.Marques.ToListAsync();
    }

    //Méthode pour obtenir la marque par Id
    public async Task<Marque> GetMarqueById(int id)
    {
        return await _context.Marques
            .Include(m => m.Modeles) 
            .FirstOrDefaultAsync(m => m.MarqueId == id);
    }
   
}