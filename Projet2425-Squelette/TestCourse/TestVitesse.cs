using System.Runtime.InteropServices.Marshalling;
using CourseVoiture;
namespace TestCourseVoiture;

/// <summary>
/// Tests des calculs de la vitesse (accélération)
/// </summary>
[TestClass]
[DoNotParallelize]
public class TestVitesse
{
    private Course? objet;
    [TestInitialize]
    public void setup()
    {
        objet = new Course();
        objet.acelerer = false;
        objet.ralentir = false;
    }
    [TestMethod]
    public void TestAccelerationMinSansAccel()
    {
        objet.vitesseActuelle = Course.VITESSE_MIN;
        objet.Accelerer();
        Assert.AreEqual(8, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAcceleration10SansAccel()
    {
        objet.vitesseActuelle = 10;
        objet.Accelerer();
        Assert.AreEqual(13, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAcceleration50SansAccel()
    {
        objet.vitesseActuelle = 50;
        objet.Accelerer();
        Assert.AreEqual(53, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAcceleration90SansAccel()
    {
        objet.vitesseActuelle = 90;
        objet.Accelerer();
        Assert.AreEqual(93, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAccelerationMaxSansAccel()
    {
        objet.vitesseActuelle = Course.VITESSE_MAX;
        objet.Accelerer();
        Assert.AreEqual(Course.VITESSE_MAX, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAccelerationMinAvecAccel()
    {
        objet.vitesseActuelle = Course.VITESSE_MIN;
        objet.acelerer = true;
        int accelerationTouche = Convert.ToInt32(2 / Math.Log10(objet.vitesseActuelle));
        int reponse = objet.vitesseActuelle + accelerationTouche;
        objet.Accelerer();
        Assert.AreEqual(reponse, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAcceleration10AvecAccel()
    {
        objet.vitesseActuelle = 10;
        objet.acelerer = true;
        int accelerationTouche = Convert.ToInt32(2 / Math.Log10(objet.vitesseActuelle));
        int reponse = objet.vitesseActuelle + accelerationTouche;
        objet.Accelerer();
        Assert.AreEqual(reponse, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAcceleration50AvecAccel()
    {
        objet.vitesseActuelle = 50;
        objet.acelerer = true;
        int accelerationTouche = Convert.ToInt32(2 / Math.Log10(objet.vitesseActuelle));
        int reponse = objet.vitesseActuelle + accelerationTouche;
        objet.Accelerer();
        Assert.AreEqual(reponse, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAcceleration90AvecAccel()
    {
        objet.vitesseActuelle = 90;
        objet.acelerer = true;
        int accelerationTouche = Convert.ToInt32(2 / Math.Log10(objet.vitesseActuelle));
        int reponse = objet.vitesseActuelle + accelerationTouche;
        objet.Accelerer();
        Assert.AreEqual(reponse, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAccelerationMaxAvecAccel()
    {
        objet.vitesseActuelle = 100;
        objet.acelerer = true;
        objet.Accelerer();
        Assert.AreEqual(Course.VITESSE_MAX, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAcceleration101()
    {
        objet.vitesseActuelle = 101;
        objet.acelerer = true;
        objet.Accelerer();
        Assert.AreEqual(Course.VITESSE_MAX, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestAcceleration0()
    {
        objet.vitesseActuelle = 0;
        objet.Accelerer();
        Assert.AreEqual(8, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestDecelerationMin()
    {
        objet.vitesseActuelle = Course.VITESSE_MIN;
        objet.ralentir = true;
        objet.Accelerer();
        Assert.AreEqual(Course.VITESSE_MIN, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestDeceleration10()
    {
        objet.vitesseActuelle = 10;
        objet.ralentir = true;
        objet.Accelerer();
        Assert.AreEqual(5, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestDeceleration50()
    {
        objet.vitesseActuelle = 50;
        objet.ralentir = true;
        int accelerationTouche = Convert.ToInt32(2 / Math.Log10(objet.vitesseActuelle));
        int reponse = 50 - (accelerationTouche + 5);
        objet.Accelerer();
        Assert.AreEqual(reponse, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestDeceleration90()
    {
        objet.vitesseActuelle = 90;
        objet.ralentir = true;
        int accelerationTouche = Convert.ToInt32(2 / Math.Log10(objet.vitesseActuelle));
        int reponse = 90 - (accelerationTouche + 5);
        objet.Accelerer();
        Assert.AreEqual(reponse, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestDecelerationMax()
    {
        objet.vitesseActuelle = Course.VITESSE_MAX;
        objet.ralentir = true;
        int accelerationTouche = Convert.ToInt32(2 / Math.Log10(objet.vitesseActuelle));
        int reponse = 100 - (accelerationTouche + 5);
        objet.Accelerer();
        Assert.AreEqual(reponse, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestDeceleration101()
    {
        objet.vitesseActuelle = 101;
        objet.ralentir = true;
        int accelerationTouche = Convert.ToInt32(2 / Math.Log10(Course.VITESSE_MAX));
        int reponse = 100 - (accelerationTouche + 5);
        objet.Accelerer();
        Assert.AreEqual(reponse, objet.vitesseActuelle);
    }
    [TestMethod]
    public void TestDeceleration0()
    {
        objet.vitesseActuelle = 0;
        objet.ralentir = true;
        objet.Accelerer();
        Assert.AreEqual(5, objet.vitesseActuelle);
    }
}