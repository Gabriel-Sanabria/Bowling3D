using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evaluador : MonoBehaviour {

	public Rigidbody[] Pinos = new Rigidbody[10];
	private Vector3[] PosPinos = new Vector3[10];

	private bool[] PinosCaidos = new bool[10];
	public int NumPinosCaidos;

	public Camera Camara;

	public GameObject PrefabBola;

	public Collider AreaDetección;
	public int TirosRegistrados;

	public GameObject InterfazPosicion;
	public GameObject SliderDireccion;
	public GameObject SliderFuerza;

	public Puntaje ScriptPuntaje;

	public int Tiro;

	public AudioSource BocinaNarrador;
	public AudioClip[] SonidosNarrador;

	public GameObject ObjetoAnotacion;
	public Text TextoAnotacion;

	private Bola ScriptBola;

	void Start () {

		//Al comenzar el juego, detectar las posiciones iniciales de los pinos para despues recordarlas
		for (int i = 0; i < PosPinos.Length; i++) {

			PosPinos [i] = Pinos [i].gameObject.transform.position;

		}
	}
	

	void FixedUpdate () {

		//Encontrar la script de la bola
		ScriptBola = GameObject.FindGameObjectWithTag ("Bola").GetComponent<Bola> ();

		//Si se activó el activador para evaluar, comenzar la evaluación
		if (ScriptBola.PinosTumbados == true) {
			
			for (int i = 0; i < Pinos.Length; i++) {

				//Preguntar si el pino se movió en el eje de las Y, si sí, activar true en la casilla correspondiente del arreglo booleano "PinosCaidos"
				if (Pinos [i].velocity.y > 0.08f) {

					PinosCaidos [i] = true;

					//Si no, como segunda opción preguntar si el pino se movió en el eje de las X, si sí, activar true en la casilla correspondiente del arreglo booleano "PinosCaidos"
				}if (Pinos [i].velocity.x > 1.8f) {

					PinosCaidos [i] = true;

				}


			}
				
			//Desde que empezó la evaluación, esperar 3.5 segundos para terminarla
			Invoke ("DesactivarEvaluacion", 3.5f);



			//cuando se desactive el activador de la evaluación, cancelar tambien el desactivador para evitar bugs
		} else {
			
			CancelInvoke ("DesactivarEvaluacion");

		}

	}



	//Función para terminar la evaluación
	void DesactivarEvaluacion(){

		//Desactivar el activador
		ScriptBola.PinosTumbados = false;

		//Contar cuantos pinos se tumbaron y guardarlo en la variable "NumPinosCaidos"
		for (int i = 0; i < PinosCaidos.Length; i++) {

			if (PinosCaidos [i] == true) {
			
				NumPinosCaidos++;

				//Si fue turno del jugador 1 y ha tirado solo una vez, sumar puntos al J1 (Si tira dos veces no sumar para que así solo cuente como media chuza)
				if (Tiro != 2 && ScriptPuntaje.Turnos == true) {

					ScriptPuntaje.PuntosJ1++;

				} else if (NumPinosCaidos < 10 && ScriptPuntaje.Turnos == true) {

					ScriptPuntaje.PuntosJ1++;

				}
			

				//Si fue turno del jugador 2 y ha tirado solo una vez, sumar puntos al J2 (Si tira dos veces no sumar para que así solo cuente como media chuza)
				if (Tiro != 2 && ScriptPuntaje.Turnos == false) {
				
					ScriptPuntaje.PuntosJ2++;

				} else if (NumPinosCaidos < 10 && ScriptPuntaje.Turnos == false) {

					ScriptPuntaje.PuntosJ2++;

				}

				//(Se desactiva el pino ya contado para limpiar el escenario y tambien para evitar bugs)
				Pinos [i].gameObject.SetActive (false);
				PinosCaidos [i] = false;


				//Si no se cayó el pino o no lo detectó, regresar el pino a su posición original
			} else if (PinosCaidos [i] == false) {
			
				Pinos[i].gameObject.SetActive(true);
				Pinos [i].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
				Pinos [i].gameObject.transform.position = PosPinos [i];
				Pinos [i].Sleep ();

			}

		}


		//Verificar si se cayeron todos los pinos, si sí, volver a poner los 10 pinos en las posiciones originales
		for (int i = 0; i < PinosCaidos.Length; i++) {

			//Si anotas chuza o no tumbas ningun pino, reiniciar los pinos
			if (NumPinosCaidos >= 10) {

				Pinos[i].gameObject.SetActive(true);
				Pinos [i].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
				Pinos [i].gameObject.transform.position = PosPinos [i];
				Pinos [i].Sleep ();

				//Reiniciar el numero de tiros registrados para evitar bugs en la jugabilidad
				TirosRegistrados = 0;

			//Si no, Si el numero de tiros es par, reinicialos tambien
			}else if(TirosRegistrados % 2 == 0){

				Pinos[i].gameObject.SetActive(true);
				Pinos [i].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
				Pinos [i].gameObject.transform.position = PosPinos [i];
				Pinos [i].Sleep ();

			}

		}


		//Activar los sliders para faciltarle a la proxima bola encontrarlos (Al instanciar una nueva bola ésta los encontrará y pasados milisegundos los desactivará de nuevo)
		InterfazPosicion.SetActive(true);
		SliderDireccion.SetActive(true);
		SliderFuerza.SetActive(true);

		//Instanciar una nueva bola en la posición de spawn y con su respectiva rotación
		Instantiate (PrefabBola, new Vector3 (0f, 0.185488f, -20.4f), Quaternion.Euler(-43.711f, 0f, 0f));

		//Destruir la bola Anterior
		Destroy (GameObject.FindGameObjectWithTag ("Bola"));

		//Mover la cámara al origen con su respectiva rotación original
		Camara.transform.position = new Vector3 (0f, 2.2f, -25.66f);
		Camara.transform.rotation = Quaternion.Euler (0, 0, 0);


		//Si se tumbaron todos los pinos o sigue el turno del otro jugador
		if (NumPinosCaidos >= 10 || TirosRegistrados % 2 == 0) {

			//Si tumbas diez pinos y eres el jugador 1
			if (NumPinosCaidos >= 10 && ScriptPuntaje.Turnos == true) {

				//se hizo chuza? suma 20 puntos (30 puntos en total)
				if (Tiro == 1) {

					ScriptPuntaje.PuntosJ1 += 20;

					//Anunciar el tiro
						BocinaNarrador.PlayOneShot (SonidosNarrador [0]);
						TextoAnotacion.text = "X";
						ObjetoAnotacion.SetActive (true);
						Invoke ("DesactivarAnotación", 2f);

		
				}

				//Se hizo media chuza? sumale 10 puntos a lo que ya tenía desde el primer tiro
				if (Tiro == 2) {
					ScriptPuntaje.PuntosJ1 += 10;

					//Anunciar el tiro
						BocinaNarrador.PlayOneShot (SonidosNarrador [1]);
						TextoAnotacion.text = "/";
						ObjetoAnotacion.SetActive (true);
						Invoke ("DesactivarAnotación", 2f);

				
				}

			}

			//Si tumbas diez pinos y eres el jugador 2
			if (NumPinosCaidos >= 10 && ScriptPuntaje.Turnos == false) {

				//se hizo chuza? suma 20 puntos (30 puntos en total)
				if (Tiro == 1) {

					ScriptPuntaje.PuntosJ2 += 20;

					//Anunciar el tiro
						BocinaNarrador.PlayOneShot (SonidosNarrador [0]);
						TextoAnotacion.text = "X";
						ObjetoAnotacion.SetActive (true);
						Invoke ("DesactivarAnotación", 2f);

				}

				//Se hizo media chuza? sumale 10 puntos a lo que ya tenía desde el primer tiro y anunciar el tiro por medio del narrador y animación
				if (Tiro == 2) {
					ScriptPuntaje.PuntosJ2 += 10;

					//Anunciar el tiro
						BocinaNarrador.PlayOneShot (SonidosNarrador [1]);
						TextoAnotacion.text = "/";
						ObjetoAnotacion.SetActive (true);
						Invoke ("DesactivarAnotación", 2f);

				}

			}

			//reiniciar la variable para contar solo los pinos del nuevo turno
			NumPinosCaidos = 0;
		}


		//Intercambiar los turnos si se tumban diez pinos o el numero de tiros es par
		if (NumPinosCaidos >= 10) {

			//Se reinicia el numero de tiros hechos en el turno del jugador
			Tiro = 0;

			ScriptPuntaje.Turnos = !ScriptPuntaje.Turnos;

		} else if (TirosRegistrados % 2 == 0) {

			//Se reinicia el numero de tiros hechos en el turno del jugador
			Tiro = 0;
		
			ScriptPuntaje.Turnos = !ScriptPuntaje.Turnos;

		}
			

		//Si aun no se terminan todos los recuadros, mostrar en pantalla las puntuaciones
		if (ScriptPuntaje.Recuadro < 11) {

			//Escribir en el recuadro actual  del Jugador 1 la puntuación que lleva hasta ahora
			ScriptPuntaje.PuntuacionesJ1 [ScriptPuntaje.Recuadro] = ScriptPuntaje.PuntosJ1;

			if (ScriptPuntaje.Turnos == true) {
				ScriptPuntaje.TextosScoreJ1 [ScriptPuntaje.Recuadro].enabled = true;
			}

			ScriptPuntaje.TextosScoreJ1 [ScriptPuntaje.Recuadro].text = "" + ScriptPuntaje.PuntuacionesJ1 [ScriptPuntaje.Recuadro];


			//Escribir en el recuadro actual  del Jugador 2 la puntuación que lleva hasta ahora
			ScriptPuntaje.PuntuacionesJ2 [ScriptPuntaje.Recuadro] = ScriptPuntaje.PuntosJ2;

			if (ScriptPuntaje.Turnos == false) {
				ScriptPuntaje.TextosScoreJ2 [ScriptPuntaje.Recuadro].enabled = true;
			}

			ScriptPuntaje.TextosScoreJ2 [ScriptPuntaje.Recuadro].text = "" + ScriptPuntaje.PuntuacionesJ2 [ScriptPuntaje.Recuadro];
		}



		//Si es el turno del jugador 1 y no ha hecho ningun tiro, pasar al siguiente recuadro
		if (ScriptPuntaje.Turnos == true && Tiro == 0) {
		
			ScriptPuntaje.Recuadro++;

		}
			

	}



	//Función para desactivar las anotaciones
	public void DesactivarAnotación(){
		ObjetoAnotacion.SetActive (false);
	}


		
	}