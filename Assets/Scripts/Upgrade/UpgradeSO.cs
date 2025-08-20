using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string nameString;
    public Upgrade upgrade;

    [Header("Pricing")]
    public long startingPrice;
    public float priceMultiplier;

    [Header("Stats")]
    public float startValue;
    public float valueChangeAmount;

    public long GetPrice(int level)
    {
        return (long) (startingPrice * Mathf.Pow(priceMultiplier, level));
    }

    public float GetValue(int level)
    {
        return startValue + (valueChangeAmount * level);
    }
}

public enum Upgrade
{
    BounceIncrease,
    FrictionDecrease,
    GravityScale,
    ScoreMultiplier,
    ThrowStrength
}
