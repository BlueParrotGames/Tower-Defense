using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private NodeManager target;
    [SerializeField] GameObject ui;

    [SerializeField] Text upgradeCostText;
    [SerializeField] Button upgradeButton;

    [SerializeField] Text sellAmountText;

    public void SetTarget(NodeManager t)
    {
        target = t;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCostText.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCostText.text = "Upgraded";
            upgradeButton.interactable = false;
        }

        sellAmountText.text = "$" + target.turretBlueprint.GetSellValue().ToString();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
