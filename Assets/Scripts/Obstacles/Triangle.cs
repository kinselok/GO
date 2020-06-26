using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Left,
    Right,
    Horizontal
}
public class Triangle : MovableObstacle
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == "Border")
        {
            isRemoved = true;
            OnNewElementRemoved();
            OnWow(isClose);
            if (direction == Direction.Left)
                rigidbody.velocity += new Vector3(-5, 0, 0);
            else
                rigidbody.velocity += new Vector3(5, 0, 0);
            
            isMoving = false;
            removeCoroutine = delayedRemove(2);
            StartCoroutine(removeCoroutine);
            if (isClose)
            {               
                PlayerSettings.Instance.WowCounter++;
                Time.timeScale = 0.5f;
                Invoke("ResumeTime", 0.5f);
            }
            

        }
    }
}
