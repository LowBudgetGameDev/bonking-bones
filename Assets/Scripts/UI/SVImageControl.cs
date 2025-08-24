using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Code from https://www.youtube.com/watch?v=otDHGmncBQY

public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] private Image pickerImage;
    [SerializeField] private ColorPickerControl colorPickerControl;

    private RawImage svImage;

    private RectTransform rectTransform;
    private RectTransform pickerTransform;

    private void Awake()
    {
        svImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();

        pickerTransform = pickerImage.GetComponent<RectTransform>();
        pickerTransform.position = new Vector2(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y * 0.5f));
    }

    private void UpdateColor(PointerEventData eventData)
    {
        Vector3 position = rectTransform.InverseTransformPoint(eventData.position);

        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;

        position.x = Mathf.Clamp(position.x, -deltaX, deltaX);
        position.y = Mathf.Clamp(position.y, -deltaY, deltaY);

        float x = position.x + deltaX;
        float y = position.y + deltaY;

        float xNormalized = x / rectTransform.sizeDelta.x;
        float yNormalized = y / rectTransform.sizeDelta.y;

        pickerTransform.localPosition = position;

        pickerImage.color = Color.HSVToRGB(0, 0, 1 - yNormalized);

        colorPickerControl.SetSV(xNormalized, yNormalized);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }
}
