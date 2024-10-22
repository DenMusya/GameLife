using UnityEngine;
using UnityEngine.EventSystems;
public class FreezeEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.FreezeMouse();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.UnfreezeMouse();
    }
}
