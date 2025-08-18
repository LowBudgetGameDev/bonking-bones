using System;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private RectTransform upgradeContainer;
    [SerializeField] private RectTransform upgradeUIPrefab;
    [SerializeField] private UIClick outsideShop;

    private UpgradeListSO upgradeListSO;

    private void Awake()
    {
        upgradeListSO = Resources.Load<UpgradeListSO>(typeof(UpgradeListSO).ToString());

        foreach (UpgradeSO upgradeSO in upgradeListSO.UpgradeList)
        {
            RectTransform upgradeUI = Instantiate(upgradeUIPrefab, upgradeContainer);

            upgradeUI.GetComponent<UpgradeUI>().Setup(upgradeSO);
        }

        outsideShop.OnClick(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        RagdollSpawner.Instance.OnRagdollSpawned += (object sender, EventArgs e) =>
        {
            Show();
        };

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
