using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {

	public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { 
		if (!isLocalPlayer)
			return;

		var x = Input.GetAxis ("Horizontal") * 0.1f;
		var z = Input.GetAxis ("Vertical") * 0.1f;

		transform.Translate (x, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}
	}

	public override void OnStartLocalPlayer() {
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}

	[Command]
	private void CmdFire() {
		GameObject bullet = (GameObject)Instantiate (bulletPrefab, transform.position - transform.forward, Quaternion.identity);

		bullet.GetComponent<Rigidbody> ().velocity = -transform.forward * 4;

		NetworkServer.Spawn (bullet);

		Destroy (bullet, 2f);
	}
}
