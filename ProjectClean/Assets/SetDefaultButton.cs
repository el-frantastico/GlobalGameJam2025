using UnityEngine;
using UnityEngine.UI;
public class SetDefaultButton : MonoBehaviour
{
    public Button defaultButton;


    private void Start()
    {
        defaultButton.Select();
    }
}
