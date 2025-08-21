using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;

    private UpgradeSO upgrade;

    public void Setup(UpgradeSO upgrade)
    {
        this.upgrade = upgrade;

        nameText.SetText(upgrade.nameString);
        priceText.SetText("$" + upgrade.startingPrice.ToString("n0"));
        levelText.SetText("LVL: " + UpgradeManager.Instance.GetUpgradeLevel(upgrade.upgrade).ToString());

        buyButton.onClick.AddListener(() =>
        {
            if (ScoreManager.Instance.GetScore() < upgrade.GetPrice(UpgradeManager.Instance.GetUpgradeLevel(upgrade.upgrade)))
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.UIError);
                return;
            }

            if (!UpgradeManager.Instance.CanLevelUpUpgrade(upgrade.upgrade))
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.UIError);
                return;
            }

            BuyUpgrade();
        });
    }

    private void BuyUpgrade()
    {
        ScoreManager.Instance.DecreaseScore(upgrade.GetPrice(UpgradeManager.Instance.GetUpgradeLevel(upgrade.upgrade)));

        UpgradeManager.Instance.LevelUpUpgrade(upgrade.upgrade);

        priceText.SetText("$" + upgrade.GetPrice(UpgradeManager.Instance.GetUpgradeLevel(upgrade.upgrade)).ToString("n0"));
        levelText.SetText("LVL: " + UpgradeManager.Instance.GetUpgradeLevel(upgrade.upgrade).ToString());

        SoundManager.Instance.PlaySound(SoundManager.Sound.Purchase);

        if (!UpgradeManager.Instance.CanLevelUpUpgrade(upgrade.upgrade))
        {
            priceText.SetText("Maxed Out");
        }
    }
}
