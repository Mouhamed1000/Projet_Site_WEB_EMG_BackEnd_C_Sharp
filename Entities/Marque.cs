namespace EMG_MED1000_BACKEND.Entities
{
    public class Marque
    {
        public int MarqueId { get; set; }
        public String NomMarque { get; set; }

        //La liste des modèles associés à la marque
        public List<Modele> Modeles { get; set; } = new List<Modele>();

        //Déclaration d'un constructeur sans paramètre de la classe pour y initaliser notre objet Marque
        public Marque()
        {
        }

        // Constructeur avec paramètre 
        public Marque(string nomMarque)
        {
            NomMarque = nomMarque;
        }

    }
}