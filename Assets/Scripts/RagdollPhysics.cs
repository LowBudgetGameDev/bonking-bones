using System;
using UnityEngine;

public class RagdollPhysics : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeLevelUp += RagdollPhysics_OnUpgradeLevelUp;

        SetPhysicsConstants();
    }

    private void OnDestroy()
    {
        UpgradeManager.Instance.OnUpgradeLevelUp -= RagdollPhysics_OnUpgradeLevelUp;
    }

    private void RagdollPhysics_OnUpgradeLevelUp(object sender, EventArgs e)
    {
        SetPhysicsConstants();
    }

    private void SetPhysicsConstants()
    {
        PhysicsMaterial2D physicsMaterial2D = new PhysicsMaterial2D();

        physicsMaterial2D.friction = GetFrictionAmount();
        physicsMaterial2D.frictionCombine = PhysicsMaterialCombine2D.Minimum;

        physicsMaterial2D.bounciness = GetBounceAmount();
        physicsMaterial2D.bounceCombine = PhysicsMaterialCombine2D.Maximum;

        rigidbody2D.sharedMaterial = physicsMaterial2D;
        rigidbody2D.gravityScale = GetGravityScale();
    }

    private float GetBounceAmount()
    {
        return UpgradeManager.Instance.GetUpgradeValue(Upgrade.BounceIncrease);
    }

    private float GetFrictionAmount()
    {
        return UpgradeManager.Instance.GetUpgradeValue(Upgrade.FrictionDecrease);
    }

    private float GetGravityScale()
    {
        return UpgradeManager.Instance.GetUpgradeValue(Upgrade.GravityScale);
    }
}
