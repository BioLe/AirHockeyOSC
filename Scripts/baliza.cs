using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class baliza : MonoBehaviour {

	public Text scoreRed, scoreBlue;

	private int goalsRed, goalsBlue;

	private bool resetPlayers;
	private bool resetPuck;

	public int getGoalsRed(){
		return goalsRed;
	}

	public int getGoalsBlue(){
		return goalsBlue;
	}

	public bool getResetPuck(){
		return resetPuck;
	}

	public void setPuck(bool scored){
		this.resetPuck = scored;
	}

	public bool getScored(){
		return resetPlayers;
	}

	public void setScored(bool scored){
		this.resetPlayers = scored;
	}

	void Start(){
		resetPlayers = false;
		resetPuck = false;
	}

	void OnTriggerEnter2D(Collider2D c){

		if(c.name.Equals("Puck")){

			if((gameObject.name).Equals("balizaBlue") ){
				scoreRed.text = "" + (++goalsRed);
			}
			else if((gameObject.name).Equals("balizaRed")){
				scoreBlue.text = "" + (++goalsBlue);
			}

			resetPlayers = true;
			resetPuck = true;
		}

		//	Debug.Log("GOLOOO BALIZA E");
	}

}
