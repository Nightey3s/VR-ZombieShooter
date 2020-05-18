using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    //lifespan
    public float lifespan = 3;
    
    void OnEnable()
    {
        // set a timer for it's destruction
        Invoke("DeactivateBullet", lifespan);
    }

    void OnDisable()
    {
        // cancel pending invokation
        CancelInvoke();
    }

    void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
