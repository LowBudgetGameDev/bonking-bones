using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string nameString;
    public int startingPrice;
    public int priceIncreaseAmount;
    public float startValue;
    public float valueChangeAmount;
}
