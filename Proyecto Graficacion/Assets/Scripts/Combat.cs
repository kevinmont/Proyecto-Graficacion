﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class Combat : NetworkBehaviour { 

    public const int maxHealth = 100;
    public bool destroyOnDeath;
    
	[SyncVar]
	public int health = maxHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int amount) {
		if (!isServer)
            return;

        health -= amount;
        if (health <= 0) {
            if (destroyOnDeath) {
                Destroy(gameObject);
            }
            health = maxHealth;

            // called on the server, will be invoked on the clients
            RpcRespawn();
        }
    }

    [ClientRpc]
    void RpcRespawn() {
        if (isLocalPlayer) {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }
}
