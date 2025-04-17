using System.Collections;
using UnityEngine;

public class BulletFiring : MonoBehaviour
{
    public Transform targetPosition;
    public Transform targetPosition2;
    public float moveSpeed = 5f;
    private Vector3 correctionForHitingBody;
    private Vector3 startPosition;
    private float elapsedTime;
    public Camera cam;
    public GameObject gun;
    public void FixedUpdate()
    {
        Time.timeScale += .2f * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp01(Time.timeScale);
        float newPosition = transform.position.z + (moveSpeed * Time.deltaTime);
        Vector3 targetPosition1 = new Vector3(transform.position.x, transform.position.y, newPosition);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition1, 3 * Time.deltaTime);
    }
    public IEnumerator MoveToTarget()
    {
        gameObject.SetActive(true);
        gun.transform.DetachChildren();
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        startPosition = transform.position;
        elapsedTime = 0f;
        correctionForHitingBody = new Vector3(0, 1.5f, 0);
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition.position + correctionForHitingBody, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed / 10;
            yield return null;
        }
        transform.position = targetPosition.position;
        StartCoroutine(BackToNormalPos());
    }
    public IEnumerator BackToNormalPos()
    {
        yield return new WaitForSeconds(0.6f);
        transform.position = targetPosition2.position;
    }
}