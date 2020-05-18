using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class CollectableController : MonoBehaviour {
    
    //serializable class for ANY item stat (health, coins, ammo)
    [Serializable]
    public class ItemStat
    {
        public string label;
        public float amount;
    }

    // flexible array of properties
    public ItemStat[] properties;

    // vr interactive item
    VRInteractiveItem vrItem;

    // player collectable ctrl
    PlayerCollectableCtrl playerCollect;

    void Awake()
    {
        // get the vr interactive item component
        vrItem = GetComponent<VRInteractiveItem>();

        // get the PlayerCollectableCtr object
        playerCollect = FindObjectOfType<PlayerCollectableCtrl>();

        if(playerCollect == null)
        {
            Debug.LogError("There needs to be a PlayerCollectableCtrl item in your scene");
        }
    }

    void OnEnable()
    {
        // subscribe to events
        vrItem.OnOver += HandleOver;
        vrItem.OnOut += HandleOut;
        vrItem.OnClick += HandleClick;
    }

    void OnDisable()
    {
        // unsubscribe to events
        vrItem.OnOver -= HandleOver;
        vrItem.OnOut -= HandleOut;
        vrItem.OnClick -= HandleClick;
    }

    void HandleClick()
    {
        // collect the item
        playerCollect.Collect(gameObject);

        // destroy the item
        Destroy(gameObject);
    }

    void HandleOut()
    {
        //the player collectable ctr knows we are looking away
        playerCollect.SelectionOut();

        // unselect the item
        Highlight(false);
    }

    void HandleOver()
    {
        if(Vector3.Distance(transform.position, playerCollect.gameObject.transform.position) <= playerCollect.maxDistance)
        {
            //the player collectable ctr knows we are selecting
            playerCollect.SelectionOver();

            // highlight the item
            Highlight(true);
        }        
    }

    void Highlight(bool flag)
    {
        GetComponent<Renderer>().material.SetFloat("_Outline", flag ? 0.002f : 0f);
    }

    // get the "amount" of a property with label "label"
    public float GetProperty(string label)
    {
        //iterating through the array to search for the label
        for (int i = 0; i < properties.Length; i++)
        {
            if (properties[i].label == label)
            {
                return properties[i].amount;
            }
        }

        return 0;
    }
}
