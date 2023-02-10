using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimpiarPoemas : MonoBehaviour
{
    [SerializeField] private float Campo;
    [SerializeField] private Button LimpiarButton;


    void OnEnable()
    {
        LimpiarButton.onClick.AddListener(delegate { LimpiarCampo(); });
    }

    void LimpiarCampo()
    {
        Debug.Log(Campo);
        Debug.Log(GameObject.Find("Mensaje" + Campo).GetComponent<InputField>().text);
        GameObject.Find("Mensaje" + Campo).GetComponent<InputField>().text = ""; 
    }
}
