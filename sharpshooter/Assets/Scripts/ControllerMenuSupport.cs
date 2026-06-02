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
        
        // select event
        EventTrigger.Entry selectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Select
        };
        selectEntry.callback.AddListener(OnSelect);
        trigger.triggers.Add(selectEntry);
        
        // deselect event
        EventTrigger.Entry deselectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Deselect
        };
        deselectEntry.callback.AddListener(OnDeselect);
        trigger.triggers.Add(deselectEntry);
        
        // pointerEnter Event
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnterEntry.callback.AddListener(OnPointerEnter);
        trigger.triggers.Add(pointerEnterEntry);
        
        // pointerExit Event
        EventTrigger.Entry pointerExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        pointerExit.callback.AddListener(OnPointerExit);
        trigger.triggers.Add(pointerExit);
        
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        Selectable selectable = eventData.selectedObject.GetComponent<Selectable>();
        if (!selectable.interactable)
        {
            StartCoroutine(SelectNextAfterOneFrame(selectable));
        }
        lastSelected = selectable;
    }

    private IEnumerator SelectNextAfterOneFrame(Selectable selectable)
    {
        yield return null;
        if (Mathf.Abs(lastNavigateValue.x) > Mathf.Abs(lastNavigateValue.y))
        {
            if (lastNavigateValue.x < 0)
            {
                EventSystem.current.SetSelectedGameObject(selectable.navigation.selectOnLeft.gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(selectable.navigation.selectOnRight.gameObject);
            }
        }
        else
        {
            if (lastNavigateValue.y > 0)
            {
                EventSystem.current.SetSelectedGameObject(selectable.navigation.selectOnUp.gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(selectable.navigation.selectOnDown.gameObject);
            }
        }
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // do stuff later
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        if(pointerData != null)
        {
            Selectable selectable = pointerData.pointerEnter.GetComponentInParent<Selectable>();
            if (selectable == null)
            {
                selectable = GetComponentInChildren<Selectable>();
            }

            if (selectable.interactable)
            {
                pointerData.selectedObject = selectable.gameObject;
            }
        }
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        if (pointerData != null)
        {
            pointerData.selectedObject = null;
        }
    }

    private void OnNavigate(InputAction.CallbackContext callbackContext)
    {
        lastNavigateValue = callbackContext.ReadValue<Vector2>();
        if (EventSystem.current.currentSelectedGameObject == null && lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected.gameObject);
        }
    }
}
