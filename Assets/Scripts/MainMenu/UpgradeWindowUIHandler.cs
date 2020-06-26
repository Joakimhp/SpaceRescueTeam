using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindowUIHandler : UIWindowBehaviour
{
    Transform upgradesLayoutGroupTransform;

    PlayerData playerData;

    SOUpgrades soUpgrades;
    List<UpgradeUIHandler> upgradeObjects;
    GameObject upgradePrefab;
    TextMeshProUGUI goldText;

    public void Initialize( PlayerData playerData ) {
        upgradesLayoutGroupTransform = GetComponentInChildren<VerticalLayoutGroup> ().transform;
        this.playerData = playerData;

        TextMeshProUGUI [] tmpTexts = GetComponentsInChildren<TextMeshProUGUI> ();
        goldText = tmpTexts [ tmpTexts.Length - 2 ]; 
            
        soUpgrades = Resources.Load<SOUpgrades> ( "Upgrades" );
        upgradePrefab = Resources.Load<GameObject> ( "UpgradeUI" );
        SetupUpgradeObjects ();

    }

    private void SetupUpgradeObjects() {
        upgradeObjects = new List<UpgradeUIHandler> ();
        for ( int i = 0; i < soUpgrades.upgrades.Count; i++ ) {
            UpgradeUIHandler obj = Instantiate ( upgradePrefab , Vector3.zero , Quaternion.identity , upgradesLayoutGroupTransform ).GetComponent<UpgradeUIHandler>();
            upgradeObjects.Add ( obj );
            Upgrade currentUpgrade = soUpgrades.upgrades [ i ];

            obj.Initialize (currentUpgrade, playerData.upgradeLevels[i], playerData, i, this); 
        }
    }

    public override void Hide() {
        SaveSystem.SavePlayer ( playerData );
        gameObject.SetActive ( false );
    }

    public override void Show() {
        UpdateUI ();
        gameObject.SetActive ( true );
    }

    public override void UpdateUI() {
        goldText.text = playerData.gold.ToString ();
        foreach ( UpgradeUIHandler upgrade in upgradeObjects ) {
            upgrade.UpdateUI ();
        }
    }
}