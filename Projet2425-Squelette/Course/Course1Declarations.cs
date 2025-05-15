using System.Text;
using MySqlConnector;

namespace CourseVoiture;

public partial class Course
{
    // ********************************************* //
    // ************** ENUMERATIONS ***************** //
    // ********************************************* //

    // Directions possibles pour les roues de la voiture
    public enum Directions
    {
        Droite,
        Gauche,
        aucune,

    }

    // Types de parties (pour le bonus)
    public enum TypesDeJeu
    {
        Distance,
        Temps
    }

    // ********************************************* //
    // *************** CONSTANTES ****************** //
    // ********************************************* //
    // Dimensions du jeu et de la route
    const int LARGEUR_CONSOLE = 80;
    const int HAUTEUR_CONSOLE = 24;
    const int LARGUEUR_PARCOURS = 60;
    const int LARGEUR_ROUTE = 30;
    public const int MILLIEU_TERRAIN = 30;
    const int LARGEUR_PELOUSE = 21;

    // Consantes pour la vitesse est le temps d'attente en ms entre deux mouvements
    public const int VITESSE_MAX = 100;
    public const int VITESSE_MIN = 5;
    public int vitesseActuelle = 5;

    // Constantes pour l'accélération
    // Bonus: l'accélération dépend du type de partie
    public bool acelerer = false;
    public bool ralentir = false;

    // Constantes pour le dessin de la voiture
    // Lien utile: https://symbl.cc/en/unicode-table/#box-drawing
    // ▐ ▌ ▊ ▀ ▅ ░ ▒ ▓ ╲ ╱ \ / ┝ ┥ ┣ ┫ | █
    public string[] tableauVoiture = [DERRIERE_VOITURE, MILLIEU_BAS_VOITURE, MILLIEU_HAUT_VOITURE, DEVANT_VOITURE, TETE_VOITURE];
    public string[] tableauVoitureGauche = [DERRIERE_VOITURE, MILLIEU_BAS_VOITURE, MILLIEU_HAUT_VOITURE, DEVANT_VOITURE_TOURNER_GAUCHE, TETE_VOITURE];
    public string[] tableauVoitureDroite = [DERRIERE_VOITURE, MILLIEU_BAS_VOITURE, MILLIEU_HAUT_VOITURE, DEVANT_VOITURE_TOURNER_DROITE, TETE_VOITURE];
    public string[] voiture;
    const string TETE_VOITURE = "▅";
    const string DEVANT_VOITURE = "┣███┫";
    const string MILLIEU_HAUT_VOITURE = "▐▀▌";
    const string MILLIEU_BAS_VOITURE = "▐ö▌";
    const string DERRIERE_VOITURE = "┣█▓█┫";
    const string DEVANT_VOITURE_ROUE_FINE = "┝███┥";
    const string DEVANT_VOITURE_TOURNER_GAUCHE = "╲███╲";
    const string DEVANT_VOITURE_TOURNER_DROITE = "╱███╱";
    public const char ROUE_GAUCHE = '┣';
    public const char ROUE_DROITE = '┫';
    public const char ROUE_GAUCHE_TOURNER = '╲';
    public const char ROUE_DROITE_TOURNER = '╱';

    // Constantes pour le dessin du terrain
    // ░ ▒ ▓ █  (0x2591 à 0x2594) donnent un niveau d'intensité de █ sous Linux
    public const char PELOUSE = '░';
    const char TERRAIN_1 = '▒';
    const char TERRAIN_2 = '▓';
    public const char BORDURE = '█';

    //ligne de 80 de longueur pour dialogue de fin
    const string LIGNE_VIDE = "                                                                                ";

    // ********************************************* //
    // *************** PROPRIETES ****************** //
    // ********************************************* //
    // Parcours
    public const string DEFAUT = "Parcours/defaut.txt";
    public List<string> parcours = new();
    public List<string> terrain = new();
    public List<string> terraintest = new();
    // Couleurs du terrain et de la voiture. La route a la coudeur du fond du décor


    // Mesure du temps pour le jeu de durée
    // A REMPLIR

    // Mesure du temps de calcul
    DateTime debutCalcul;
    TimeSpan tempsCalcul;
    DateTime tempsJeu;
    TimeSpan tempsEntreDebut;

    // Paramètres de la course
    public bool PartieEnCours = false;
    public int compteurAvancerRoute = 24;
    public Directions directionAcctuelle = Directions.aucune;
    public Directions directionPrecedent = Directions.aucune;
    public int decalerVoiture = 0;
    public string affichage = "";
    public bool collision = false;
    public int metreCompteur = 0;
    public List<ScorePartie> listeScoresParties = [];
    public bool bonus = false;
    public TypesDeJeu typeDejeu;
    public bool test = false;
}