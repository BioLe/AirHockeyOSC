using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bola : MonoBehaviour {

	private Rigidbody2D rb;
	public float maximumSpeed = 10.0f;

	public baliza balizaRed;
    public baliza balizaBlue;

	private float posX, posY, velocity;

	public float getPosX(){
		return posX;
	}

	public float getPosY(){
		return posY;
	}
	
	public float getVel(){
		return velocity;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(balizaBlue.getResetPuck() == true || balizaRed.getResetPuck() == true){
            transform.position = new Vector2(0,0);
            balizaRed.setPuck(false);
			balizaBlue.setPuck(false);
        }


		Vector3 vel = rb.velocity;
		if (vel.magnitude > maximumSpeed) {
			rb.velocity = vel.normalized * maximumSpeed;
		}

		posX = transform.position.x;
		posY = transform.position.y;
		velocity = rb.velocity.magnitude;
	}


}
