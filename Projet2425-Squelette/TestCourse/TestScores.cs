using CourseVoiture;
namespace TestCourseVoiture;

/// <summary>
/// Tests relatifs aux scores:
/// - Création de la base de données
/// - Écriture des joueurs et des scores dans la base de données
/// - Lecture des joueurs et des scores de la base de données
/// Ces tests sont exécutés avec une base de données de test qui est différente 
/// de la base de donnée du jeu (pour ne pas perturber ou perdre les résultats des jeux)
/// </summary>
[TestClass]
[DoNotParallelize]
public class TestScores
{
    private Course? objet;
    [TestInitialize]
    public void setup()
    {
        objet = new Course();
    }
    [TestCleanup]
    public void effacer()
    {
        string mess;
        Course.EffacerDB(true, out mess);
    }
    [TestMethod]
    public void TestCreerBaseDeDonnee()
    {
        string mess;
        Assert.IsTrue(Course.CreerDB(true, out mess));
    }
    [TestMethod]
    public void TestEffacerBaseDeDonnee()
    {
        string mess;
        Course.CreerDB(true, out mess);
        Assert.IsTrue(Course.EffacerDB(true, out mess));
    }
    [TestMethod]
    public void TestAjouterScores()
    {
        string testNom = "Alice";
        int testDistance = 100;
        decimal testDuree = 6.2133242523M;
        string mess;
        Course.AjouterScore(true, testNom, testDistance, testDuree, objet.typeDejeu = Course.TypesDeJeu.Distance, out mess);
    }
    [TestMethod]
    public void TestLireScores()
    {
        string testNom = "Alice";
        int testDistance = 100;
        decimal testDuree = 6.2133242523M;
        string mess;
        for (int index = 0; index < 4; index++)
        {
            Course.AjouterScore(true, testNom, testDistance + 1, testDuree + 1.234M, objet.typeDejeu = Course.TypesDeJeu.Distance, out mess);
        }
        Course.LireScores(true, 5, objet.typeDejeu = Course.TypesDeJeu.Distance,out mess);
    }
}