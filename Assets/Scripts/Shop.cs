using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager buildManager;
    
    void Start()
    {
        buildManager = BuildManager.instance;
    }
	public void SelectStandartTurret()
    {
        Debug.Log("Standart turret");
        buildManager.SelectTurretToBuild(standartTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile lancher esletced");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer esletced");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
