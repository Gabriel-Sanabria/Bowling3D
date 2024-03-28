using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour {

	public bool Pausado;
	public bool Opciones;
	public GameObject PanelPausa;
	public GameObject PanelOpciones;

	public AudioSource BocinaPublico;
	public AudioSource BocinaMusica;
	public AudioClip[] Canciones;

	public Dropdown SelectorMusica;
	public Toggle ToggleMusica;
	public Toggle TogglePublico;

	private GameObject BolaActual;

	void Start () {
		//Al iniciar la partida, reproducir la canción que esté seleccionada
		BocinaMusica.clip = Canciones[PlayerPrefs.GetInt("MusicaSeleccionada", 0)];
		BocinaMusica.Play ();

		//Al iniciar el juego, recordar la ultima musica seleccionada
		switch(PlayerPrefs.GetInt("MusicaSeleccionada", 0)){

		case 0:

			SelectorMusica.value = 0;

			break;

		case 1:

			SelectorMusica.value = 1;

			break;

		case 2:

			SelectorMusica.value = 2;

			break;

		case 3:

			SelectorMusica.value = 3;

			break;

		}

		//Al iniciar la partida, recordar si la musica esta activa o no
		if(PlayerPrefs.GetInt("TogMusic", 1) == 0){
			BocinaMusica.enabled = false;
			ToggleMusica.isOn = false;
		}
		if(PlayerPrefs.GetInt("TogMusic", 1) == 1){
			BocinaMusica.enabled = true;
			ToggleMusica.isOn = true;
		}
			
		//Al iniciar la partida, recordar si los efectos del publico estan activos o no
		if(PlayerPrefs.GetInt("TogPub", 1) == 0){
			BocinaPublico.enabled = false;
			TogglePublico.isOn = false;
		}
		if(PlayerPrefs.GetInt("TogPub", 1) == 1){
			BocinaPublico.enabled = true;
			TogglePublico.isOn = true;
		}
		
	}
	

	void Update () {
		//Encontrar la bola actual
		BolaActual = GameObject.FindGameObjectWithTag("Bola");

		//Si la pausa está activa, pausar y aparecer el panel de pausa (Se desactiva la bocina de la bola para evitar que suene durante la pausa)
		if(Pausado == true){
			Time.timeScale = 0;
			BolaActual.GetComponent<AudioSource> ().enabled = false;

			//Si no esta activado el panel de opciones, mostrar el panel principal de pausa
			if (Opciones == false) {
				PanelPausa.SetActive (true);
			}

		}

		//Si la pausa está desactivada, quitar la pausa y desaparecer el panel de pausa
		//(no se activa la bocina de la bola porque eso dependerá de los parametros que su script dicten)
		if(Pausado == false){
			Time.timeScale = 1;
			Opciones = false;
			PanelPausa.SetActive (false);
			PanelOpciones.SetActive (false);
		}

		//Activar o desactivar la musica por medio del toggle
		if(ToggleMusica.isOn == true){
			PlayerPrefs.SetInt ("TogMusic", 1);
			BocinaMusica.enabled = true;
		}
		if (ToggleMusica.isOn == false) {
			PlayerPrefs.SetInt ("TogMusic", 0);
			BocinaMusica.enabled = false;
		}

		//Activar o desactivar los efectos del publico por medio del toggle
		if(TogglePublico.isOn == true){
			PlayerPrefs.SetInt ("TogPub", 1);
			BocinaPublico.enabled = true;
		}
		if (TogglePublico.isOn == false) {
			PlayerPrefs.SetInt ("TogPub", 0);
			BocinaPublico.enabled = false;
		}

	}


	//Función para pausar o despausar el juego dependiendo el caso
	public void Pausar_Despausar(){
		Pausado = !Pausado;
	}

	//Función para activar el panel de opciones
	public void PanelDeOpciones(){
		Opciones = true;
		PanelPausa.SetActive (false);
		PanelOpciones.SetActive (true);
	}

	//Función para seleccionar musica
	public void SeleccionarMusica(){

		switch(SelectorMusica.value){

		case 0:

			PlayerPrefs.SetInt ("MusicaSeleccionada", 0);
			BocinaMusica.clip = Canciones[PlayerPrefs.GetInt("MusicaSeleccionada", 0)];
			BocinaMusica.Play ();

			break;

		case 1:

			PlayerPrefs.SetInt ("MusicaSeleccionada", 1);
			BocinaMusica.clip = Canciones[PlayerPrefs.GetInt("MusicaSeleccionada", 0)];
			BocinaMusica.Play ();

			break;

		case 2:

			PlayerPrefs.SetInt ("MusicaSeleccionada", 2);
			BocinaMusica.clip = Canciones[PlayerPrefs.GetInt("MusicaSeleccionada", 0)];
			BocinaMusica.Play ();

			break;

		case 3:

			PlayerPrefs.SetInt ("MusicaSeleccionada", 3);
			BocinaMusica.clip = Canciones[PlayerPrefs.GetInt("MusicaSeleccionada", 0)];
			BocinaMusica.Play ();

			break;

		}

	}

	//Función para regresar al panel de pausa principal
	public void Regresar(){
		Opciones = false;
		PanelPausa.SetActive (true);
		PanelOpciones.SetActive (false);
	}


	//Función para regresar al menú
	public void RegresarAlMenu(){
		Pausado = false;
		SceneManager.LoadScene ("Bowling-Menu");
	}

}
