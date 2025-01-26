using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public TextMeshProUGUI theText;

    string originalText;

    private void Start()
    {
        originalText = theText.text;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.text = "[" + theText.text + "]";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.text = originalText;
    }



}