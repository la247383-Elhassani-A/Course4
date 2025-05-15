using CourseVoiture;
namespace TestCourseVoiture;

/// <summary>
/// Tests de détection des collisions entre la voiture et le décor
/// </summary>
[TestClass]
[DoNotParallelize]
public class TestCollisions
{
    private Course? objet;
    [TestInitialize]
    public void setup()
    {
        objet = new Course();

    }
    [TestMethod]
    public void TestCollisionsGauche()
    {
        string folderName = "../../../../Parcours";
        objet.ChargerParcoursAuHasard(folderName);
        Assert.IsTrue(objet.ChargerParcoursAuHasard(folderName));
        objet.InitialiserPartie();
        //Une boucle qui va aller a gauche tant qu il ne touche pas la bordure (il la touchera 100%) et on avance le parcours car la bordure peut changer
        objet.decalerVoiture = 0;
        for (int index = 0; index < 4; index++)
        {
            objet.directionAcctuelle = Course.Directions.Gauche;
            objet.DeplacerVoiture();
            objet.decalerVoiture--;
            objet.AvancerRoute();
        }
        Assert.IsTrue(objet.PartieEnCours == false);
    }
    [TestMethod]
    public void TestCollisionsDroite()
    {
        string folderName = "../../../../Parcours";
        objet.ChargerParcoursAuHasard(folderName);
        Assert.IsTrue(objet.ChargerParcoursAuHasard(folderName));
        objet.InitialiserPartie();
        objet.decalerVoiture = 0;
        for (int index = 0; index < 4; index++)
        {
            objet.directionAcctuelle = Course.Directions.Droite;
            objet.DeplacerVoiture();
            objet.decalerVoiture++;
            objet.AvancerRoute();
        }
        Assert.IsTrue(objet.PartieEnCours == false);
    }
     [TestMethod]
    public void TestCollisionsToutDroit()
    {
        string folderName = "../../../../Parcours";
        objet.ChargerParcoursAuHasard(folderName);
        Assert.IsTrue(objet.ChargerParcoursAuHasard(folderName));
        objet.InitialiserPartie();
        objet.decalerVoiture = 0;
        for (int index = 0; index < 70; index++)
        {
            objet.directionAcctuelle = Course.Directions.aucune;
            objet.DeplacerVoiture();
            objet.AvancerRoute();
        }
        Assert.IsTrue(objet.PartieEnCours == false);
    }
}