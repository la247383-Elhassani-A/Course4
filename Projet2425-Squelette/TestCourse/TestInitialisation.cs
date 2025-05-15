using CourseVoiture;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
namespace TestCourseVoiture;

/// <summary>
/// Tests de l'initialisation des parties (lecture des circuits, etc.)
/// </summary>
[TestClass]
[DoNotParallelize]
public class TestInitialisation
{
    private Course? objet;
    
    [TestInitialize]
    public void setup()
    {
        objet = new Course();
        string folderName = "../../../../Parcours";
        objet.ChargerParcoursAuHasard(folderName);

    }
    [TestMethod]
    public void testDeclarationCompteur()
    {
        int testCompteur = 24;
        objet.InitialiserPartie();
        Assert.AreEqual(testCompteur, objet.compteurAvancerRoute);
    }
    [TestMethod]
    public void testDeclarationDecalerVoiture()
    {
        int testdecaler = 0;
        objet.InitialiserPartie();
        Assert.AreEqual(testdecaler, objet.decalerVoiture);
    }
    [TestMethod]
    public void testDeclarationMetreCompteur()
    {
        int testCompteur = 0;
        objet.InitialiserPartie();
        Assert.AreEqual(testCompteur, objet.metreCompteur);
    }
    [TestMethod]
    public void testDeclarationDirectionsActuelle()
    {
        objet.InitialiserPartie();
        Assert.AreEqual(objet.directionAcctuelle = Course.Directions.aucune, objet.directionAcctuelle = Course.Directions.aucune);
    }
    [TestMethod]
    public void testDeclarationDirectionsPrecedente()
    {
        objet.InitialiserPartie();
        Assert.AreEqual(objet.directionPrecedent = Course.Directions.aucune, objet.directionPrecedent = Course.Directions.aucune);
    }
}