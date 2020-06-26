using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwipeDetector : MonoBehaviour, IInputHandler, IPointerDownHandler, IPointerExitHandler
{
    public event Action OnUp;
    public event Action OnDown;
    public event Action OnLeft;
    public event Action OnRight;
    public event Action OnPress;
    public event Action OnRelease;
    public event Action<Vector3> OnPointerMove;

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private bool isTouched = false;

    [SerializeField] private bool enableOutOfBoundsDetection = true;
    [SerializeField] private bool detectSwipeOnlyAfterRelease = true;
    [SerializeField] private float SWIPE_THRESHOLD = 20f;

    // Update is called once per frame
    void Update()
    {

        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if(touch.phase == TouchPhase.Moved)
            {
                OnPointerMove?.Invoke(touch.position);
                if(!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if(touch.phase == TouchPhase.Ended)
            {
                if(isTouched || enableOutOfBoundsDetection)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                    isTouched = false;
                    OnRelease?.Invoke();
                }
            }
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if(verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if(fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnUp();
                isTouched = false;
            }
            else if(fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnDown();
                isTouched = false;
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if(horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if(fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnRight();
                isTouched = false;
            }
            else if(fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnLeft();
                isTouched = false;
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouched = true;
        OnPress?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isTouched && !enableOutOfBoundsDetection)
        {
            OnRelease?.Invoke();
            isTouched = false;
        }
    }
}

