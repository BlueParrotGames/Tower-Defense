using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeManager : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color noMoneycolor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;
    public TurretBlueprint turretBlueprint;
    public bool isUpgraded = false;

    Color defaultColor;
    Renderer render;

    BuildManager buildManager;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        defaultColor = render.material.color;

    }

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }


        BuildTurret(buildManager.GetTurretBluePrint);
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.cash < blueprint.cost)
        {
            return;
        }

        PlayerStats.cash -= blueprint.cost;

        GameObject t = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = t;

        turretBlueprint = blueprint;

        GameObject b = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);

        StartCoroutine(WaitForDestroy(b));
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.cash < turretBlueprint.upgradeCost)
        {
            return;
        }

        PlayerStats.cash -= turretBlueprint.upgradeCost;

        Destroy(turret);

        GameObject t = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = t;

        isUpgraded = true;

        GameObject b = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);

        StartCoroutine(WaitForDestroy(b));
    }

    public void SellTurret()
    {
        PlayerStats.cash += turretBlueprint.GetSellValue();

        Destroy(turret);
        turretBlueprint = null;
        // instantiate effect
    }

    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            render.material.color = hoverColor;
        }
        else
        {
            render.material.color = noMoneycolor;
        }

        //play holy particle effect surrounding node
    }

    private void OnMouseExit()
    {
        render.material.color = defaultColor;
    }

    IEnumerator WaitForDestroy(GameObject g)
    {
        yield return new WaitForSeconds(1);

        Destroy(g);
    }
}
