using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed, tilt;
    public float fireDelta;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;


    private Rigidbody rb;
    private float myTime, nextFire;

	private void Start()
	{
        rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
        myTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }

	}

	private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Giving a velocity instead of a force so that the spaceship's 
        // speed is constant at start.
        // This is to give it an arcade feel instead of a real life Feel.
        rb.velocity = movement * speed;

        // Clamp the rigidbody's position to boundaries of the game
        rb.position = new Vector3
            (Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
             0.0f, 
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));

        // Give the player obj a sense of tilt everytime 
        // a velocity is applied in the x direction.
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
