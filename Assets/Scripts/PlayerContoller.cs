using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCollectableCtrl))]
[RequireComponent(typeof(PlayerFreeTeleportController))]
public class PlayerContoller : MonoBehaviour
{

    // gun
    public GunController gun;

    // player collect controller
    PlayerCollectableCtrl playerCollect;

    // player free teleport. controller
    PlayerFreeTeleportController playerTeleport;

    void Awake()
    {
        // get components
        playerCollect = GetComponent<PlayerCollectableCtrl>();
        playerTeleport = GetComponent<PlayerFreeTeleportController>();
    }

    void OnEnable()
    {
        // subscribe to events
        playerCollect.OnCollect += HandleCollection;
    }

    void OnDisable()
    {
        // unsubscribe to events
        playerCollect.OnCollect -= HandleCollection;
    }

    // call this when an item is collected
    void HandleCollection(GameObject item)
    {
        gun.Recharge(item.GetComponent<CollectableController>().GetProperty("ammo"));
    }

    void Update()
    {
        // check that we are not selecting (items, teleportation)
        if (playerCollect.IsSelecting() || playerTeleport.IsSelecting()) return;

        // check for button press
        if (Input.GetButtonDown("Fire1"))
        {
            if (gun.CanShoot())
                gun.Shoot();
        }
    }

    
}
