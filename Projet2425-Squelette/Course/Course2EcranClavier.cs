using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CourseVoiture;

public partial class Course
{
    // ********************************************* //
    // **** METHODES POUR L'AFFICHAGE A L'ECRAN **** //
    // ********************************************* //
    /// Initialisation du jeu: dimensions de la console
    /// Ne pas utiliser en tests unitaires
    /// Jouer une partie comporte plusieurs phases
    /// - Initialiser
    /// - Boucler tant que la partie est en cours
    /// - Terminer avec un dialogue <summary>


    /// <summary>
    /// Initialisation du jeu en fonction de la console
    /// </summary>
    /// <param name="ModeTest">true c'est pendant les tests</param>
    public void InitialiserJeu(bool ModeTest = false)
    {
        //rendre le curseur invisible
        Console.CursorVisible = false;

        //Initialiser les couleurs de la console pour le menu principale
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        //Verifier si les valeurs de longueur et de largeur de la console sont correcte
        if (Console.WindowWidth != 80 || Console.WindowHeight != 24)
        {
            Console.WriteLine("La longueur et la largeur du terminal pour le jeu ne correspondent pas aux valeurs par défaut (80,24). Veuillez les changer dans les paramètres.");
            TerminerJeu();
        }
        string message;
        CreerDB(false, out message);
    }

    /// <summary>
    /// Dialogue de fin de jeu. 
    /// Remet les couleurs aux valeurs par défaut, efface l'écran et affiche un message d'au revoir
    /// Ne pas utiliser en tests unitaires
    /// </summary>
    public void TerminerJeu()
    {
        Console.ResetColor();
        Console.Clear();
        Console.WriteLine("Au revoir !");
    }

