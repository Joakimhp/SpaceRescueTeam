using System.Linq;
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

    PlayerData playerData;

    public override void Show() {
        throw new System.NotImplementedException ();
    }
    public override void Hide() {
        throw new System.NotImplementedException ();
    }

    public void Initialize( Upgrade upgrade , int currentLevelIndex , PlayerData playerData , int upgradeIndex ) {
        Image [] tmpImages = GetComponentsInChildren<Image> ();
        upgradeImage = tmpImages [ 2 ];

        TextMeshProUGUI [] tmpTexts = GetComponentsInChildren<TextMeshProUGUI> ();
        upgradeName = tmpTexts [ 0 ];
        upgradeLevel = tmpTexts [ 1 ];

        this.playerData = playerData;
        buyButton = GetComponentInChildren<Button> ();

        buyButton.onClick.AddListener ( () => {
            if ( playerData.upgradeLevels [ upgradeIndex ] < myUpgrade.upgradeDatas.Count - 1 ) {

                int priceToPay = upgrade.upgradeDatas [ currentLevelIndex ].upgradeCost;
                if ( playerData.CanBuyForGold ( priceToPay ) ) {
                    playerData.upgradeLevels [ upgradeIndex ]++;
                    SetCurrentLevelIndex ( playerData.upgradeLevels [ upgradeIndex ] );
                    playerData.SubtractGold ( priceToPay );
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
        upgradeName.text = myUpgrade.name;
        upgradeLevel.text = ( currentLevelIndex + 1 ).ToString () + "/" + myUpgrade.upgradeDatas.Count.ToString ();
        buyButton.GetComponentInChildren<TextMeshProUGUI> ().text = "Buy\n" + myUpgrade.upgradeDatas [ currentLevelIndex ].upgradeCost;
    }
}
