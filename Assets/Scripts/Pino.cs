using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pino : MonoBehaviour {

	public Collider Col;
	public AudioSource Bocina;
	public AudioClip SonidoPino;

	public Renderer Renderizador;
	public Material[] Skins;

	public Renderer RenderLinea1;
	public Renderer RenderLinea2;

	void Awake(){

		//Al iniciar la partida, colocar la skin seleccionada anteriormente en el menú
		Renderizador.material = Skins[PlayerPrefs.GetInt("SkinPinos", 0)];

		//Si el valor de la skin de los pinos es 0 o 1 (clasicos o vintage), hacer las lineas rojas
		if(PlayerPrefs.GetInt("SkinPinos", 0) == 0 || PlayerPrefs.GetInt("SkinPinos", 0) == 1) {

			//Se coloca el material de las lineas como el indice 3 del arreglo de las skins ya que éste es de color rojo
			RenderLinea1.material = Skins [3];
			RenderLinea2.material = Skins [3];

		}

		//Si el valor de la skin de los pinos es 2 o 3 (azules o rojos), hacer las lineas blancas
		if (PlayerPrefs.GetInt("SkinPinos", 0) == 2 || PlayerPrefs.GetInt("SkinPinos", 0) == 3) {

			//Se coloca el material de las lineas como el indice 0 del arreglo de las skins ya que éste es de color blanco
			RenderLinea1.material = Skins[0];
			RenderLinea2.material = Skins [0];

		} 
			
	}

	//Función para hacer sonido al colisionar con la bola
	void OnTriggerEnter(Collider Col){
	
		if (Col.gameObject.tag == "Bola") {
		
			Bocina.PlayOneShot (SonidoPino);

		}
			
	}



}
