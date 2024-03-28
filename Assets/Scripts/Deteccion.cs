using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deteccion : MonoBehaviour {

	public Collider ColDetector;
	private Evaluador ScriptEva;

	void Start(){

		//Al iniciar el juego, encontrar la script del evaluador
		ScriptEva = GameObject.Find("Pista").GetComponent<Evaluador>();
	}

	//Función para detectar los tiros que se han hecho en la partida y pasar el dato al evaluador
	void OnTriggerEnter(Collider ColDetector){

		if (ColDetector.gameObject.tag == "Bola") {
		
			ScriptEva.TirosRegistrados++;

		}

	}

}