    /// <summary>
    /// Affichage des meilleurs scores
    /// Ne pas utiliser en tests unitaires
    /// </summary>
    public void AfficherScores()
    {
        //Affichage:
        string mess;
        if (bonus)
        {
            Console.Clear();
            Console.SetCursorPosition(13, 11);
            Console.WriteLine("Quel type de jeu voulez vous voir les meilleurs scores");
            Console.SetCursorPosition(21, 12);
            Console.WriteLine("(D) JEU DE DISTANCE | JEU DE TEMPS (T)");
            while (true)
            {
                ConsoleKeyInfo touche = Console.ReadKey(true);
                if (touche.Key == ConsoleKey.D)
                {
                    typeDejeu = TypesDeJeu.Distance;
                    break;
                }
                if (touche.Key == ConsoleKey.T)
                {
                    typeDejeu = TypesDeJeu.Temps;
                    break;
                }
            }
            Console.Clear();
            string jeuAcctuelle =
            typeDejeu == TypesDeJeu.Distance ? "Distance" : "Temps";
            int decal =
            typeDejeu == TypesDeJeu.Distance ? 52 : 49;
            string cadreHaut =
            typeDejeu == TypesDeJeu.Distance ? "╔═════════════════════════════════════╗" : "╔══════════════════════════════════╗";
            string cadreBas =
            typeDejeu == TypesDeJeu.Distance ? "╚═════════════════════════════════════╝" : "╚══════════════════════════════════╝";
            Console.WriteLine(cadreHaut.PadLeft(50));
            Console.WriteLine($"║ Meilleurs scores du jeu de {jeuAcctuelle} ║".PadLeft(50));
            Console.WriteLine(cadreBas.PadLeft(50));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Jeu de disctance".PadLeft(24));
            Console.WriteLine("════════════════".PadLeft(24));
            listeScoresParties = LireScores(false, 5, typeDejeu, out mess)!;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (ScorePartie score in listeScoresParties)
            {
                //a completer
                Console.WriteLine("\t{0} a parcouru {1} m. en {2} sec.", score.Nom, score.Distance, score.Duree);
            }
            Console.WriteLine();
            Console.Write("Enfonce ENTER pour retourner au menu");

            //Boucle pour vérifier si la touche est ENTER
            while (true)
            {
                //lire la touche sans écrire dans la console
                ConsoleKeyInfo touche = Console.ReadKey(true);
                if (touche.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════╗".PadLeft(46));
            Console.WriteLine("║  Meilleurs scores  ║".PadLeft(46));
            Console.WriteLine("╚════════════════════╝".PadLeft(46));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Jeu de disctance".PadLeft(24));
            Console.WriteLine("════════════════".PadLeft(24));

            listeScoresParties = LireScores(false, 5, TypesDeJeu.Distance, out mess)!;
            foreach (ScorePartie score in listeScoresParties)
            {
                Console.WriteLine("\t{0} a parcouru {1} m. en {2} sec.", score.Nom, score.Distance, score.Duree);
            }
            Console.WriteLine();
            Console.Write("Enfonce [ENTER] pour revenir au menu principale.");

            //Boucle pour vérifier si la touche est ENTER
            while (true)
            {
                //lire la touche sans écrire dans la console
                ConsoleKeyInfo touche = Console.ReadKey(true);
                if (touche.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
    }



    /// <summary>
    /// Dialogue principal
    /// Affiche le menu principal et l'aide
    /// En cas de bonus, fait choisir entre les types de jeu: Distance ou Temps 
    /// Vérifie la taille de l'écran
    /// Ne pas utiliser en tests unitaires
    /// </summary>
    /// <returns>true s'il faut jouer une partie, false s'il faut quitter le jeu. 
    /// Dans les autres case, on reste dans le menu et des sous-fonctions</returns>
    public bool MenuPrincipal()
    {
        // Caractères pour le cadre du menu
        // ╔ ═ ╗ ║ ╚  ╝

        //Affichage:
        Console.Clear();
        if (bonus == true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔════════════════════╗".PadLeft(46));
        Console.WriteLine("║  Course - Circuit  ║".PadLeft(46));
        Console.WriteLine("╚════════════════════╝".PadLeft(46));
        if (bonus == true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(Course.TETE_VOITURE.PadLeft(35));
        Console.WriteLine(Course.DEVANT_VOITURE_ROUE_FINE.PadLeft(37));
        Console.WriteLine(Course.MILLIEU_HAUT_VOITURE.PadLeft(36));
        Console.WriteLine(Course.MILLIEU_BAS_VOITURE.PadLeft(36));
        Console.WriteLine(Course.DERRIERE_VOITURE.PadLeft(37));
        if (bonus == true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" - Le jeu de distance [D] consiste à parcourir la plus grande distance sans sortie de route avec une voiture qui accélère jusqu'à 100 km/h");
            Console.WriteLine(" - Le jeu de temps [T] consiste à parcourir le plus vite possible  la distance de 1000 m avec une voiture qui accélère jusqu'à 100 km/h et redémarre à 5 km/h à chaque sortie de route");
            Console.WriteLine("   sans sortie de route avec une voiture qui accélère jusqu'à 100 km/h ");
            Console.WriteLine();
            Console.WriteLine(" Commandes au clavier:");
            Console.WriteLine(" - J, I et L ou ←, ↑ et → pour changer la direction.");
            Console.WriteLine(" - K ou ↓ pour freiner.");
            Console.WriteLine(" - ESC pour abandonner.");
            Console.WriteLine(" - ENTER ou ESPACE pour mettre en pause et redémarrer.");
            Console.WriteLine(" - Une touche enfoncée n'est lue qu'une fois!");
            Console.WriteLine();
            Console.Write(" [D] ou [T] pour le jeu choisi, [S] pour les scores et [Q] pour quitter. [ESC] pour revenir en arrière");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" - Le jeu de distance [D] consiste à parcourir la plus grande distance sans touché les bordures");
            Console.WriteLine("   sans sortie de route avec une voiture qui accélère jusqu'à 100 km/h ");
            Console.WriteLine();
            Console.WriteLine(" Commandes au clavier:");
            Console.WriteLine(" - J, I et L ou ←, ↑ et → pour changer la direction.");
            Console.WriteLine(" - K ou ↓ pour freiner.");
            Console.WriteLine(" - ESC pour abandonner.");
            Console.WriteLine(" - ENTER ou ESPACE pour mettre en pause et redémarrer.");
            Console.WriteLine(" - Une touche enfoncée n'est lue qu'une fois!");
            Console.WriteLine();
            Console.Write(" [D] pour démarrer le jeu, [S] pour les scores et [Q] pour quitter. [B] pour le BONUS");
        }
        //Boucle pour voir quel est la touche enfoncé par l'utilisateur
        while (true)
        {
            ConsoleKeyInfo touche = Console.ReadKey(true);
            if (bonus)
            {
                switch (touche.Key)
                {
                    case ConsoleKey.D:
                        typeDejeu = TypesDeJeu.Distance;
                        return true;
                    case ConsoleKey.S:
                        AfficherScores();
                        return MenuPrincipal();
                    case ConsoleKey.Q:
                        return false;
                    case ConsoleKey.T:
                        typeDejeu = TypesDeJeu.Temps;
                        return true;
                    case ConsoleKey.Escape:
                        bonus = false;
                        return MenuPrincipal();
                    default:
                        break;
                }
            }
            switch (touche.Key)
            {
                case ConsoleKey.D:
                    typeDejeu = TypesDeJeu.Distance;
                    return true;
                case ConsoleKey.S:
                    AfficherScores();
                    return MenuPrincipal();
                case ConsoleKey.Q:
                    return false;
                case ConsoleKey.B:
                    bonus = true;
                    return MenuPrincipal();
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Dessine le circuit à l'écran
    /// Elle dessine des lignes de caractères représentant l'herbe et la route
    /// sauf en haut au milieu, où elle affiche les données de vitesse, distance et durée
    /// et en bas où elle dessine la voiture avec les roues qui montrent la direction suivie
    /// A ne pas utiliser en tests unitaires
    /// </summary>
    public void DessinerParcours()
    {
        if (!PartieEnCours)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(affichage);
        }
        else
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(affichage);
            if (bonus) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(33, 4);
            Console.WriteLine("┌─────────────┐");
            Console.SetCursorPosition(33, 5);
            Console.WriteLine("│ " + vitesseActuelle + "   km/h  │");
            Console.SetCursorPosition(33, 6);
            Console.WriteLine("│ " + metreCompteur + "   mètres │");
            metreCompteur += 1;
            Console.SetCursorPosition(33, 7);
            DateTime compteurSecondes = DateTime.Now;
            tempsEntreDebut = compteurSecondes.Subtract(tempsJeu);
            Console.WriteLine("│ " + tempsEntreDebut.TotalSeconds.ToString("F2") + "  sec.│");
            Console.SetCursorPosition(33, 8);
            Console.WriteLine("└─────────────┘");
            if (typeDejeu == TypesDeJeu.Temps)
            {
                if (metreCompteur == 1000) PartieEnCours = false;
            }
            if (bonus) Console.ForegroundColor = ConsoleColor.Green;
        }
    }

    /// <summary>
    /// Commence le comptage du temps pour calculer et afficher un déplacement
    /// La variable global debutCalcul retient l'heure de départ
    /// Ne pas utiliser en tests unitaires
    /// </summary>
    public void DebutComptageTemps()
    {
        debutCalcul = DateTime.Now;
    }

    /// <summary>
    /// Termine le comptage du temps pour calculer et afficher un déplacement
    /// La variable globale dureeCalcul contient le résultat en ms
    /// Ce temps devra être retiré du sleep simulant la vitesse
    /// de façon à tenir compte du temps de calcul du déplacmene et de l'affichage
    /// Ne pas utiliser en tests unitaires
    /// </summary>
    public void FinComptageTemps()
    {
        DateTime fin = DateTime.Now;
        tempsCalcul = debutCalcul.Subtract(fin);
    }

    /// <summary>
    /// Fonction qui met en attente le joueur en fonction de la vitesse
    /// Note: sous Windows, l'affichage est plus lent, il faut donc un temps plus court !
    /// Ne pas utiliser en tests unitaires
    /// </summary>
    public void AttendreSelonLaVitesse()
    {
        int tempsDeBase = 140;
        int tempsAttente = tempsDeBase - vitesseActuelle - tempsCalcul.Milliseconds;

        if (tempsAttente > 0)
        {
            Thread.Sleep(tempsAttente);
        }
    }

    /// <summary>
    /// Affiche un message d'erreur en cas d'erreur à la lecture du parcours
    /// et attend qu'en enfonce ENTER avant de continuer avec un parcours par défaut
    /// Ne pas utiliser en tests unitaires
    /// </summary>
    public void AfficherMessageErreurParcours()
    {
        Console.Clear();
        Console.WriteLine("Le chargement du parcour n'a pu être correctement établie");
        Console.WriteLine(" - Le dossier contenant les parcours du jeu n'existe pas");
        Console.WriteLine(" - Le dossier existe mais les fichiers se trouvant dedans n'existent pas");
        Console.WriteLine(" - Lors du téléchargement du dossier pour le jeu, il y a peu avoir une erreur");
        Console.WriteLine(" - Le dossier a peu être supprimer");
        Console.WriteLine(" - ...");
        Console.WriteLine();
        Console.Write("Appuyer sur ENTER pour charger le parcour par défaut");
        while (true)
        {
            ConsoleKeyInfo touche = Console.ReadKey(true);
            if (touche.Key == ConsoleKey.Enter)
            {
                LireParcours(DEFAUT);
            }
        }

    }

    /// <summary>
    /// Affiche le dialogue de fin de partie qui propose une nouvelle partie ou la fin du jeu
    /// Renvoie true en cas de nouvelle partie, false si fin de jeu
    /// A ne pas utiliser en tests unitaires
    /// </summary>
    public void DialogueDeFinDePartie()
    {
        string nomScore = null!;
        bool nomPasValide = true;
        string mess = "";
        if (bonus) Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(29, 9);
        Console.WriteLine("╔═══════════════════╗");
        Console.SetCursorPosition(29, 10);
        Console.WriteLine("║     Game Over!    ║");
        Console.SetCursorPosition(29, 11);
        Console.WriteLine("║ Distance: " + metreCompteur + "  m. ║");
        Console.SetCursorPosition(29, 12);
        Console.WriteLine("║ Durée: " + tempsEntreDebut.TotalSeconds.ToString("F2") + " sec. ║");
        Console.SetCursorPosition(29, 13);
        Console.WriteLine("╚═══════════════════╝");
        Console.SetCursorPosition(0, 23);
        Console.Write(LIGNE_VIDE);
        while (nomPasValide)
        {
            Console.SetCursorPosition(0, 23);
            Console.Write("\tEntre ton nom : ");
            nomScore = Console.ReadLine()!;
            bool verifBon = true;
            while (nomScore == null || nomScore == "" || !verifBon)
            {
                Console.Clear();
                if (bonus) Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(affichage);
                if (bonus) Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(29, 9);
                Console.WriteLine("╔═══════════════════╗");
                Console.SetCursorPosition(29, 10);
                Console.WriteLine("║     Game Over!    ║");
                Console.SetCursorPosition(29, 11);
                Console.WriteLine("║ Distance: " + metreCompteur + "  m. ║");
                Console.SetCursorPosition(29, 12);
                Console.WriteLine("║ Durée: " + tempsEntreDebut.TotalSeconds.ToString("F2") + " sec.  ║");
                Console.SetCursorPosition(29, 13);
                Console.WriteLine("╚═══════════════════╝");
                Console.SetCursorPosition(0, 23);
                Console.Write(LIGNE_VIDE);
                Console.SetCursorPosition(0, 23);
                Console.Write("\tEntre ton nom sans caractères spéciaux : ");
                nomScore = Console.ReadLine()!;

                for (int index = 0; index < nomScore.Length; index++)
                {
                    char verifNom = nomScore[index];
                    if (!char.IsLetterOrDigit(verifNom))
                    {
                        verifBon = false;
                        break;
                    }
                    verifBon = true;
                }
            }
            nomPasValide = false;
        }
        decimal convertDuree = (decimal)tempsEntreDebut.TotalSeconds;
        AjouterScore(false, nomScore, metreCompteur, convertDuree, typeDejeu, out mess);
    }

    /// <summary>
    /// Lit s'il y a une entrée au clavier et applique le changement correspondant
    /// Attention: si une touche est maintenue enfoncée, elle n'est lue qu'une fois
    /// A ne pas utiliser en tests unitaires
    /// </summary>
    public void LireDeplacementAuClavier()
    {
        ///Fonction qui permet de gérer l'accélération liée aux touches et les directions par l'énum Directions.
        ///Pour la direction, on utilise deux énums, l'un qui va enregistrer les mouvements acctuels et l'autre qui enregistre les mouvements précédents.
        ///Pour l'accélération, on utilse le booléens correspondant à l'action désiré (accélérer ou ralentir) qui sont mis à true suivie de l'appel de la fonction Accelerer qui va augmenter ou réduire
        ///la vitesse.
        if (!Console.KeyAvailable)
        {
            directionAcctuelle = directionPrecedent;
            return;
        }
        ConsoleKeyInfo touche = Console.ReadKey(true);
        switch (touche.Key)
        {
            case ConsoleKey.J:
                directionAcctuelle = Directions.Gauche;
                directionPrecedent = Directions.Gauche;
                break;
            case ConsoleKey.L:
                directionAcctuelle = Directions.Droite;
                directionPrecedent = Directions.Droite;
                break;
            case ConsoleKey.I:
                directionAcctuelle = Directions.aucune;
                directionPrecedent = Directions.aucune;
                acelerer = true;
                Accelerer();
                acelerer = false;
                break;
            case ConsoleKey.K:
                directionAcctuelle = Directions.aucune;
                directionPrecedent = Directions.aucune;
                ralentir = true;
                Accelerer();
                ralentir = false;
                break;
            case ConsoleKey.LeftArrow:
                directionAcctuelle = Directions.Gauche;
                directionPrecedent = Directions.Gauche;
                break;
            case ConsoleKey.RightArrow:
                directionAcctuelle = Directions.Droite;
                directionPrecedent = Directions.Droite;
                break;
            case ConsoleKey.UpArrow:
                directionAcctuelle = Directions.aucune;
                directionPrecedent = Directions.aucune;
                acelerer = true;
                Accelerer();
                acelerer = false;
                break;
            case ConsoleKey.DownArrow:
                directionAcctuelle = Directions.aucune;
                directionPrecedent = Directions.aucune;
                ralentir = true;
                Accelerer();
                ralentir = false;
                break;
        }
    }
}