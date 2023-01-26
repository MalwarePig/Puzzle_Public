using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tiempo : MonoBehaviour
{
    public Text SegundosText;
    public Text MinutosText;
    public Text RecordText;


    private float tiempo = 0;

    float tiempoRecord = 0f;

    int Nivel = 0;



    // Start is called before the first frame update
    void Start()
    {
        Nivel = PlayerPrefs.GetInt("Nivel");
        tiempoRecord = PlayerPrefs.GetFloat("Mapa" + Nivel, 0);
        //Formateo minutos y segundos a dos dígitos
        string minutos = Mathf.Floor(tiempoRecord / 60).ToString("00");
        string segundos = Mathf.Floor(tiempoRecord % 60).ToString("00");
        RecordText.text = "Record " + minutos + ":" + segundos;
    }

    // Update is called once per frame
    void Update()
    {
        //lineaTiempo = (int)Time.time;
        tiempo += Time.deltaTime; //Le añade a la variable tiempo el tiempo de un frame

        //Formateo minutos y segundos a dos dígitos
        string minutos = Mathf.Floor(tiempo / 60).ToString("00");
        string segundos = Mathf.Floor(tiempo % 60).ToString("00");

        //Mlostrar el cronometro
        MinutosText.text = minutos + " :";
        SegundosText.text = segundos;
    }

    public void escribirRecord()
    {
        Debug.Log("Dentro del if" + tiempoRecord);
        PlayerPrefs.SetFloat("Mapa" + Nivel, tiempo);
    }

   
}




