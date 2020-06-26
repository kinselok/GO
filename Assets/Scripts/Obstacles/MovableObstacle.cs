using UnityEngine;
using System.Collections;

public class MovableObstacle: Obstacle
{
    [SerializeField] protected Direction direction;

    private Camera mainCamera;
    protected bool isRemoved = false;
    private float startPositionX;
    protected IEnumerator removeCoroutine;
    protected bool isMoving = false;

    // Use this for initialization
    void Start()
    {
        var swipeDetector = GetComponent<IInputHandler>();

        swipeDetector.OnPress += () => { isMoving = true; };
        swipeDetector.OnPointerMove += Move;
        swipeDetector.OnRelease += () => { isMoving = false; };
        mainCamera = Camera.main;
        startPositionX = transform.position.x;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        isRemoved = false;
        startPositionX = transform.position.x;

        if (removeCoroutine != null)
            StopCoroutine(removeCoroutine);
    }

    protected void Move(Vector3 screenPos)
    {
        if (!isMoving)
            return;
        if (isRemoved)
            return;

        Debug.Log(gameObject.layer);
        var ray = mainCamera.ScreenPointToRay(screenPos);
        Physics.Raycast(ray, out var hit, float.MaxValue, 7 << 8);

        switch (direction)
        {
            case Direction.Left:
                if (startPositionX < hit.point.x)
                    return;
                break;
            case Direction.Right:
                if (startPositionX > hit.point.x)
                    return;
                break;
            case Direction.Horizontal:
                break;
        }

        var pos = new Vector3(hit.point.x, transform.position.y, transform.position.z);
        transform.position = pos;
    }

    protected IEnumerator delayedRemove(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Remove();
    }

    protected void ResumeTime()
    {
        Time.timeScale = 1;
    }
}
