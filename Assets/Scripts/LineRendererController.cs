using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] Camera REFCamera;

    [SerializeField] public float ZAxixControll;
    [SerializeField] public WordContainerr WordContainerr;
    [SerializeField] private Touch t;
    private Vector3 touchPosinWorldSpace;
    private Vector2 touchPos;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.touches[0].position;
            t = Input.GetTouch(0);
            touchPosinWorldSpace = REFCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, ZAxixControll));
        }
        if (WordContainerr.textGameObjects != null)
        {
            lineRenderer.positionCount = WordContainerr.textGameObjects.Count;
            for (int i = 0; i < WordContainerr.textGameObjects.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(WordContainerr.textGameObjects[i].transform.position.x, WordContainerr.textGameObjects[i].transform.position.y, ZAxixControll));
                if (WordContainerr.textGameObjects != null && (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary))
                {
                    lineRenderer.positionCount = WordContainerr.textGameObjects.Count + 1;
                    lineRenderer.SetPosition(i + 1, touchPosinWorldSpace);
                }
            }
        }
    }
}
