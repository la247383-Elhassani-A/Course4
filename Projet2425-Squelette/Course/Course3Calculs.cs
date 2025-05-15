using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;

namespace CourseVoiture;

public partial class Course
{
    // ********************************************* //
    // ******* METHODES SANS CLAVIER NI ECRAN ****** //
    // ********************************************* //
    // ******** A vérifier en tests unitaires ****** //
    // ********************************************* //

    /// <summary>
    /// Initialise le jeu avec le parcours par défaut est utilisé
    /// A vérifier en tests unitaires        
    /// </summary>    
    public Course()
    {

    }

    /// <summary>
    /// Initialise le jeu en lisant le parcours du fichier de parcours.
    /// Si le fichier ne peut être lu, un parcours par défaut est utilisé
    /// en utilisant le constructeur par défaut
    /// A vérifier en tests unitaires 
    /// </summary>
    /// <param name="fichierParcours">Nom du fichier de parcours (de préférence relatif)</param>
    public Course(string fichierParcours) : this()
    {
        LireParcours(fichierParcours);
    }

    /// <summary>
    /// Lit un parcours dans un fichier et calcule ses caractèristiques comme la longueur.
    /// En cas d'erreur, charge un parcours par défaut.
    /// </summary>
    /// <param name="fichierParcours">Nom du fichier de parcours (de préférence relatif)</param>
    /// <returns>true si OK, false si le parcours par défaut a dû être chargé</returns>
    public bool LireParcours(string fichierParcours)
    {
        try
        {
            parcours.Clear();
            string[] lignes = File.ReadAllLines(fichierParcours);
            for (int index = 0; index < lignes.Length; index++)
            {
                if (lignes[index].Length > 60)
                {
                    return false;
                }
                parcours.Add(lignes[index]);
            }
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine("Problème avec la manipulation du fichier : ", err.Message);
            return false;
        }
    }

    /// <summary>
    /// Charge un parcours au hasrad
    /// </summary>
    /// <param name="folderName">Nom du répertoire avec les parcours</param>
    /// <returns>true si OK, false si le parcours par défaut a dû être chargé</returns>
    public bool ChargerParcoursAuHasard(string folderName = "Parcours")
    {
        //Cherche si le dossier existe
        string cheminComplet = Path.GetFullPath(folderName);

        if (!Directory.Exists(cheminComplet))
        {
            return false;
        }
        // Cherche les fichiers de parcours dans le dossier
        string[] fichier = Directory.GetFiles(cheminComplet, "fichier*.txt");
        if (fichier.Length == 0)
        {
            return false;
        }
        //Choisi un nombre aléatoire et prend le fichier a la position du nombre
        int aleatoire = Random.Shared.Next(0, fichier.Length);
        string fichierParcours = fichier[aleatoire];
        return LireParcours(fichierParcours);
    }


    /// <summary>
    /// Initialise le terrain du parcours et retient le moment de début 
    /// d'une partie pour mesurer la durée du jeu.
    /// La ligne 0 est en bas de l'écran,
    /// puis les lignes 1 à 5 contiennent la voiture, 
    /// puis les lignes 6 à la fin contiennent le reste du parcours
    /// </summary>
    public void InitialiserPartie()
    {
        compteurAvancerRoute = 24;
        vitesseActuelle = 5;
        terrain.Clear();
        metreCompteur = 0;
        decalerVoiture = 0;
        directionAcctuelle = Directions.aucune;
        directionPrecedent = Directions.aucune;
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Black;
        if (bonus)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        PartieEnCours = true;
        for (int index = 0; index < 24; index++)
        {
            terrain.Add(parcours[index]);
        }
        tempsJeu = DateTime.Now;
    }

    /// <summary>
    /// Fonction qui fait avancer la route en la décalant vers le bas
    /// et en insérant une nouvelle ligne en haut, qui fait tourner la
    /// route à gauche ou à droite, ou bien continuer tout droit, 
    /// selon le parcours de jeu téléchargé
    /// </summary>
    public void AvancerRoute()
    {
        if (compteurAvancerRoute == parcours.Count)
        {
            compteurAvancerRoute = 0;
        }
        terrain.Add(parcours[compteurAvancerRoute]);
        terrain.RemoveAt(0);
        compteurAvancerRoute++;
    }


