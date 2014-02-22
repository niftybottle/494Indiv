using UnityEngine;
using System.Collections;

public class DoubleHealthScript : MonoBehaviour {

	public float maxH1;
	public float maxH2;
	public float health1;
	public float health2;
	public string h1str;
	public string h2str;
	//The cut-off for health2 damaging health1
	public float tippingPoint;
	//The rate at which health1 deacreases when health2 is below tippingPoint, in points/frame
	public float damageRate;
	public string H1label;
	public string H2label;
	public bool softMax;



	// Set values - change these here, or via code (they are public variables)
	void Start () {
		//Set total health for primary (H1) and secondary (H2)
		maxH1 = 100;
		maxH2 = 100;


		health1 = maxH1;
		health2 = maxH2;

		//Set the tipping point where H1 will start to decrease due to H2 being too low (default = 50% of H2)
		tippingPoint = maxH2/2;
		//Set how fast H1 depletes (in damage/frame) after H2 passes the tipping point
		damageRate = .01f;

		h1str = health1.ToString ("F0");
		h2str = health2.ToString ("F0");

		//Set what Health 1 and Health 2 are labeled as
		H1label = "Health 1";
		H2label = "Health 2";

		//Set whether you can exceed the maximum amount of health (by healing) (default = 50%)
		softMax = false;
	}
	//-----------------------------------------------------
	//--- Functions users may want to mess with or call ---
	//-----------------------------------------------------
	//Function to call when health reaches zero
	void OnDeath(){
		
	}
	//Represent damage to Primary Health. Negative damage heals
	void hitHealth1(float damage){
		health1 -= damage;
		h1str = health1.ToString ("F0");
	}
	//Represent damage to Secondary Health. Negative damage heals
	void hitHealth2(float damage){
		health2 -= damage;
		h2str = health2.ToString ("F0");
	}
	//-----------------------------------------------------
	//- End functions users may want to mess with or call -
	//-----------------------------------------------------

	// Update is called once per frame
	void Update () {
		if(health2 < 0){
			health2 = 0;
			h2str = health2.ToString ("F0");
		}
		if(health1 <= 0){
			OnDeath ();
		}

		if (softMax && health1 > maxH1){
			health1 = maxH1;
			h2str = health2.ToString ("F0");
		}
		if (softMax && health2 > maxH2){
			health2 = maxH2;
			h2str = health2.ToString ("F0");
		}
		if(health2 < tippingPoint){
			hitHealth1 (damageRate);
		}
	}
	void OnGUI () {

		GUI.Label(new Rect(0,0,100,25), H1label+": " + h1str);
		GUI.Label(new Rect(0,25,100,25), H2label + ": " + h2str);
	}


}
