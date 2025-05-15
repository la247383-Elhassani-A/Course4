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
    private List<string> listTest1;
    private List<string> listTest2;

    [TestInitialize]
    public void setup()
    {
        objet = new Course();
        listTest1 = new();
        listTest2 = new();
        string folderName = "../../../../Parcours";
        objet.ChargerParcoursAuHasard(folderName);
        for (int index = 0; index < 24; index++)
        {
            listTest1.Add(objet.parcours[index]);
        }
        objet.terrain = listTest2;
    }
    [TestMethod]
    public void testDossierExiste()
    {
        string folderName = "../../../../Parcours";
        bool testExiste = true;
        Assert.AreEqual(testExiste, objet.ChargerParcoursAuHasard(folderName));
    }
    [TestMethod]
    public void testDossierExistePas()
    {
        string folderName = "../../../Parcours";
        bool testExistePas = false;
        Assert.AreEqual(testExistePas, objet.ChargerParcoursAuHasard(folderName));
    }
    [TestMethod]
    public void testLireParcoursBon()
    {
        string fichier = "../../../../Parcours/fichier1.txt";
        bool testFichierBon = true;
        Assert.AreEqual(testFichierBon, objet.LireParcours(fichier));
    }
    [TestMethod]
    public void testLireParcoursPasBon()
    {
        string fichier = "../../../../Parcours/fichier1000.txt";
        bool testFichierPasBon = false;
        Assert.AreEqual(testFichierPasBon, objet.LireParcours(fichier));
    }
    [TestMethod]
    public void testLireParcoursLigne61()
    {
        string fichier = "fichierTestLigneEnTrop61.txt";
        bool testFichierPasBon = false;
        Assert.AreEqual(testFichierPasBon, objet.LireParcours(fichier));
    }
    [TestMethod]
    public void testLireParcoursLigne103()
    {
        string fichier = "103ligne.txt";
        bool testFichierPasBon = false;
        Assert.AreEqual(testFichierPasBon, objet.LireParcours(fichier));
    }
    [TestMethod]
    public void testInitialiserParcoursBon()
    {
        bool verif = true;
        objet.InitialiserPartie();
        for (int index = 0; index < listTest1.Count; index++)
        {
            string ligne1 = listTest1[index];
            string ligne2 = listTest2[index];

            for (index = 0; index < ligne1.Length; index++)
            {
                char carac1 = ligne1[index];
                char carac2 = ligne2[index];
                if (carac1 != carac2) verif = false;
            }
        }
        Assert.IsTrue(verif);
    }
    [TestMethod]
    public void testDeclarationCompteur()
    {
        int testCompteur = 24;
        objet.InitialiserPartie();
        Assert.AreEqual(testCompteur,objet.compteurAvancerRoute);
    }
     [TestMethod]
    public void testDeclarationTerrain()
    {
        int testCompteur = 24;
        objet.InitialiserPartie();
        Assert.AreEqual(testCompteur,objet.compteurAvancerRoute);
    }
}