namespace EMG_MED1000_BACKEND.Entities
{
    public class Marque
    {
        private int MarqueId;
        private String NomMarque;

        //La liste des modèles associés à la marque
        private List<Modele> Modeles;

        //Déclaration d'un constructeur sans paramètre de la classe pour y initaliser notre objet Marque
        public Marque()
        {
            Modeles = new List<Modele>();
        }

        // Constructeur avec paramètre 
        public Marque(string nomMarque)
        {
            NomMarque = nomMarque;
            Modeles = new List<Modele>(); 
        }

        //Déclaration des getters et setters
        public int MarqId
        {
            get { return MarqueId; }
            set { MarqueId = value; }
        }

        public String NomMarq
        {
            get { return NomMarque; }
            set { NomMarque = value; }
        }

        public List<Modele> ListModele 
        {
            get { return Modeles; }
            set { Modeles = value; }
        }

    }
}