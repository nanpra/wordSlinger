using UnityEngine;

public class FanRot : MonoBehaviour
{
    [SerializeField] public GameObject FanRotPrefab;
    private int rotSpeed = 100;

    private void Update()
    {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }
}
