using System.Security.Cryptography.X509Certificates;
using CourseVoiture;
namespace TestCourseVoiture;

/// <summary>
/// Tests des déplacements (voiture qui bouge dans le circuit qui se décale)
/// </summary>
[TestClass]
[DoNotParallelize]
public class TestDeplacements
{
    private Course? objet;
    private List<string> terrainTest;
    private List<string> terraininitiale;
    private string ligne;

    [TestInitialize]
    public void setup()
    {
        objet = new Course();
        terrainTest = new();
        terraininitiale = new();
        objet.test = true;
        ligne = "░░░░░░░░░░░░░░░░░░░░░█                 █░░░░░░░░░░░░░░░░░░░░";
        for (int index = 0; index < 24; index++)
        {
            terrainTest.Add(ligne);
        }
        objet.terrain = terrainTest;
        foreach (string element in terrainTest)
        {
            terraininitiale.Add(element);
        }
    }
    [TestMethod]
    public void TestDeplacementGaucheUnMouvement()
    {
        objet.decalerVoiture = 0;
        // faire - 1, car les indice vont de 0 a 59;
        int indiceRoueDeBase = 28;
        objet.directionAcctuelle = Course.Directions.Gauche;
        objet.DeplacerVoiture();
        terrainTest.Clear();
        foreach (string element in objet.terraintest)
        {
            terrainTest.Add(element);
        }
        int indiceRoueApres = terrainTest[19].IndexOf(Course.ROUE_GAUCHE_TOURNER);
        Assert.AreEqual(indiceRoueDeBase - 1, indiceRoueApres);
    }
    [TestMethod]
    public void TestDeplacementGaucheDeuxMouvement()
    {
        objet.decalerVoiture = 0;
        objet.directionAcctuelle = Course.Directions.Gauche;
        // faire - 1, car les indice vont de 0 a 59;
        int indiceRoueDeBase = 28;
        objet.DeplacerVoiture();
        objet.terraintest.Clear();
        objet.directionAcctuelle = Course.Directions.Gauche;
        objet.DeplacerVoiture();
        terrainTest.Clear();
        foreach (string element in objet.terraintest)
        {
            terrainTest.Add(element);
        }
        int indiceRoueApres = terrainTest[19].IndexOf(Course.ROUE_GAUCHE_TOURNER);
        Assert.AreEqual(indiceRoueDeBase - 2, indiceRoueApres);
    }
    [TestMethod]
    public void TestDeplacementDroiteUnMouvement()
    {
        objet.directionAcctuelle = Course.Directions.Droite;
        // faire + 1, car les indice vont de 0 a 59;
        int indiceRoueDeBase = 28;
        objet.DeplacerVoiture();
        terrainTest.Clear();
        foreach (string element in objet.terraintest)
        {
            terrainTest.Add(element);
        }
        int indiceRoueApres = terrainTest[19].IndexOf(Course.ROUE_DROITE_TOURNER);
        Assert.AreEqual(indiceRoueDeBase + 1, indiceRoueApres);
    }
    [TestMethod]
    public void TestDeplacementDroiteDeuxMouvement()
    {

        objet.directionAcctuelle = Course.Directions.Droite;
        // faire + 1, car les indice vont de 0 a 59;
        int indiceRoueDeBase = 28;
        objet.DeplacerVoiture();
        objet.terraintest.Clear();
        objet.directionAcctuelle = Course.Directions.Droite;
        objet.DeplacerVoiture();
        terrainTest.Clear();
        foreach (string element in objet.terraintest)
        {
            terrainTest.Add(element);
        }
        int indiceRoueApres = terrainTest[19].IndexOf(Course.ROUE_DROITE_TOURNER);
        Assert.AreEqual(indiceRoueDeBase + 2, indiceRoueApres);
    }
    [TestMethod]
    public void testAvancerRouteBon()
    {
        string folderName = "../../../../Parcours";
        bool verifEndroit = false;
        terrainTest.Clear();

        for (int index = 0; index < 24; index++)
        {
            ligne = "░░░░░░░░░░░░░░░░░░░░░█                 █░░░░░░░░░░░░░░░░░░░░";
            if (index == 12)
            {
                string gauche = ligne.Substring(0, 30);
                string testString = "o";
                string droite = ligne.Substring(30 + testString.Length);
                ligne = gauche + testString + droite;
                terrainTest.Add(ligne);
            }
            else
            {
                terrainTest.Add(ligne);
            }
        }
        objet.ChargerParcoursAuHasard(folderName);
        objet.AvancerRoute();
        if (terrainTest[11].Contains("o") && !terrainTest[12].Contains("o"))
        {
            verifEndroit = true;
        }
        Assert.IsTrue(verifEndroit);
    }
    [TestMethod]
    public void testAvancerRoutePasBon()
    {
        string folderName = "../../../../Parcours";
        bool verifEndroit = false;
        terrainTest.Clear();

        for (int index = 0; index < 24; index++)
        {
            ligne = "░░░░░░░░░░░░░░░░░░░░░█                 █░░░░░░░░░░░░░░░░░░░░";
            if (index == 12)
            {
                string gauche = ligne.Substring(0, 30);
                string testString = "o";
                string droite = ligne.Substring(30 + testString.Length);
                ligne = gauche + testString + droite;
                terrainTest.Add(ligne);
            }
            else
            {
                terrainTest.Add(ligne);
            }
        }
        objet.ChargerParcoursAuHasard(folderName);
        objet.AvancerRoute();
        if (terrainTest[12].Contains("o") && !terrainTest[11].Contains("o"))
        {
            verifEndroit = true;
        }
        Assert.IsFalse(verifEndroit);
    }
    [TestMethod]
    public void TestDeplacementGauchePuisDroite()
    {

        objet.directionAcctuelle = Course.Directions.Gauche;
        // faire + 1, car les indice vont de 0 a 59;
        int indiceRoueDeBase = 28;
        objet.DeplacerVoiture();
        objet.terraintest.Clear();
        objet.directionAcctuelle = Course.Directions.Droite;
        objet.DeplacerVoiture();
        terrainTest.Clear();
        foreach (string element in objet.terraintest)
        {
            terrainTest.Add(element);
        }
        int indiceRoueApres = terrainTest[19].IndexOf(Course.ROUE_DROITE_TOURNER);
        Assert.AreEqual(indiceRoueDeBase, indiceRoueApres);
    }
    [TestMethod]
    public void TestDeplacementDroitePuisGauche()
    {
        objet.directionAcctuelle = Course.Directions.Droite;
        // faire + 1, car les indice vont de 0 a 59;
        int indiceRoueDeBase = 28;
        objet.DeplacerVoiture();
        objet.terraintest.Clear();
        objet.directionAcctuelle = Course.Directions.Gauche;
        objet.DeplacerVoiture();
        terrainTest.Clear();
        foreach (string element in objet.terraintest)
        {
            terrainTest.Add(element);
        }
        int indiceRoueApres = terrainTest[19].IndexOf(Course.ROUE_GAUCHE_TOURNER);
        Assert.AreEqual(indiceRoueDeBase, indiceRoueApres);
    }
    [TestMethod]
    public void TestDeplacementDroitePuisToutDroit()
    {
        objet.directionAcctuelle = Course.Directions.Droite;
        // faire + 1, car les indice vont de 0 a 59;
        int indiceRoueDeBase = 28;
        objet.DeplacerVoiture();
        objet.terraintest.Clear();
        objet.directionAcctuelle = Course.Directions.aucune;
        objet.DeplacerVoiture();
        terrainTest.Clear();
        foreach (string element in objet.terraintest)
        {
            terrainTest.Add(element);
        }
        int indiceRoueApres = terrainTest[19].IndexOf(Course.ROUE_GAUCHE);
        Assert.AreEqual(indiceRoueDeBase + 1, indiceRoueApres);
    }
    [TestMethod]
    public void TestDeplacementGauchePuisToutDroit()
    {
        objet.directionAcctuelle = Course.Directions.Gauche;
        // faire + 1, car les indice vont de 0 a 59;
        int indiceRoueDeBase = 28;
        objet.DeplacerVoiture();
        objet.terraintest.Clear();
        objet.directionAcctuelle = Course.Directions.aucune;
        objet.DeplacerVoiture();
        terrainTest.Clear();
        foreach (string element in objet.terraintest)
        {
            terrainTest.Add(element);
        }
        int indiceRoueApres = terrainTest[19].IndexOf(Course.ROUE_GAUCHE);
        Assert.AreEqual(indiceRoueDeBase - 1, indiceRoueApres);
    }
}
