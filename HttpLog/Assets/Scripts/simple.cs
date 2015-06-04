using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class simple : MonoBehaviour {

	public Text mytext;
	// Use this for initialization

	void Start () {
	//	Vector3 vec= new Vector3 (477.5,107.5,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}	

	public void setText(){
		mytext.text="This is good!"	;
	}

	//setting boolean parameter
	public void enableBoolean(Animator anim){
		anim.SetBool ("isDisplayed",true);
	}

	public void disableBoolean(Animator anim){
		anim.SetBool ("isDisplayed",false);
	}

	public void enableGraphBoolean(Animator anim){
		anim.SetBool ("graphDisplayed",true);
	}
	
	public void disableGraphBoolean(Animator anim){
		anim.SetBool ("graphDisplayed",false);
	}



}