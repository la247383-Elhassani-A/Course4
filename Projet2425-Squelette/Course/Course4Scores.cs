using MySqlConnector;

namespace CourseVoiture;

public partial class Course
{
    /// <summary>
    /// Structure pour stocker une partie (nom, distance, durée, type de jeu si bonus)
    /// </summary>
    public struct ScorePartie()
    {
        public string Nom;
        public int Distance;
        public decimal Duree;
    }

    /// <summary>
    /// Efface la base de données
    /// </summary>
    /// <param name="DBDeTest">Indique si c'est la DB de test ou de production</param>
    /// <param name="messageDerreur">Pour renvoyer un texte explicatif s'il y a un erreur</param>
    /// <returns>Renvoie true si OK, false s'il y a une erreur</returns>
    public static bool EffacerDB(bool DBDeTest, out string messageDerreur)
    {
        //Variables
        messageDerreur = "";
        string dbTest = "LA247383";
        string dbProd = "la247383";
        string nomDB;

        //Code
        if (DBDeTest == true)
        {
            nomDB = dbTest;
        }
        else
        {
            nomDB = dbProd;
        }
        try
        {
            MySqlConnectionStringBuilder builder = new()
            {
                Server = "127.0.0.1",
                UserID = "root",
                Database = nomDB
            };
            MySqlConnection connection = new(builder.ConnectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $"USE {nomDB};";
            command.ExecuteNonQuery();
            command.CommandText = $"DROP DATABASE IF EXISTS {nomDB};";
            command.ExecuteNonQuery();
        }
        catch (Exception erreur)
        {
            //Le dollar permet de mettre le message erreur dans un string
            messageDerreur = $"La base de données n'a pu être supprimée. Erreur de type : {erreur.Message}";
            return false;
        }
        return true;
    }

    /// <summary>
    /// Créer la base de donnée
    /// </summary>
    /// <param name="DBDeTest">Indique si c'est la DB de test ou de production</param>
    /// <param name="messageDerreur">Pour renvoyer un texte explicatif s'il y a un erreur</param>
    /// <returns>Renvoie true si OK, false s'il y a une erreur</returns>
    public static bool CreerDB(bool DBDeTest, out string messageDerreur)
    {
        //Variables
        messageDerreur = "";
        string dbTest = "LA247383";
        string dbProd = "la247383";
        string nomDB;

        //Code
        if (DBDeTest == true)
        {
            nomDB = dbTest;
        }
        else
        {
            nomDB = dbProd;
        }
        try
        {
            MySqlConnectionStringBuilder builder = new()
            {
                Server = "127.0.0.1",
                UserID = "root",
            };
            MySqlConnection connection = new(builder.ConnectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $"CREATE DATABASE IF NOT EXISTS {nomDB};";
            command.ExecuteNonQuery();
            command.CommandText = $"USE {nomDB};";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Joueur (ID_JOUEUR INT AUTO_INCREMENT,nom VARCHAR(50) NOT NULL, PRIMARY KEY(ID_JOUEUR), UNIQUE(nom));";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Score (ID_SCORE INT AUTO_INCREMENT, distance INT NOT NULL, temps DECIMAL(15,3) NOT NULL, typepartie CHAR(1) NOT NULL, ID_JOUEUR INT NOT NULL, PRIMARY KEY(ID_SCORE), FOREIGN KEY(ID_JOUEUR) REFERENCES Joueur(ID_JOUEUR));";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT IGNORE INTO Joueur (nom) VALUES ('BOT');";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO Score (distance,temps,typepartie,ID_JOUEUR) VALUES (100,2.456,'D',1);";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO Score (distance,temps,typepartie,ID_JOUEUR) VALUES (100,2.456,'T',1);";
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception erreur)
        {
            //Le dollar permet de mettre le message erreur dans un string
            messageDerreur = $"La base de données n'a pu être créer. Erreur de type : {erreur.Message}";
            return false;
        }
        return true;
    }


    /// <summary>
    /// Renvoie d'identifiant d'un joueur 
    /// </summary>
    /// <param name="connexion">Connexion à la DB</param>
    /// <param name="nom">Nom du jour</param>
    /// <returns>Renvoie l'ID du joureur (clé primaire)</returns>
    private static int LireIDJoueur(MySqlConnection connexion, string nom)
    {
        try
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = $"SELECT ID_JOUEUR FROM Joueur WHERE nom LIKE '{nom}'";
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int identifiant = reader.GetInt32("ID_JOUEUR");
                reader.Close();
                return identifiant;
            }
            reader.Close();
            return -1;
        }
        catch (Exception)
        {
            Console.WriteLine("Erreur lors de la lecture de l'Id du joueur.");
        }
        return -1;
    }

