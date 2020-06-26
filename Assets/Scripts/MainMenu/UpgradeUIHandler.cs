using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIHandler : UIWindowBehaviour
{
    private Image upgradeImage;
    private TextMeshProUGUI upgradeName;
    private TextMeshProUGUI upgradeLevel;
    private Button buyButton;

    private Upgrade myUpgrade;
    private int currentLevelIndex;
    private int buyUpgradePrice;

    PlayerData playerData;

    public override void Show() {
        throw new System.NotImplementedException ();
    }
    public override void Hide() {
        throw new System.NotImplementedException ();
    }

    public void Initialize( Upgrade upgrade , int currentLevelIndex , PlayerData playerData , int upgradeIndex , UpgradeWindowUIHandler upgradeWindowUIHandler ) {
        Image [] tmpImages = GetComponentsInChildren<Image> ();
        upgradeImage = tmpImages [ 2 ];

        TextMeshProUGUI [] tmpTexts = GetComponentsInChildren<TextMeshProUGUI> ();
        upgradeName = tmpTexts [ 0 ];
        upgradeLevel = tmpTexts [ 1 ];

        this.playerData = playerData;
        buyUpgradePrice = upgrade.upgradeDatas [ currentLevelIndex ].upgradeCost;

        buyButton = GetComponentInChildren<Button> ();

        buyButton.onClick.AddListener ( () => {
            if ( playerData.upgradeLevels [ upgradeIndex ] < myUpgrade.upgradeDatas.Count - 1 ) {

                buyUpgradePrice = upgrade.upgradeDatas [ this.currentLevelIndex ].upgradeCost;
                if ( playerData.CanBuyForGold ( buyUpgradePrice ) ) {
                    playerData.upgradeLevels [ upgradeIndex ]++;
                    SetCurrentLevelIndex ( playerData.upgradeLevels [ upgradeIndex ] );
                    playerData.SubtractGold ( buyUpgradePrice );
                    upgradeWindowUIHandler.UpdateUI ();
                }

                UpdateUI ();
            }
        } );

        myUpgrade = upgrade;
        this.currentLevelIndex = currentLevelIndex;
    }

    public void SetCurrentLevelIndex( int index ) {
        currentLevelIndex = index;
    }

    public override void UpdateUI() {
        upgradeImage.sprite = myUpgrade.upgradeDatas [ currentLevelIndex ].sprite;
        if ( playerData.CanBuyForGold ( buyUpgradePrice ) ) {
            buyButton.interactable = true;
        } else {
            buyButton.interactable = false;
        }
        upgradeName.text = myUpgrade.name;
        upgradeLevel.text = ( currentLevelIndex + 1 ).ToString () + "/" + myUpgrade.upgradeDatas.Count.ToString ();
        buyButton.GetComponentInChildren<TextMeshProUGUI> ().text = "Buy\n" + myUpgrade.upgradeDatas [ currentLevelIndex ].upgradeCost;
    }
}
