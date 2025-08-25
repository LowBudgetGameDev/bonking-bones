using System;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private RectTransform upgradeContainer;
    [SerializeField] private RectTransform upgradeUIPrefab;
    [SerializeField] private UIClick outsideShop;

    private UpgradeListSO upgradeListSO;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

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

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        animator.SetBool("IsClosed", false);
    }

    public void Hide()
    {
        animator.SetBool("IsClosed", true);

        FunctionTimer.Create(() =>
        {
            gameObject.SetActive(false);
        }, 0.25f);
    }
}
