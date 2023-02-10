using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poemas : MonoBehaviour
{

    [SerializeField] Button textTableButton;
    [SerializeField] Button textSaveButton;
    [SerializeField] GameObject TablaPoemas;
    private string[] Msj = new string[5];
    private bool isShowing = true;

    private void Start()
    {
        CargarTablaMensajes();
    }

    void OnEnable()
    {
        textTableButton.onClick.AddListener(delegate { MostrarTablaMensajes(); });
        textSaveButton.onClick.AddListener(delegate { GuardarMensajes(); });
    }

    void MostrarTablaMensajes()
    {
        isShowing = !isShowing;
        TablaPoemas.SetActive(isShowing);
    }

    void GuardarMensajes()//Guardar urls de inputs a PlayerPrefs
    {
        for (int i = 0; i < 5; i++)
        {
            Msj[i] = GameObject.Find("Mensaje" + i).GetComponent<InputField>().text;
            PlayerPrefs.SetString("Poema" + i, Msj[i]);
        }
    }

    void CargarTablaMensajes()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject.Find("Mensaje" + i).GetComponent<InputField>().text = PlayerPrefs.GetString("Poema" + i);
        }
        MostrarTablaMensajes();
    }



}
