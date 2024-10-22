using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;
    public void Toggle()
    {
        canvasObject.SetActive(!canvasObject.activeSelf);
    }
}
