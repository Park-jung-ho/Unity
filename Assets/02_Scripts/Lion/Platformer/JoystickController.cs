using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private GameObject backgroundUI;
    [SerializeField] private GameObject handleUI;
    public Button jumpButton;
    public Button attackButton;
    public float maxDistance;
    private Vector2 startPos, currentPos;
    public Vector2 direction;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundUI.gameObject.SetActive(true);
        backgroundUI.transform.position = eventData.position;
        startPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        handleUI.transform.position = eventData.position;
        currentPos = eventData.position;
        direction = (currentPos - startPos).normalized;
        if (Vector2.Distance(startPos, currentPos) > maxDistance)
        {
            handleUI.transform.position = startPos + direction * maxDistance;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handleUI.transform.localPosition = Vector2.zero;
        direction = Vector2.zero;
        backgroundUI.gameObject.SetActive(false);
    }
}
