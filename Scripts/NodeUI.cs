using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

	private Node node;

	public GameObject ui;

	public Text upgradeCost;
	public Button upgradeButton;

	public Text sellCost;

	public void SetNode(Node _node)
	{
		node = _node;
		transform.position = node.transform.position + node.offset;

		if (!node.isUpgraded) {
			upgradeCost.text = node.turretScheme.upgradeCost + "$";
			upgradeButton.interactable = true;
		} else 
		{
			upgradeCost.text = "DONE";
			upgradeButton.interactable = false; 
		}

		sellCost.text = node.turretScheme.GetSellAmount () + "$";
		ui.SetActive (true);
	}

	public void Hide()
	{
		ui.SetActive (false);
	}

	public void Upgrade()
	{
		node.UpgradeTurret ();
		Builder.builderInstance.DeselectNode ();
	}

	public void Sell()
	{
		node.SellTurret ();
		Builder.builderInstance.DeselectNode ();
	}

}
