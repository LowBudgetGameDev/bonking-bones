using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RagdollLaunch : MonoBehaviour
{
    public event EventHandler OnLaunch;

    [SerializeField] private Rigidbody2D torsoRigidbody2D;
    [SerializeField] private List<Rigidbody2D> limbRigidbodyList;
    [SerializeField] private Transform launchArrowTransform; // The arrow should be a child of the ragdoll to make things easier
    [SerializeField] private SpriteRenderer launchArrowSpriteRenderer;

    private float maxDragDistance = 10f;
    private float maxArrowSize = 5f;

    private Vector3 startDragPosition;

    private bool isLaunching;
    private bool hasLaunched;

    private Vector3 dragDir;
    private float dragStrength;

    private void Awake()
    {
        hasLaunched = false;
        FreezeBody();
    }

    private void Update()
    {
        if (hasLaunched) return;

        if (IsMouseOverUIWithIgnores() && !isLaunching) return; // You cannot launch ragdoll when a manu is open

        if (Input.GetMouseButtonDown(0))
        {
            startDragPosition = UtilsClass.GetMouseWorldPosition();
            launchArrowTransform.gameObject.SetActive(true);
            isLaunching = true;
        }

        if (Input.GetMouseButton(0) && isLaunching)
        {
            Aim();
        }

        if (Input.GetMouseButtonUp(0) && isLaunching)
        {
            Launch();
        }
    }

    private bool IsMouseOverUIWithIgnores()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<MouseUIClickthrough>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }

        return raycastResultList.Count > 0;
    }

    private void Aim()
    {
        Vector3 dragVector = startDragPosition - UtilsClass.GetMouseWorldPosition(); // To make it so that dragging back launches forward

        dragDir = dragVector.normalized;
        dragStrength = Mathf.Clamp(dragVector.magnitude, 0f, maxDragDistance);

        launchArrowTransform.eulerAngles = new Vector3(0f, 0f, UtilsClass.VectorToAngleDegrees(dragDir));

        launchArrowSpriteRenderer.size = new Vector2(dragStrength / maxDragDistance * maxArrowSize, launchArrowSpriteRenderer.size.y);
    }

    private void Launch()
    {
        UnfrezzeBody();

        float launchStrength = dragStrength / maxDragDistance * GetThrowStrength();

        torsoRigidbody2D.AddForce(dragDir * launchStrength, ForceMode2D.Impulse);

        hasLaunched = true;
        launchArrowTransform.gameObject.SetActive(false);

        SoundManager.Instance.PlaySound(SoundManager.Sound.RagdollThrow);

        OnLaunch?.Invoke(this, EventArgs.Empty);
    }

    private void FreezeBody()
    {
        torsoRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        foreach (Rigidbody2D rigidbody2D in limbRigidbodyList)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void UnfrezzeBody()
    {
        torsoRigidbody2D.constraints = RigidbodyConstraints2D.None;

        foreach (Rigidbody2D rigidbody2D in limbRigidbodyList)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
        }
    }

    public bool GetHasLaunched()
    {
        return hasLaunched;
    }

    public float GetThrowStrength()
    {
        return UpgradeManager.Instance.GetUpgradeValue(Upgrade.ThrowStrength) * Mathf.Sqrt(UpgradeManager.Instance.GetUpgradeValue(Upgrade.GravityScale)); // This keeps the same trajectory no matter the gravity scale
    }
}
