namespace EMG_MED1000_BACKEND.Entities
{
    using System.Text.Json.Serialization;

    public class Modele
    {
        //Définition des propriétés de notre entité Modele avec leurs getters et setters
        public int ModeleId { get; set; }

        public string NomModele { get; set; }

        public DateTime AnneeModele { get; set; }

        //Pour une marque donnée d'une voiture, il doit être disponible ses modèles
        //Donc dans l'entité Modele, il y'aura l'id de la marque qui est une clé étrangère
        public int MarqueId { get; set; }
        //Propriété de navigation 
        [JsonIgnore]
        public Marque Marque { get; set; }

        // Constructeur par défaut sans paramètres pour EF Core
        public Modele() {}

        //Déclaration d'un constructeur de la classe pour y initaliser notre objet Modele
        public Modele (String nomModele, DateTime anneeModele, int marqueId)
        {
            NomModele = nomModele;
            AnneeModele = anneeModele;
            MarqueId = marqueId;
        }
    }

}
