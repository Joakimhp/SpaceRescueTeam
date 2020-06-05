using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIHandler : UIWindowBehaviour
{
    Image upgradeImage;
    TextMeshProUGUI upgradeName;
    TextMeshProUGUI upgradeLevel;
    Button buyButton;

    Upgrade myUpgrade;
    int currentLevelIndex;

    public override void Show() {
        throw new System.NotImplementedException ();
    }
    public override void Hide() {
        throw new System.NotImplementedException ();
    }

    public void Initialize( Upgrade upgrade , int currentLevelIndex ) {
        Image [] tmpImages = GetComponentsInChildren<Image> ();
        upgradeImage = tmpImages [ 1 ];

        TextMeshProUGUI [] tmpTexts = GetComponentsInChildren<TextMeshProUGUI> ();
        upgradeName = tmpTexts [ 0 ];
        upgradeLevel = tmpTexts [ 1 ];

        buyButton = GetComponentInChildren<Button> ();

        myUpgrade = upgrade;
        this.currentLevelIndex = currentLevelIndex;
    }

    public override void UpdateUI() {
        upgradeImage.sprite = myUpgrade.upgradeDatas [ currentLevelIndex ].sprite;
        upgradeName.text = myUpgrade.name;
        upgradeLevel.text = ( currentLevelIndex + 1 ).ToString () + "/" + myUpgrade.upgradeDatas.Count.ToString ();
        buyButton.GetComponentInChildren<TextMeshProUGUI> ().text = "Buy\n" + myUpgrade.upgradeDatas [ currentLevelIndex ].upgradeCost;
    }
}
