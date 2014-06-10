using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public static float Gravity = 45;
	public static float Friction = 0.05f;

	// mode: 1 = heavy, 2 = light
	public int m_mode = 1;
	public bool jumping = false;
	public Material[] materials;

	public float Force = 1.0f;
	public float Mass = 1000.0f;
	public float ground = 0.45f;

	private float vx, vy, vz;

	// Use this for initialization
	void Start () {
		m_mode = 1; // heavy
	}

	void Awake () {
		Debug.Log ("Using" + renderer.materials.Length);
		ChangeMode (1);
		vx = 0.0f;
		vy = 0.0f;
		vz = 0.0f;
		jumping = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 op = transform.position;

		if (Input.GetKeyDown ("1"))
				ChangeMode (1);
		else if (Input.GetKeyDown ("2"))
				ChangeMode (2);

		// get speed scaling factor
		float scale = 1.0f;
		if (Input.GetKey (KeyCode.LeftShift))
			scale = 1.8f;

		// get jump
		if (Input.GetKeyDown (KeyCode.Space) && jumping == false) {
			jumping = true;
			op.y += 0.00001f;
			vy = .5f;
		}

		Debug.Log ("VY = " + vy);

		// get movement input
		float Fx = 0.0f;
		float Fz = 0.0f;
		if (Input.GetKey (KeyCode.DownArrow))
			Fz = -Force;
		if (Input.GetKey (KeyCode.UpArrow))
			Fz = Force;
		if (Input.GetKey (KeyCode.LeftArrow))
			Fx = -Force;
		if (Input.GetKey (KeyCode.RightArrow))
			Fx = Force	;

		// play with gravity
		float Fy = 0.0f;
		if (op.y > ground) {
			Fy = -Gravity;
		} else if (op.y <= ground) {
			Fy = 0;
			jumping = false;
		}

		// apply forces
		float ax = Fx / Mass;
		float az = Fz / Mass;
		float ay = Fy / (Mass > 50f ? 10f : 100f);
		vx += ax * Time.deltaTime;
		vz += az * Time.deltaTime;
		vy += ay * Time.deltaTime;

		// compute ball angles
		float deltax = vx * scale;
		float deltaz = vz * scale;
		const float radius = 0.5f;
		float alphax = 180 * deltax;
		float alphaz = 180 * deltaz;
		transform.Rotate (alphaz, 0, -alphax, Space.World);

		transform.position = new Vector3(
			op.x + deltax,
			(jumping ? op.y + vy : ground),
			op.z + deltaz);
	}

	void ChangeMode(int mode) {
		if (mode != 1 && mode != 2)
			return;

		m_mode = mode;
		Debug.Log ("Mode: " + m_mode);

		switch (m_mode) {
		case 1: // heavy
			renderer.sharedMaterial = materials[1];
			Mass = 100.0f;
			break;
		case 2: // light
			renderer.sharedMaterial = materials[0];
			Mass = 10.0f;
			break;
		}
	}
}
