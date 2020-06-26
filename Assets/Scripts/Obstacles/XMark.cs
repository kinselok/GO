using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class XMark : MovableObstacle
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == "Border")
        {
            OnWow(isClose);
            isRemoved = true;
            OnNewElementRemoved();
            if (transform.position.x < 0)
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
            }
            Invoke("ResumeTime", 0.5f);
        }
    }
}

