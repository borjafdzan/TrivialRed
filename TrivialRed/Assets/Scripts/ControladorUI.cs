using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ControladorUI : MonoBehaviour
{
    public PeticionRed controladorPeticion;
    public Peticion peticion;

    public GameObject panelInicio;
    public GameObject panelPreguntas;
    public Text textoRespuesta1;
    public Text textoRespuesta2;
    public Text textoRespuesta3;
    public TextMeshProUGUI textoPregunta;

    private int indiceActual = 0;
    private string[] conjuntoRespuestas;

    void Start(){
        this.conjuntoRespuestas = new string[10];
    }

    public void ClickBtnSiguientePregunta(){
        indiceActual++;
        CargarPreguntasInterfaz();
    }

    public void ClickBtnPreguntaAnterior(){
        indiceActual--;
        CargarPreguntasInterfaz();
    }
    public void ClickEmpezarPartida(){
        this.indiceActual = 0;
        this.controladorPeticion.RequestDatos(this);
        //CargarPreguntasInterfaz();
    }

    public void CargarInicioPartida(){
        this.panelInicio.SetActive(false);
        this.panelPreguntas.SetActive(true);
        this.indiceActual = 0;
        this.conjuntoRespuestas = new string[peticion.results.Length];
        CargarPreguntasInterfaz();
    }

    public void ClickBtnSeleccionarCategoria(){

    }
    public void OnRespuestaCambiar(Toggle respuesta){
        if (respuesta.isOn){
            Text texto = respuesta.GetComponentInChildren<Text>();
            Debug.Log("La respuesta seleccionada es " + texto.text);
            //this.conjuntoRespuestas[indiceActual] = 
        }
    }

    public void CargarPreguntasInterfaz(){
        Pregunta pregunta = peticion.results[indiceActual];
        textoPregunta.text = pregunta.question;
        if (pregunta.type == "boolean"){
            textoRespuesta1.text = pregunta.correct_answer;
            this.textoRespuesta2.gameObject.SetActive(false);
            this.textoRespuesta3.gameObject.SetActive(false);
        } else if (pregunta.type == "multiple"){
            textoRespuesta1.text = pregunta.correct_answer;
            this.textoRespuesta2.gameObject.SetActive(true);
            this.textoRespuesta3.gameObject.SetActive(true);
            if (pregunta.incorrect_answers.Length > 2){
                textoRespuesta2.text = pregunta.incorrect_answers[0];
                textoRespuesta3.text = pregunta.incorrect_answers[1];
            }
        }
    }
}
