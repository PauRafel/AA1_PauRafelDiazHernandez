using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [TextArea(3, 6)]
    public string description;

    [Header("References")]
    public TMP_Text descriptionLabel;

    private string _defaultText = "Selecciona una simulación para comenzar.";

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionLabel != null)
            descriptionLabel.text = description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionLabel != null)
            descriptionLabel.text = _defaultText;
    }
}