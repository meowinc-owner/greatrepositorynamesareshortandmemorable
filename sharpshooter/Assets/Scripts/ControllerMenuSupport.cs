using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerMenuSupport : MonoBehaviour
{
    public List<Selectable> selectables = new List<Selectable>();
    [SerializeField] private Selectable firstSelected;
    [SerializeField] private InputActionReference navigateReference;
    private Selectable lastSelected;
    private Vector2 lastNavigateValue;

    private void Awake()
    {
        for (int i = 0; i < selectables.Count; i++)
        {
            AddSelectionListeners(selectables[i]);
        }
    }

    private void OnEnable()
    {
        navigateReference.action.performed += OnNavigate;
        StartCoroutine(SelectAfterOneFrame());
    }

    private void OnDisable()
    {
        navigateReference.action.performed -= OnNavigate;
    }
    private IEnumerator SelectAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    private void AddSelectionListeners(Selectable selectable)
    {
        EventTrigger trigger = selectable.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger =  selectable.gameObject.AddComponent<EventTrigger>();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        
    }

    private IEnumerator SelectNextAfterOneFrame()
    {
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        
    }

    private void OnNavigate(InputAction.CallbackContext callbackContext)
    {
        
    }
}
