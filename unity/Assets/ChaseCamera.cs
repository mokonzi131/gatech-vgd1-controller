using UnityEngine;
using System.Collections;

public class ChaseCamera : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Vector3 pos = player.transform.position;
		this.transform.position = new Vector3 (pos.x, pos.y + 2.67f, pos.z - 3f);
	}
}
