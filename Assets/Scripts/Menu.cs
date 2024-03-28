using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public Animator AnimCamara;
	public GameObject PanelPrincipal;
	public GameObject PanelSkins;
	public GameObject PanelOpciones;

	public RawImage RecuadroImagenJ1;
	public RawImage RecuadroImagenJ2;

	public Texture[] ImagenesSkin;

	public RawImage RecuadroImagenPinos;

	public Color[] ColoresImagenPino;

	public Dropdown SelectorMusica;

	public Toggle ToggleMusica;
	public Toggle TogglePublico;

	public AudioSource BocinaMusica;

	void Start () {

		//Al iniciar el juego, recordar las skins seleccionadas y mostrarlas en los respectivos recuadros de cada jugador y de los pinos
		RecuadroImagenJ1.texture = ImagenesSkin [PlayerPrefs.GetInt ("SkinJ1", 0)];
		RecuadroImagenJ2.texture = ImagenesSkin [PlayerPrefs.GetInt ("SkinJ2", 0)];
		RecuadroImagenPinos.color = ColoresImagenPino [PlayerPrefs.GetInt ("SkinPinos", 0)];

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

		//Al iniciar el juego, recordar la seleccion de los toggles
		if(PlayerPrefs.GetInt("TogMusic", 1) == 0){
			ToggleMusica.isOn = false;
			BocinaMusica.enabled = false;
		}
		if(PlayerPrefs.GetInt("TogMusic", 1) == 1){
			ToggleMusica.isOn = true;
			BocinaMusica.enabled = true;
		}



		if(PlayerPrefs.GetInt("TogPub", 1) == 0){
			TogglePublico.isOn = false;
		}
		if(PlayerPrefs.GetInt("TogPub", 1) == 1){
			TogglePublico.isOn = true;
		}

	}

	void Update () {


		//Selección de musica
		switch(SelectorMusica.value){

		case 0:

			PlayerPrefs.SetInt ("MusicaSeleccionada", 0);

			break;

		case 1:

			PlayerPrefs.SetInt ("MusicaSeleccionada", 1);

			break;

		case 2:

			PlayerPrefs.SetInt ("MusicaSeleccionada", 2);

			break;

		case 3:

			PlayerPrefs.SetInt ("MusicaSeleccionada", 3);

			break;

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
		}
		if (TogglePublico.isOn == false) {
			PlayerPrefs.SetInt ("TogPub", 0);
		}


	}
		
	//Función de cargar el juego
	public void CargarJuego(){
		AnimCamara.SetBool ("Jugar", true);
		Invoke ("IniciarJuego", 0.3f);
	}
		
	//Función de iniciar el juego
	public void IniciarJuego(){
		SceneManager.LoadScene ("Bowling-Game");
	}

	//Función para regresar al menú principal
	public void Regresar(){
		PanelPrincipal.SetActive (true);
		PanelSkins.SetActive (false);
		PanelOpciones.SetActive (false);
	}
		
	//Función para desplegar el submenú de skins
	public void Skins(){
		PanelPrincipal.SetActive (false);
		PanelSkins.SetActive (true);
		PanelOpciones.SetActive (false);
	}

	//Función para desplegar el submenú de opciones
	public void Opciones(){
		PanelPrincipal.SetActive (false);
		PanelSkins.SetActive (false);
		PanelOpciones.SetActive (true);
	}

	//Función para salir del juego
	public void Salir(){
		Application.Quit ();
	}




	/////////////FUNCIONES DE SKINS/////////////

	//JUGADOR 1:

	//Skin Clasica
	public void ClasicaJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [0];
		PlayerPrefs.SetInt ("SkinJ1", 0);
	}

	//Skin Mar
	public void MarJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [1];
		PlayerPrefs.SetInt ("SkinJ1", 1);
	}

	//Skin Chicle
	public void ChicleJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [2];
		PlayerPrefs.SetInt ("SkinJ1", 2);
	}
		
	//Skin Oro
	public void OroJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [3];
		PlayerPrefs.SetInt ("SkinJ1", 3);
	}

	//Skin Colorful
	public void ColorfulJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [4];
		PlayerPrefs.SetInt ("SkinJ1", 4);
	}

	//Skin Sandía
	public void SandiaJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [5];
		PlayerPrefs.SetInt ("SkinJ1", 5);
	}

	//Skin Naranja
	public void NaranjaJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [6];
		PlayerPrefs.SetInt ("SkinJ1", 6);
	}

	//Skin Payaso
	public void PayasoJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [7];
		PlayerPrefs.SetInt ("SkinJ1", 7);
	}

	//Skin Cebra
	public void CebraJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [8];
		PlayerPrefs.SetInt ("SkinJ1", 8);
	}

	//Skin Escocesa
	public void EscocesaJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [9];
		PlayerPrefs.SetInt ("SkinJ1", 9);
	}

	//Skin Militar
	public void MilitarJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [10];
		PlayerPrefs.SetInt ("SkinJ1", 10);
	}

	//Skin Plata
	public void PlataJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [11];
		PlayerPrefs.SetInt ("SkinJ1", 11);
	}

	//Skin Volcanica
	public void VolcanicaJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [12];
		PlayerPrefs.SetInt ("SkinJ1", 12);
	}

	//Skin Abeja
	public void AbejaJ1(){
		RecuadroImagenJ1.texture = ImagenesSkin [13];
		PlayerPrefs.SetInt ("SkinJ1", 13);
	}






	//JUGADOR 2:

	//Skin Clasica
	public void ClasicaJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [0];
		PlayerPrefs.SetInt ("SkinJ2", 0);
	}

	//Skin Mar
	public void MarJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [1];
		PlayerPrefs.SetInt ("SkinJ2", 1);
	}

	//Skin Chicle
	public void ChicleJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [2];
		PlayerPrefs.SetInt ("SkinJ2", 2);
	}

	//Skin Oro
	public void OroJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [3];
		PlayerPrefs.SetInt ("SkinJ2", 3);
	}

	//Skin Colorful
	public void ColorfulJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [4];
		PlayerPrefs.SetInt ("SkinJ2", 4);
	}

	//Skin Sandía
	public void SandiaJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [5];
		PlayerPrefs.SetInt ("SkinJ2", 5);
	}

	//Skin Naranja
	public void NaranjaJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [6];
		PlayerPrefs.SetInt ("SkinJ2", 6);
	}

	//Skin Payaso
	public void PayasoJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [7];
		PlayerPrefs.SetInt ("SkinJ2", 7);
	}

	//Skin Cebra
	public void CebraJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [8];
		PlayerPrefs.SetInt ("SkinJ2", 8);
	}

	//Skin Escocesa
	public void EscocesaJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [9];
		PlayerPrefs.SetInt ("SkinJ2", 9);
	}

	//Skin Militar
	public void MilitarJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [10];
		PlayerPrefs.SetInt ("SkinJ2", 10);
	}

	//Skin Plata
	public void PlataJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [11];
		PlayerPrefs.SetInt ("SkinJ2", 11);
	}

	//Skin Volcanica
	public void VolcanicaJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [12];
		PlayerPrefs.SetInt ("SkinJ2", 12);
	}

	//Skin Abeja
	public void AbejaJ2(){
		RecuadroImagenJ2.texture = ImagenesSkin [13];
		PlayerPrefs.SetInt ("SkinJ2", 13);
	}






	//Pinos:

	//Skin Clasicos
	public void PinosClasicos(){
		RecuadroImagenPinos.color = ColoresImagenPino [0];
		PlayerPrefs.SetInt ("SkinPinos", 0);
	}

	//Skin Vintage
	public void PinosVintage(){
		RecuadroImagenPinos.color = ColoresImagenPino [1];
		PlayerPrefs.SetInt ("SkinPinos", 1);
	}

	//Skin Vintage
	public void PinosAzules(){
		RecuadroImagenPinos.color = ColoresImagenPino [2];
		PlayerPrefs.SetInt ("SkinPinos", 2);
	}

	//Skin Vintage
	public void PinosRojos(){
		RecuadroImagenPinos.color = ColoresImagenPino [3];
		PlayerPrefs.SetInt ("SkinPinos", 3);
	}



}
