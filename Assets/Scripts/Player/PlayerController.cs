using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const float PLAYER_MOVE_SPEED = 3.0f;
    private const float ARRIVAL_DISTANCE = 0.1f;
    
    private Camera mainCamera;
    private Vector3 targetPosition;
    private Vector3 lastDirection;

    void Start()
    {
        mainCamera = Camera.main;
        targetPosition = transform.position;
    }

    void Update()
    {
        HandleTouchInput();
        MovePlayer();
    }
    
    private void HandleTouchInput()
    {
        var hasInput = false;
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                ProcessInputPosition(touch.position);
                hasInput = true;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            ProcessInputPosition(Input.mousePosition);
            hasInput = true;
        }
        
        if (!hasInput)
        {
            targetPosition = transform.position;
        }
    }
    
    private void ProcessInputPosition(Vector3 inputPosition)
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, 0f));
        targetPosition = new Vector3(worldPosition.x, worldPosition.y, 0f);
    }
    
    private void MovePlayer()
    {
        var distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        var isMoving = distanceToTarget > ARRIVAL_DISTANCE;
        var direction = (targetPosition - transform.position).normalized;
        
        if (isMoving)
        {
            transform.position += direction * PLAYER_MOVE_SPEED * Time.deltaTime;
            lastDirection = direction;
            
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        
        var currentDirection = isMoving ? direction : lastDirection;
        animator.SetFloat("x", Mathf.Clamp(currentDirection.x, -1f, 1f));
        animator.SetFloat("y", Mathf.Clamp(currentDirection.y, -1f, 1f));
    }
}
