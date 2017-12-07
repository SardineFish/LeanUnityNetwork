using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public float VerticalMovment = 1;
    public float HorizontalMovment = 1;
    public float Jump = 1;
    public Vector3 ctrl;
	// Use this for initialization
	void Start () {
		
	}

    public override void OnStartLocalPlayer()
    {
        Debug.Log("LocalStart");
        Color color;
        ColorUtility.TryParseHtmlString("#66CCFF", out color);
        transform.Find("Capsule").GetComponent<MeshRenderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * HorizontalMovment;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * VerticalMovment;
            ctrl = new Vector3(x, 0, z);
            CmdBroadCastControl(ctrl);
        }
        if (!isLocalPlayer)
            Debug.Log(ctrl);
        transform.Translate(ctrl);

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * Jump, ForceMode.Impulse);
        }*/
    }

    [ClientRpc]
    public void RpcRecvControl(Vector3 ctrl)
    {
        if (isLocalPlayer)
            return;
        this.ctrl = ctrl;
    }
    [Command]
    public void CmdBroadCastControl(Vector3 ctrl)
    {
        RpcRecvControl(ctrl);
    }
}