    /// <summary>
    /// Ajoute un score à la base de données
    /// </summary>
    /// <param name="DBDeTest">Indique si c'est la DB de test ou de production</param>
    /// <param name="nom">Nom du joueur</param>
    /// <param name="distance">Distance parcourue en m</param>
    /// <param name="duree">Durée en milisecondes</param>
    /// <param name="typeDeJeu">Type de jeu (pour le bonus uniquement)</param>
    /// <param name="messageDerreur">Pour renvoyer un texte explicatif s'il y a un erreur</param>
    /// <returns>Renvoie true si OK, false s'il y a une erreur</returns>
    public static bool AjouterScore(bool DBDeTest, string nom, int distance, decimal duree, TypesDeJeu typeDeJeu, out string messageDerreur)
    {
        //Variables
        messageDerreur = "";
        string dbTest = "LA247383";
        string dbProd = "la247383";
        string nomDB;
        char typeDEJEU =
        typeDeJeu == TypesDeJeu.Distance ? 'D' : 'T';

        //Code
        if (DBDeTest == true)
        {
            nomDB = dbTest;
        }
        else
        {
            nomDB = dbProd;
        }
        try
        {
            MySqlConnectionStringBuilder builder = new()
            {
                Server = "127.0.0.1",
                UserID = "root",
                Database = nomDB
            };
            MySqlConnection connexion = new(builder.ConnectionString);
            connexion.Open();
            MySqlCommand command = connexion.CreateCommand();
            int id = LireIDJoueur(connexion, nom);
            if (id == -1)
            {
                command.CommandText = $"INSERT INTO Joueur (nom) VALUES ('{nom}');";
                command.ExecuteNonQuery();
                command.CommandText = $"SELECT ID_JOUEUR FROM Joueur WHERE nom LIKE '{nom}';";
                MySqlDataReader reader = command.ExecuteReader();
                int ID = 0;
                if (reader.Read())
                {
                    ID = reader.GetInt32("ID_JOUEUR");
                    reader.Close();
                }
                command.CommandText = $"INSERT INTO Score (distance,temps,typepartie,ID_JOUEUR) VALUES ({distance},{duree},'{typeDEJEU}',{ID});";
                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = $"INSERT INTO Score (distance,temps,typepartie,ID_JOUEUR) VALUES ({distance},{duree},'{typeDEJEU}',{id});";
                command.ExecuteNonQuery();
            }
            connexion.Close();
        }
        catch (Exception erreur)
        {
            messageDerreur = $"Erreur lors de l'ajout des scores dans la base de donées. Type d'erreur : {erreur.Message}";
            return false;
        }
        return true;
    }

    /// <summary>
    /// Renvoie une liste de meilleurs scores
    /// </summary>
    /// <param name="DBDeTest">Indique si c'est la DB de test ou de production</param>
    /// <param name="nombreDeScores">Nombre de scores maximum à renvoyer</param>
    /// <param name="typeDeJeu">Type de jeu (pour le bonus uniquement)</param>
    /// <param name="messageDerreur">Pour renvoyer un texte explicatif s'il y a un erreur</param>
    /// <returns>Une Liste typée de structures ScorePartie</returns>
    public static List<ScorePartie>? LireScores(bool DBDeTest, int nombreDeScores, TypesDeJeu typeDeJeu, out string messageDerreur)
    {
        //Variables
        messageDerreur = "";
        nombreDeScores = 5;
        string dbTest = "LA247383";
        string dbProd = "la247383";
        string nomDB;
        char typeDEJEU =
        typeDeJeu == TypesDeJeu.Distance ? 'D' : 'T';
        List<ScorePartie> listeDesScores;
        listeDesScores = [];

        //Code
        if (DBDeTest == true)
        {
            nomDB = dbTest;
        }
        else
        {
            nomDB = dbProd;
        }
        try
        {
            MySqlConnectionStringBuilder builder = new()
            {
                Server = "127.0.0.1",
                UserID = "root",
                Database = nomDB
            };
            MySqlConnection connexion = new(builder.ConnectionString);
            connexion.Open();
            MySqlCommand command = connexion.CreateCommand();
            if (typeDeJeu == TypesDeJeu.Distance)
            {
                command.CommandText = $"SELECT Joueur.nom, Score.distance, Score.temps FROM Score INNER JOIN Joueur ON Score.ID_JOUEUR = Joueur.ID_JOUEUR WHERE typepartie = 'D' ORDER BY distance DESC, temps ASC LIMIT 5;";
            }
            //Le order by distance est inutile car chaque parties se finissent après 1000 mètres.
            else
            {
                command.CommandText = $"SELECT Joueur.nom, Score.distance, Score.temps FROM Score INNER JOIN Joueur ON Score.ID_JOUEUR = Joueur.ID_JOUEUR WHERE typepartie = 'T' ORDER BY distance DESC, temps ASC LIMIT 5;";
            }
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string nomScore = reader.GetString("nom");
                int distanceScore = reader.GetInt32("distance");
                decimal tempsScore = reader.GetDecimal("temps");
                ScorePartie joueur = new()
                {
                    Nom = nomScore,
                    Distance = distanceScore,
                    Duree = tempsScore
                };
                listeDesScores.Add(joueur);
            }
            reader.Close();
            connexion.Close();
            return listeDesScores;
        }
        catch (Exception erreur)
        {
            messageDerreur = $"Erreur lors de la lecture des scores dans la base de donées. Type d'erreur : {erreur.Message}";
        }
        return null;
    }
}