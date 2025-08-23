using TMPro;
using UnityEngine;

public class UITab : MonoBehaviour
{
    [SerializeField] private GameObject content;

    [SerializeField] private TextMeshProUGUI text;

    public void Select()
    {
        content.SetActive(true);
        text.fontStyle = FontStyles.Underline;
    }

    public void Unselect()
    {
        content.SetActive(false);
        text.fontStyle = FontStyles.Normal;
    }
}
