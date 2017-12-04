using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public float VerticalMovment = 1;
    public float HorizontalMovment = 1;
    public float Jump = 1;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * HorizontalMovment;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * VerticalMovment;

        transform.Translate(x, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * Jump, ForceMode.Impulse);
        }
    }
}
