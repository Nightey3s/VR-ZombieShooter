using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenvaVR;

[RequireComponent(typeof(ObjectPool))]
public class GunController : MonoBehaviour {

    // ammo
    public float ammo = 5;

    // max ammo
    public float maxAmmo = 10;

    // bullet speed
    public float bulletSpeed = 50;

    // ammo panel
    public RectTransform ammoIndicator;

    // pool
    ObjectPool bulletPool;

    void Awake()
    {
        //get the object pool for our bullets
        bulletPool = GetComponent<ObjectPool>();

        // initialize
        bulletPool.InitPool();

        // refresh ui
        RefreshUI();
    }

    // return whether we can shoot or not
    public bool CanShoot()
    {
        return ammo > 0;
    }

    // shoot a bullet
    public void Shoot()
    {
        // get a bullet from the pool
        GameObject newBullet = bulletPool.GetObj();

        // position the new bullet at the center of the gun
        newBullet.transform.position = transform.position;

        // get rigid body of the new bullet
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        // give the bullet a velocity
        rb.velocity = transform.forward * bulletSpeed;

        // decrease ammo
        ammo--;

        //refresh ui
        RefreshUI();
    }

    void RefreshUI()
    {
        ammoIndicator.localScale = new Vector3( (float)ammo / maxAmmo , 1, 1);
    }

    //add more ammo
    public void Recharge(float amount)
    {
        // the new ammo value is the minimum between: ammo + amount, maximum possible ammo
        ammo = Mathf.Min(ammo + amount, maxAmmo);

        RefreshUI();
    }
}