    /// <summary>
    /// Fonction qui fait se déplacer la voiture vers la gauche ou la droite
    /// et vérifie s'il y a une sortie de route, détectée en tant que
    //  collision entre une ou plusieurs roues avec la bordure ou la pelouse
    /// </summary>
    public void DeplacerVoiture()
    {
        affichage = "";
        voiture = tableauVoiture;
        if (directionAcctuelle == Directions.Gauche)
        {
            decalerVoiture--;
            voiture = tableauVoitureGauche;
        }
        else if (directionAcctuelle == Directions.Droite)
        {
            decalerVoiture++;
            voiture = tableauVoitureDroite;
        }
        for (int index = terrain.Count - 1; index >= 0; index--)
        {
            string ligne = terrain[index];
            if (index <= 5 && index > 0)
            {
                int largueurVoiture = voiture[index - 1].Length;
                int debutAffichageVoiture = MILLIEU_TERRAIN - (largueurVoiture / 2) + decalerVoiture;

                string gauche = ligne.Substring(0, debutAffichageVoiture);
                string droite = ligne.Substring(debutAffichageVoiture + largueurVoiture);

                int bordure1 = ligne.IndexOf(BORDURE);
                int bordure2 = ligne.LastIndexOf(BORDURE);

                ligne = gauche + voiture[index - 1] + droite;
                affichage += ligne.PadLeft(70);

                int posRoueG = ligne.IndexOf(ROUE_GAUCHE);
                int posRoueGT = ligne.IndexOf(ROUE_GAUCHE_TOURNER);
                int posRoueD = ligne.IndexOf(ROUE_DROITE);
                int posRoueDT = ligne.IndexOf(ROUE_DROITE_TOURNER);

                if ((posRoueG != -1 && posRoueG <= bordure1) || (posRoueGT != -1 && posRoueGT <= bordure1) || (posRoueD != -1 && posRoueD >= bordure2) || (posRoueDT != -1 && posRoueDT >= bordure2))
                {
                    if (typeDejeu == TypesDeJeu.Temps)
                    {
                        vitesseActuelle = 5;
                    }
                    else PartieEnCours = false;
                }
            }
            else
            {
                affichage += ligne.PadLeft(70);
            }
            if (index != 0)
            {
                affichage += "\n";
            }
            if (test) terraintest.Add(ligne);
        }

    }

    /// <summary>
    /// Fonction pour accélerer, c'est-à-dire augmenter la vitesse
    /// L'accélération dépend du facteur d'accélération du type de partie
    /// et de la vitesse (quand on va vite, on accélère moins)
    /// La vitesse est limité à VITESSE_MAX
    /// </summary>
    public void Accelerer()
    {
        try
        {
            int acceleration = 1;
            if (vitesseActuelle <= 0) vitesseActuelle = VITESSE_MIN;
            if (vitesseActuelle >= 100) vitesseActuelle = VITESSE_MAX;
            int accelerationTouche = Convert.ToInt32(2 / Math.Log10(vitesseActuelle));
            if (acelerer)
            {
                if (vitesseActuelle < VITESSE_MAX)
                {
                    vitesseActuelle += accelerationTouche;
                }
                if (vitesseActuelle > VITESSE_MAX)
                {
                    vitesseActuelle = VITESSE_MAX;
                }
            }
            else if (ralentir)
            {
                if (vitesseActuelle > VITESSE_MIN)
                {
                    vitesseActuelle -= accelerationTouche + 5;
                }
                if (vitesseActuelle < VITESSE_MIN)
                {
                    vitesseActuelle = VITESSE_MIN;
                }
            }
            else
            {
                if (vitesseActuelle < VITESSE_MAX)
                {
                    vitesseActuelle += acceleration;
                }
                if (vitesseActuelle > VITESSE_MAX)
                {
                    vitesseActuelle = VITESSE_MAX;
                }
            }
        }
        catch (Exception err)
        {
            Console.WriteLine(err);
        }

    }
}