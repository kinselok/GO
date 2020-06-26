using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseSwipeDetector : MonoBehaviour, IInputHandler, IPointerDownHandler, IPointerExitHandler
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
    [SerializeField] private bool isTouched = false;
    [SerializeField] private bool enableOutOfBoundsDetection = true;

    [SerializeField] private bool detectSwipeOnlyAfterRelease = true;
    [SerializeField] private float SWIPE_THRESHOLD = 20f;

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            fingerUp = Input.mousePosition;
            fingerDown = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            OnPointerMove?.Invoke(Input.mousePosition);
            if(!detectSwipeOnlyAfterRelease)
            {
                fingerDown = Input.mousePosition;
                checkSwipe();
            }
        }

        //Detects swipe after finger is released
        if(Input.GetMouseButtonUp(0))
        {
            if(isTouched || enableOutOfBoundsDetection)
            {
                fingerDown = Input.mousePosition;
                checkSwipe();
                isTouched = false;
                OnRelease?.Invoke();
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
                OnUp?.Invoke();
                isTouched = false;
            }
            else if(fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnDown?.Invoke();
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
                OnRight?.Invoke();
                isTouched = false;
            }
            else if(fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnLeft?.Invoke();
                isTouched = false;
            }
            fingerUp = fingerDown;
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
        if(isTouched && !enableOutOfBoundsDetection)
        {
            OnRelease?.Invoke();
            isTouched = false;
        }
    }
}

