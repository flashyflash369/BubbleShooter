using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpinner : MonoBehaviour,IDamagable
{
    [SerializeField] private float spinSpeed;

    public float force;

    public enum State
    {
        Stationary,
        move
    }
    public State currentState;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*spinSpeed*Time.deltaTime);
        if(currentState == State.move)
        {
            transform.position +=Vector3.up*force*Time.deltaTime;
            Invoke("DestroyObject",7f);
        }
    }

    void IDamagable.OnHit()
    {
         PlayerStats.instance.Health-=1;
    }
    void DestroyObject(){Destroy(gameObject);}
}
