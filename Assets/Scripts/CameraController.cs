using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }



    // runs after all other updates are done
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
