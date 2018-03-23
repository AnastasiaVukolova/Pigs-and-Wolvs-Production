using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color noMoney;
    public Vector3 positionOffset;
   

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;


    private BuildManager buildManager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        Debug.Log("Turret build! Money left: " + PlayerStats.Money.ToString());
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

       
        if(turret != null)
        {
            Debug.Log("We cant place this");
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

	void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (turret != null)
        {
            return;
        }

        if (buildManager.CanHover)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = noMoney;
        }
        
    }


    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //Get rid of the old turret
        Destroy(turret);


        //Build new
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret Upgraded!");
    }


    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        //TODO: Effect
        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
