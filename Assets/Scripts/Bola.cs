using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour {

	public Rigidbody RB;
	public float Dirección;
	public float Fuerza;

	public float VelocidadLateral;

	public Camera Camara;
	public float MaxTiempoSeguimiento;
	public Vector3 PosCamaraPersp;
	private bool SeguimientoCamara;
	private float TiempoSeguimiento;

	private bool ActivarSonido;
	public AudioSource BocinaEfecto;

	public bool PinosTumbados;

	public int FaseDeLanzamiento;

	private bool CambioLado;

	public GameObject InterfazPosicion;

	public GameObject BarritaDireccion;
	private Slider SliderDireccion;

	public GameObject BarritaFuerza;
	private Slider SliderFuerza;

	public Puntaje ScriptPuntaje;
	public Evaluador ScriptEvaluador;
	private bool lanzado;

	public Renderer Renderizador;
	public Material[] Skins;

	void Start () {

		//Encontrar la script del puntaje
		ScriptPuntaje = GameObject.Find("Pista").GetComponent<Puntaje>();

		//Establecer la skin del jugador 1 si es el turno del J1
		if(ScriptPuntaje.Turnos == true){
			Renderizador.material = Skins [PlayerPrefs.GetInt ("SkinJ1", 0)];
		}

		//Establecer la skin del jugador 2 si es el turno del J2
		if(ScriptPuntaje.Turnos == false){
			Renderizador.material = Skins [PlayerPrefs.GetInt ("SkinJ2", 0)];
		}

		//Al ser instaciada la bola por primera vez, encontrar la camara
		Camara = GameObject.Find ("Camara").GetComponent<Camera> ();

		//Encontrar el objeto que contiene la interfaz de la posición y desactivarla para evitar que se muestre al principio
		InterfazPosicion = GameObject.FindGameObjectWithTag("posicion");

		//Encontrar la barra de la dirección y desactivarla para evitar que se muestre al principio
		BarritaDireccion = GameObject.FindGameObjectWithTag("dirección");
		SliderDireccion = BarritaDireccion.GetComponent<Slider> ();
		BarritaDireccion.SetActive (false);

		//Encontrar la barra de la fuerza y desactivarla para evitar que se muestre al principio
		BarritaFuerza = GameObject.FindGameObjectWithTag("Fuerza");
		SliderFuerza = BarritaFuerza.GetComponent<Slider> ();
		BarritaFuerza.SetActive (false);

		//Encontrar la script del evaluador
		ScriptEvaluador = GameObject.Find("Pista").GetComponent<Evaluador>();

		//Al instanciar esta bola, activar la fase 1 de lanzamiento
		FaseDeLanzamiento = 1;

	}
	

	void Update () {

		//Al presionar el botón medio del mouse, pasar a la siguiente fase de lanzamiento
		if (Input.GetKeyDown (KeyCode.Mouse2)) {
		
			FaseDeLanzamiento++;

		}
			
		//Condicional multiple para cada fase de lanzamiento
		switch (FaseDeLanzamiento) {

		//FASE 1: POSICIÓN
		//(Controlar posición por medio de los clicks izquierdo y derecho)
		case 1:

			//Activar la interfaz de la posición
			InterfazPosicion.SetActive(true);

			//Si presionas el click izquierdo y la pelota esta a una distancia mayor o igual a -1.5 unidades hacia la izquierda, mover a la izquierda
			if (Input.GetKey (KeyCode.Mouse0) && transform.position.x >= -1.5f) {
				transform.Translate (new Vector3 (-VelocidadLateral * Time.deltaTime, 0, 0));
			}

			//Si presionas el click derecho y la pelota esta a una distancia menor o igual a 1.5 unidades hacia la derecha, mover a la derecha
			if (Input.GetKey (KeyCode.Mouse1) && transform.position.x <= 1.5f) {
				transform.Translate (new Vector3 (VelocidadLateral * Time.deltaTime, 0, 0));
			}


			break;

			//FASE 2: DIRECCIÓN
		case 2:

			//Desactivar la interfaz de la posición
			InterfazPosicion.SetActive(false);

			//Activar la barrita de la dirección
			BarritaDireccion.SetActive (true);

			//Si el cambio de lado esta desactivado, subir handle
			if (CambioLado == false) {


				SliderDireccion.value += 35 * Time.deltaTime;

			}

			//si el handle está en la parte de arriba, activar el cambio de lado
			if (SliderDireccion.value >= SliderDireccion.maxValue) {

				CambioLado = true;

			}

			//Si se cambia de lado, bajar el handle
			if (CambioLado == true) {

				SliderDireccion.value -= 35 * Time.deltaTime;

			}

			//Si el handle esta en la parte de abajo, desactivar el cambio de lado para que suba
			if (SliderDireccion.value <= SliderDireccion.minValue) {

				CambioLado = false;

			}


			//Hacer la magnitud de la fuerza a lo que indique el slider
			Dirección = SliderDireccion.value;


			break;

			//FASE 3: FUERZA
			//(controlar la fuerza de la bola por medio del handle movible de una barra)
		case 3:

			//Desactivar la barrita de la dirección
			BarritaDireccion.SetActive(false);

			//Activar la barrita de la fuerza
			BarritaFuerza.SetActive (true);

			//Si el cambio de lado esta desactivado, subir handle
			if (CambioLado == false) {

				SliderFuerza.value += 80 * Time.deltaTime;

			}

			//si el handle está en la parte de arriba, activar el cambio de lado
			if (SliderFuerza.value >= SliderFuerza.maxValue) {

				CambioLado = true;

			}

			//Si se cambia de lado, bajar el handle
			if (CambioLado == true) {

				SliderFuerza.value -= 80 * Time.deltaTime;

			}

			//Si el handle esta en la parte de abajo, desactivar el cambio de lado para que suba
			if (SliderFuerza.value <= SliderFuerza.minValue) {

				CambioLado = false;

			}


			//Hacer la magnitud de la fuerza a lo que indique el slider
			Fuerza = SliderFuerza.value;

			break;


			//FASE 4: LANZAMIENTO
			//(Lanzar la bola con las condiciones anteriormente dadas y activar el seguimiento de la camara)
		case 4:

			//Se activa el booleano lanzado para que se le pueda sumar al contador de tiros 1
			lanzado = true;

			if (lanzado == true) {
			
				ScriptEvaluador.Tiro += 1;

				//Se desactiva para evitar bugs
				lanzado = false;

			}


			//Desactivar la barrita de la fuerza
			BarritaFuerza.SetActive (false);

			SeguimientoCamara = true;

			RB.AddForce (new Vector3 (Dirección, 0, Fuerza), ForceMode.Impulse);

			//Se pasa a una supuesta fase 5 para evitar que el jugador al dar varios clicks active las acciones de esta fase
			FaseDeLanzamiento = 5;

			break;

		}





		//Si se activo el seguimiento de la bola, seguirla con la camara y empezar el contador
		if (SeguimientoCamara == true) {

			TiempoSeguimiento += 1 * Time.deltaTime;
		
			Camara.transform.position = new Vector3 (transform.position.x, Camara.transform.position.y, transform.position.z -4.5f);

			//Si el contador pasó del tiempo establecido, dejar de seguir y poner la camara en perspectiva
			if (TiempoSeguimiento > MaxTiempoSeguimiento) {
			
				SeguimientoCamara = false;

				Camara.transform.rotation = Quaternion.Euler (0, -30, 0);

				Camara.transform.position = PosCamaraPersp;

			}

		}


		//Si la pelota tiene movimiento y se dió la orden de que se pueda activar el sonido, hacer el sonido del desplazamiento activando la bocina, si no, apagar la bocina
		if (RB.velocity.z > 0 && ActivarSonido == true) {
		
			BocinaEfecto.enabled = true;
		

		} else {
			BocinaEfecto.enabled = false;
		}
			


	}






	//Función para que cuando la bola tumbe pinos (o por lo menos haya pasado por el area donde se encuentran), se active el evaluador para comenzar a evaluar cuantos pinos se tumbaron (Comunicación con el script del evaluador)
	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Area") {

			//Activador de evaluación (aun y no se tumbe ninguno, mandar esta señal)
			PinosTumbados = true;

		}

	}


	//Función para que mientras la bola toque la pista, se pueda escuchar el sonido del desplazamiento
	void OnTriggerStay(Collider other){

		if (other.gameObject.tag == "Pista") {
		
			ActivarSonido = true;

		}

	}

    //Función para que mientras la bola no toque la pista, no se pueda escuchar el sonido de desplazamiento
	void OnTriggerExit(Collider other){

		if (other.gameObject.tag == "Pista") {

			ActivarSonido = false;

		}

	}



}