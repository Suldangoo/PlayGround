using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private float CAMERA_FOLLOW_SPEED = 5.0f;
    private Vector3 cameraOffset;
    private Vector3 targetPosition;

    void Start()
    {
        if (player == null)
        {
            var playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                player = playerController.transform;
            }
        }
        
        if (player != null)
        {
            cameraOffset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;
        
        targetPosition = player.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, CAMERA_FOLLOW_SPEED * Time.deltaTime);
    }
}
