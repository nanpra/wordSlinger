using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    private WordContainerr containerInstance;
    [SerializeField] public GraphicRaycaster raycaster;
    private PointerEventData eventDataCurrentPosition1;
    private List<RaycastResult> results;
    private GameObject touchedObject;

    private void Awake()
    {
        containerInstance = GameObject.Find("WordContainer").GetComponent<WordContainerr>();
    }

    private void FixedUpdate()
    {
        HandleTouchBegin();
    }

    private void HandleTouchBegin()
    {
        if (Input.touchCount > 0)
        {
            eventDataCurrentPosition1 = new PointerEventData(EventSystem.current);
            results = new List<RaycastResult>();
            eventDataCurrentPosition1.position = Input.touches[0].position;
            raycaster.Raycast(eventDataCurrentPosition1, results);


            foreach (RaycastResult raycast in results)
            {
                touchedObject = raycast.gameObject;

                if (touchedObject.CompareTag("Pickable") && !CheckDuplicate(touchedObject))
                {
                    containerInstance.textGameObjects.Add(touchedObject);
                }
            }
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                containerInstance.textGameObjects.Clear();
            }
        }
    }
    private bool CheckDuplicate(GameObject data)
    {
        return containerInstance.textGameObjects.Contains(data);
    }
}