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
    public GameObject panelPreguntasMultiples;
    public GameObject panelPreguntaUnica;
    public Text textoRespuesta1;
    public Text textoRespuesta2;
    public Text textoRespuesta3;
    public Text preguntaUnica;

    public Toggle togglePreguntaUnica;

    public GameObject panelCorreccion;
    public TextMeshProUGUI textoRespuestasCorrectas;
    public TextMeshProUGUI textoRespuestasIncorrectas;

    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;
    public TextMeshProUGUI textoPregunta;

    private int indiceActual = 0;
    private string[] conjuntoRespuestas;


    void Awake()
    {
        this.conjuntoRespuestas = new string[10];
    }

    public void ClickBtnSiguientePregunta()
    {
        ComprobarRespuestaUsuario();
        if (indiceActual == conjuntoRespuestas.Length - 1)
        {
            ComprobarRespuestas();
            return;
        }
        indiceActual++;
        CargarPreguntasInterfaz();
    }

    public void ComprobarRespuestas()
    {
        int respuestasCorrectas = 0;
        int respuestasIncorrectas = 0;
        for (int i = 0; i < this.conjuntoRespuestas.Length; i++)
        {
            Debug.Log("La respuesta dada es " + this.conjuntoRespuestas[i]);
            Debug.Log("La respuesta correcta es " + this.peticion.results[i].correct_answer);
            if (this.conjuntoRespuestas[i].Equals(this.peticion.results[i].correct_answer))
            {
                respuestasCorrectas++;
            }
            else
            {
                respuestasIncorrectas++;
            }
        }
        this.textoRespuestasCorrectas.text = "Has acertado " + respuestasCorrectas;
        this.textoRespuestasIncorrectas.text = "Has fallado " + respuestasIncorrectas;
        this.textoRespuestasIncorrectas.text = "Has fallado " + respuestasIncorrectas;
        this.panelPreguntas.SetActive(false);
        this.panelCorreccion.SetActive(true);
    }
    public void ClickBtnPreguntaAnterior()
    {
        ComprobarRespuestaUsuario();
        if (indiceActual == 0)
        {
            return;
        }
        indiceActual--;
        CargarPreguntasInterfaz();
    }
    public void ClickEmpezarPartida()
    {
        this.indiceActual = 0;
        this.controladorPeticion.RequestDatos(this);
    }

    public void CargarInicioPartida()
    {
        this.panelInicio.SetActive(false);
        this.panelPreguntas.SetActive(true);
        this.indiceActual = 0;
        this.conjuntoRespuestas = new string[peticion.results.Length];
        CargarPreguntasInterfaz();
    }


    private void ComprobarRespuestaUsuario()
    {
        if (peticion.results[indiceActual].type == "boolean")
        {
            if (togglePreguntaUnica.isOn)
            {
                this.conjuntoRespuestas[indiceActual] = togglePreguntaUnica.GetComponentInChildren<Text>().text;
            }
            else
            {
                this.conjuntoRespuestas[indiceActual] = togglePreguntaUnica.GetComponentInChildren<Text>().text=="True"?"False":"True";
            }
        }
        else
        {
            Toggle[] toggleMultiples = new Toggle[] { toggle1, toggle2, toggle3 };
            foreach (Toggle tog in toggleMultiples)
            {
                if (tog.isOn)
                {
                    this.conjuntoRespuestas[indiceActual] = tog.GetComponentInChildren<Text>().text;
                }
            }
        }
    }
    /*
    public void OnRespuestaCambiar(Toggle respuesta)
    {
        if (respuesta.isOn)
        {
            Text texto = respuesta.GetComponentInChildren<Text>();
            this.conjuntoRespuestas[indiceActual] = texto.text;
        }
    }
    public void OnRespuetasCambiarBoolean(Toggle toggleRespueta){
        if (toggleRespueta.isOn){
            Text texto = toggleRespueta.GetComponent<Text>();
            this.conjuntoRespuestas[indiceActual] = texto.text;
        } else {
            //this.conjuntoRespuestas[indiceActual] = "Noconstada"
        }
    }
    */

    public void CargarPreguntasInterfaz()
    {
        Pregunta pregunta = peticion.results[indiceActual];
        textoPregunta.text = pregunta.question;
        if (pregunta.type == "boolean")
        {
            preguntaUnica.text = pregunta.correct_answer;
            panelPreguntaUnica.SetActive(true);
            panelPreguntasMultiples.SetActive(false);
        }
        else if (pregunta.type == "multiple")
        {
            panelPreguntaUnica.SetActive(false);
            panelPreguntasMultiples.SetActive(true);
            textoRespuesta1.text = pregunta.correct_answer;
            if (pregunta.incorrect_answers.Length > 2)
            {
                textoRespuesta2.text = pregunta.incorrect_answers[0];
                textoRespuesta3.text = pregunta.incorrect_answers[1];
            }
        }
        if (this.conjuntoRespuestas[indiceActual] != "")
        {
            PonerRespuestasCorrectas();
        }
    }

    private void PonerRespuestasCorrectas()
    {
        Toggle[] listaToggles = new Toggle[] { toggle1, toggle2, toggle3 };
        if (this.conjuntoRespuestas[indiceActual] == null)
        {
            this.toggle1.isOn = true;
        }
        else
        {
            foreach (Toggle to in listaToggles)
            {
                Text textoToggle = to.GetComponentInChildren<Text>();
                if (textoToggle.text == this.conjuntoRespuestas[indiceActual])
                {
                    to.isOn = true;
                }
            }
        }
    }
}
