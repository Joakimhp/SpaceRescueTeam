using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindowUIHandler : UIWindowBehaviour
{
    Transform upgradesLayoutGroupTransform;

    PlayerData playerData;

    SOUpgrades soUpgrades;
    List<UpgradeUIHandler> upgradeObjects;
    GameObject upgradePrefab;

    public void Initialize( PlayerData playerData ) {
        upgradesLayoutGroupTransform = GetComponentInChildren<VerticalLayoutGroup> ().transform;
        this.playerData = playerData;

        soUpgrades = Resources.Load<SOUpgrades> ( "Upgrades" );
        upgradePrefab = Resources.Load<GameObject> ( "UpgradeUI" );
        SetupUpgradeObjects ();
    }

    private void SetupUpgradeObjects() {
        upgradeObjects = new List<UpgradeUIHandler> ();
        for ( int i = 0; i < soUpgrades.upgrades.Count; i++ ) {
            UpgradeUIHandler obj = Instantiate ( upgradePrefab , Vector3.zero , Quaternion.identity , upgradesLayoutGroupTransform ).GetComponent<UpgradeUIHandler>();
            upgradeObjects.Add ( obj );
            obj.Initialize (); //YOU'RE HERE!!!
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
        
    }
}


public abstract class UIWindowBehaviour : MonoBehaviour
{
    public abstract void Show();
    public abstract void Hide();
    public abstract void UpdateUI();
}