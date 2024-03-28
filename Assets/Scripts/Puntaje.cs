using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Puntaje : MonoBehaviour {

	public bool Turnos;

	public int PuntosJ1;
	public int PuntosJ2;

	public Text TurnosTexto;

	public int Recuadro;

	public int[] PuntuacionesJ1 = new int[11];
	public Text[] TextosScoreJ1 = new Text[11];

	public int[] PuntuacionesJ2 = new int[11];
	public Text[] TextosScoreJ2 = new Text[11];

	public GameObject PanelBorroso;
	public GameObject UIFases;
	public GameObject UIPuntuaciones;
	public GameObject UITurnos;
	public GameObject BotonPausa;
	public Text TextoGanador;
	public Text TextoPuntFin;

	public Evaluador ScriptEvaluador;
	public Pausa ScriptPausa;

	public GameObject Bola;

	void Awake () {
		//Al iniciar, empezar el turno del jugador 1 marcando true el booleano
		//Si "Turnos" = true, es el turno del jugador 1
		//Si "Turnos" = false, es el turno del jugador 2
		Turnos = true;
	}
	

	void FixedUpdate () {

		//Si aun no se terminan todos los recuadros, mostrar en pantalla las puntuaciones
		if (Recuadro < 11) {

			//Escribir en el recuadro actual  del Jugador 1 la puntuación que lleva hasta ahora
			PuntuacionesJ1 [Recuadro] = PuntosJ1;
		
			if (Turnos == true) {
				TextosScoreJ1 [Recuadro].enabled = true;
			}

			TextosScoreJ1 [Recuadro].text = "" + PuntuacionesJ1 [Recuadro];


			//Escribir en el recuadro actual  del Jugador 2 la puntuación que lleva hasta ahora
			PuntuacionesJ2 [Recuadro] = PuntosJ2;

			if (Turnos == false) {
				TextosScoreJ2 [Recuadro].enabled = true;
			}

			TextosScoreJ2 [Recuadro].text = "" + PuntuacionesJ2 [Recuadro];
		}

		
		//Escribir en pantalla el turno de los jugadores
		if (Turnos == true) {
		
			TurnosTexto.text = "Jugador 1";
		
		}

		if (Turnos == false) {
		
			TurnosTexto.text = "Jugador 2";

		}

		//Encontrar la bola actual
		Bola = GameObject.FindGameObjectWithTag("Bola");
	
		//Cuando se acaben los diez cuadros, desactivar la script de la bola actual, la interfaz y hacer el protocolo del ganador
		if (Recuadro >= 11) {

			//Desactivar el sonido y la script de la bola actual
			Bola.GetComponent<Bola> ().BocinaEfecto.enabled = false;
			Bola.GetComponent<Bola> ().enabled = false;

			//Desactivar la interfaz de usuario
			UIFases.SetActive (false);
			UIPuntuaciones.SetActive (false);
			UITurnos.SetActive (false);
			BotonPausa.SetActive (false);

			//Invocar el protocolo en 0.5 segundos
			Invoke ("ProtocoloGanador", 0.5f);

		}

	}



	//Función de hacer el protocolo del ganador
	public void ProtocoloGanador(){

		//Quitar pausa en dado caso que esta activa
		ScriptPausa.Pausado = false;

		//Activar la pantalla borrosa
		PanelBorroso.SetActive (true);

		//Activar los textos del ganador y los puntos finales
		TextoGanador.enabled = true;
		TextoPuntFin.enabled = true;

		//Si el ganador es el jugador 1, mostrar su victoria y los puntos de cada jugador en pantalla
		if (PuntosJ1 > PuntosJ2) {

			TextoGanador.text = "Gana Jugador 1!" + "\n Jugador 1:" + "     Jugador 2:";
			TextoPuntFin.text = "" + PuntosJ1 + "                          " + PuntosJ2;
		}

		//Si el ganador es el jugador 2, mostrar su victoria y los puntos de cada jugador en pantalla
		if (PuntosJ2 > PuntosJ1) {

			TextoGanador.text = "Gana Jugador 2!" + "\n Jugador 1:" + "     Jugador 2:";
			TextoPuntFin.text = "" + PuntosJ1 + "                          " + PuntosJ2;
		}

		//Si se empata, mostrar el empate y los puntos de cada jugador en pantalla
		if (PuntosJ1 == PuntosJ2 || PuntosJ2 == PuntosJ1) {

			TextoGanador.text = "Empate!" + "\n Jugador 1:" + "     Jugador 2:";
			TextoPuntFin.text = "" + PuntosJ1 + "                          " + PuntosJ2;
		}

		//Invocar la función para regresar al menu despues de 4 segundos iniciado el protocolo
		Invoke ("RegresarAlMenu", 2.5f);


	}


	//Función para regresar al menú despues de hacer el protocolo del ganador
	public void RegresarAlMenu(){
		SceneManager.LoadScene ("Bowling-Menu");
	}


}
