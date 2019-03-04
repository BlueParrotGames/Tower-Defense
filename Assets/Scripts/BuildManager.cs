using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    Vector3 rangeOffset;

    private void Awake()
    {
        rangeOffset = new Vector3(0, 2, 0);

        if(instance != null)
        {
            Debug.LogError("There's too many BuildManagers in this scene! Tell a programmer!");
        }
        instance = this;
    }

    [Header("Effects")]
    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;
    private NodeManager selectedNode;
    [SerializeField] RenderRange rangeRenderer;

    [SerializeField] NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.cash >= turretToBuild.cost; } }
    public TurretBlueprint GetTurretBluePrint { get { return turretToBuild; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void SelectNode(NodeManager node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
        rangeRenderer.gameObject.SetActive(true);

        rangeRenderer.SetPoints(node.turret.GetComponent<Turret>().range);
        rangeRenderer.transform.position = node.turret.transform.position + rangeOffset;
    }

    public void DeselectNode()
    {
        rangeRenderer.gameObject.SetActive(false);
        selectedNode = null;
        nodeUI.Hide();
    }
}
