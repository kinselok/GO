using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public event Action<Obstacle> OnDestroy;
    public int Id { get; set; }

    #region Editor
    [SerializeField] private GameObject helpCanvas;
    [SerializeField] private float velocity;
    [SerializeField] private float lifeTime;
    #endregion
    public float Velocity 
    { 
        get { return velocity; } 
        set 
        { 
            velocity = value;
            rigidbody.velocity = new Vector3(0, 0, velocity);
        }     
    }

    protected Rigidbody rigidbody;
    private IEnumerator DelayedDestroyCorutine;
    private Action onNewElementTrigger;
    private Action onNewElementRemoved;
    public event Action<bool> onWow;
    private bool isNew = false;
    protected bool isClose = false;
    private bool isOnWowCalled = false;

    protected void OnNewElementRemoved()
    {
        if (isNew)
        {
            isNew = false;
            onNewElementRemoved?.Invoke();          
        }
    }
    protected void OnWow(bool val)
    {
        if (!isOnWowCalled)
        {
            isOnWowCalled = true;
            onWow?.Invoke(val);
        }
    }

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Level1.onPause += OnPause;
        Level1.onResume += OnResume;
    }

    protected void ResumeTime()
    {
        Time.timeScale = 1;
    }

    public virtual void Init(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        isOnWowCalled = false;
        rigidbody.velocity = new Vector3(0, 0, velocity);
    }

    public void Init(Action onNewElementTrigger, Action onNewElementRemoved)
    {
        this.onNewElementTrigger = onNewElementTrigger;
        this.onNewElementRemoved = onNewElementRemoved;
        isNew = true;
        gameObject.SetActive(true);
        isOnWowCalled = false;
        rigidbody.velocity = new Vector3(0, 0, velocity);
    }

    private void OnPause()
    {
        rigidbody.velocity = Vector3.zero;
        StopCoroutine(DelayedDestroyCorutine);
    }    
    private void OnResume()
    {
        if (!gameObject.activeSelf)
            return;
        rigidbody.velocity = new Vector3(0, 0, velocity);
        DelayedDestroyCorutine = DelayedDestroy();
        StartCoroutine(DelayedDestroyCorutine);
    }

    protected virtual void OnEnable()
    {
        Debug.Log("Enable " + gameObject.name);
        rigidbody.velocity =  new Vector3(0, 0, velocity);
        DelayedDestroyCorutine = DelayedDestroy();
        StartCoroutine(DelayedDestroyCorutine);
    }



    protected void Remove()
    {
        helpCanvas.SetActive(false);
        Debug.Log("Remove " + gameObject.name);
        OnDestroy?.Invoke(this);
        StopCoroutine(DelayedDestroyCorutine);
        gameObject.SetActive(false);
        Time.timeScale = 1;         

        isClose = false;
        //Exploder.Utils.ExploderSingleton.Instance.ExplodeObject(gameObject);       
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        if(!isNew)
            Remove();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamageHandler damageHandler))
            damageHandler.ApplyDamage(100);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NewElementTrigger" && isNew)
        {
            helpCanvas.SetActive(true);
            rigidbody.velocity = Vector3.zero;
            onNewElementTrigger.Invoke();
            //Debug.Assert(onNewElementTrigger == null, "onNewElementTrigger == null");
        }
        else
            if (other.tag == "Wow")
        {
            isClose = true;
        }
    }
}

