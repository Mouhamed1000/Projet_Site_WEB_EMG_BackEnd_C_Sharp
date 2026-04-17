using System.Text.Json.Serialization;

namespace EMG_MED1000_BACKEND.Entities
{
    //Création d'une classe domaine Voiture
    public class Voiture
    {
        //Définition des propriétés relatives à Voiture
        public int VoitureId { get; set; }
        public StatutVoiture Statut { get; set; }
        public String Photo { get; set; }
        public String Description { get; set; }
        public DateTime AnneeVoiture { get; set; }

        //Clés étrangères vers Marque et Modele
        public int MarqueId { get; set; }

        //Une voiture possede ses marques et à ses marques est rattachées ses modèles
        //Propriétés de navigation
        [JsonIgnore]
        public Marque Marque { get; set; } 

        //Déclaration d'un constructeur sans paramètres
        public Voiture() {}

        //Déclaration d'un constructeur de la classe pour y initaliser notre objet Voiture
        public Voiture(StatutVoiture statut, String photo, String description, DateTime anneeVoiture, int marqueId)
        {
            Statut = statut;
            Photo = photo;
            Description = description;
            AnneeVoiture = anneeVoiture;
            MarqueId = marqueId;
        }

    }

    public enum StatutVoiture
    {
        Disponible,
        Vendue,
    }

}